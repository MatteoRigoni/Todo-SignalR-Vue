<template>
  <div class="container home">
    <h2>To Do List</h2>
    <p class="text-muted">SignalR example</p>
    <br />
    <table class="table">
      <thead>
        <tr>
          <th>List Name</th>
          <th>Pending</th>
          <th>Completed</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="l in lists" :key="l.id">
          <td>
            <router-link :to="{ name: 'list', params: { listId: l.id } }">{{
              l.name
            }}</router-link>
          </td>
          <td>{{ l.pending }}</td>
          <td>{{ l.completed }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component({
  components: {
    
  },
  filters: {
      pendingCount: (value: any) => {
        const r = value.filter((p: any) => !p.completed);
        return r.length;
      },
      completedCount: (value: any) => {
        const r = value.filter((p: any) => p.completed);
        return r.length;
      },
    },
})
export default class HomeView extends Vue {
  lists: any[] = [];

  async created() {
    Vue.$connectionService.events.on("updatedToDoList", (values: any[]) => {
      this.lists = values;
      console.log(this.lists);
    });
    await this.$connectionService.getLists();
    await this.$connectionService.subscribeToCountUpdates();
  }

  deactivated() {
    this.$connectionService.unsubscribeToCountUpdates();
  }

  demoList() {
    this.lists.push({ id: 0, listName: "List_0", completed: 5, pending: 10 });
    this.lists.push({ id: 1, listName: "List_1", completed: 15, pending: 10 });
    this.lists.push({ id: 2, listName: "List_2", completed: 0, pending: 2 });
  }
}
</script>
