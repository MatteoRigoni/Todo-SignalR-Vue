public class ToDoList {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ToDoItem> ToDoItems { get; set; }
    public int Pending => ToDoItems.Count(p => !p.Completed);
    public int Completed => ToDoItems.Count(p => p.Completed);
    public ToDoListMinimal GetMinimal() 
    {
        return new ToDoListMinimal(){
            Id = Id,
            Name = Name,
            Pending = Pending,
            Completed = Completed
        };
    }

    public void AddItem(string name)
    {
        var newId = ToDoItems.Any() ? ToDoItems.Max(p => p.Id) + 1 : 0;
        ToDoItems.Add(new ToDoItem() {
            Name = name,
            Id = newId
        });
    }

    public void ToggleItem(int listItemId)
    {
        var item = ToDoItems.FirstOrDefault(p => p.Id.Equals(listItemId));
        if (item == null)
            throw new NullReferenceException("Invalid item id");
        else
            item.Completed = !item.Completed;
    }
}

public class ToDoListMinimal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Pending { get; set; }
    public int Completed { get; set; }
}