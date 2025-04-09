using System.Collections.Generic;

namespace Todo.Models.ViewModels // It is a model for the object that you want to bind to your view
{

    public class TodoViewModel
    {
        public List<TodoItem> TodoList { get; set; }
        public TodoItem Todo { get; set; }
    }
}

// And don't forget to use new statement on `
// ntroller.cs`