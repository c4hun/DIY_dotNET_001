using System.Data;

namespace Todo.Models;

public class TodoItem
{
    public int Id { get; set; }
    
    // A solution for the potential issues: Null Reference Issues, Database Cibstraints and Unexpected Behavior in APIs
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public bool IsEnded { get; set; } = false;

}
