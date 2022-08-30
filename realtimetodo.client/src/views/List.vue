<template>
    <div class="container">
        <h1>List: {{list.name}}</h1>
        <hr />
        <table class="table">
            <thead>                
                <th>Task</th>
                <th>&nbsp;</th>
            </thead>
            <tbody>
                <tr v-for="l in list.toDoItems" :key="l.id">
                    <td>
                        {{l.name}}
                    </td>
                    <td>
                        <input type="checkbox" class="form-input" v-model="l.completed" @change="toggleToDoItem(l.id)" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" class="form-input" v-model.trim="newItemText" />
                        <button class="form-input" @click="addNewItem" >+</button>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component({
  components: {
  },
})
export default class List extends Vue {
    listId = -1;
    newItemText = "";
    list: any = {
        name: "",
        toDoItems: []
    }

    addNewItem() {
        if (this.newItemText == "") return;
        Vue.$connectionService.addToDoItem(this.listId, this.newItemText);
        this.newItemText = "";
    }

    toggleToDoItem(itemId: number) {
        Vue.$connectionService.toggleToDoItem(this.listId, itemId);
    }

    created() {
        this.listId = parseInt(this.$route.params.listId);

        Vue.$connectionService.events.on("updatedListData", (data: any) => {
            this.list = data;
            console.log(data);
        });

        Vue.$connectionService.getListData(this.listId);
        Vue.$connectionService.subscribeToListUpdates(this.listId);
    }

    // destroyed {
    //     Vue.$connectionService.unsubscribeToListUpdates(id);
    // }
}
</script>
