public class InMemoryToDoRepo : IToDoRepo
{
    private static List<ToDoList> Lists { get; set; } = new List<ToDoList>();
    static InMemoryToDoRepo()
    {
        Lists.Add(new ToDoList() { Id = 0, Name = "First", ToDoItems = new List<ToDoItem>() });
        Lists.Add(new ToDoList() { Id = 1, Name = "Second", ToDoItems = new List<ToDoItem>() });
        Lists.Add(new ToDoList() { Id = 2, Name = "Third", ToDoItems = new List<ToDoItem>() });
    }
    public Task<List<ToDoListMinimal>> GetLists()
    {
        return Task.FromResult<List<ToDoListMinimal>>(Lists.Select(p => p.GetMinimal()).ToList());
    }

    public Task<ToDoList> GetList(int id)
    {
        return Task.FromResult(Lists.FirstOrDefault(p => p.Id.Equals(id)));
    }

    public async Task AddToDoItem(int listId, string name)
    {
        var getList = await GetList(listId);
        if (getList == null) 
        {
            throw new NullReferenceException("Invalid list id");
        }
        else
        {
            getList.AddItem(name);
        }
    }

    public async Task ToggleToDoItem(int listId, int listItemId)
    {
        var getList = await GetList(listId);
        if (getList == null) 
        {
            throw new NullReferenceException("Invalid list id");
        }
        else
        {
            getList.ToggleItem(listItemId);
        }
    }
}