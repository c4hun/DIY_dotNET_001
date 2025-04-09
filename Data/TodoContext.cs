using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> TodoItem { get; set; }

        // OnModelCreating is a method that is called when the model for a derived context is being created.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map TodoItem class to the "todo" table
            modelBuilder.Entity<TodoItem>().ToTable("todo");
        }
    }
}
