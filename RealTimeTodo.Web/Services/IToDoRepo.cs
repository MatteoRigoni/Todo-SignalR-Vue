public interface IToDoRepo {
    Task<List<ToDoListMinimal>> GetLists();
    Task<ToDoList> GetList(int id);
    Task AddToDoItem(int listId, string name);
    Task ToggleToDoItem(int listId, int listItemId);
}