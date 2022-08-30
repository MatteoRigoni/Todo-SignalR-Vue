using Microsoft.AspNetCore.SignalR;

public class ToDoHub: Hub
{
    private readonly IToDoRepo repo;

    public ToDoHub(IToDoRepo repo)
    {
        this.repo = repo;
    }

    public async Task GetLists()
    {
        var results = await repo.GetLists();
        await Clients.Caller.SendAsync("updateToDoList", results);
    }

    public async Task GetList(int listId)
    {
        var result = await repo.GetList(listId);
        await Clients.Caller.SendAsync("updatedListData", result);
    }

    public async Task SubscribeToListUpdates(int listId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, _ListIdGroupName(listId));
    }
    public async Task UnsubscribeToListUpdates(int listId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, _ListIdGroupName(listId));
    }
    public async Task SubscribeToCountUpdates()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "counts");
    }
    public async Task UnsubscribeFromCountUpdates()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, "counts");
    }

    public async Task AddToDoItem(int listId, string text)
    {
        await repo.AddToDoItem(listId, text);

        var allLists = await repo.GetLists();   
        var listUpdate = await repo.GetList(listId);   

        await Clients.Group("counts").SendAsync("updateToDoList", allLists);
        await Clients.Group(_ListIdGroupName(listId)).SendAsync("updatedListData", listUpdate);
    }
    
    public async Task ToggleToDoItem(int listId, int itemId)
    {
        await repo.ToggleToDoItem(listId, itemId);

        var allLists = await repo.GetLists();   
        var listUpdate = await repo.GetList(listId);   

        await Clients.Group("counts").SendAsync("updateToDoList", allLists);
        await Clients.Group(_ListIdGroupName(listId)).SendAsync("updatedListData", listUpdate);
    }

    private string _ListIdGroupName(int listId) => $"list-updates-{listId}";
}