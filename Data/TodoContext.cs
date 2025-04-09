using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext (DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<Todo.Models.TodoItem> TodoItem { get; set; }

        // OnModelCreating is a method that is called when the model for a derived context is being created.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Map TodoItem class to the "todo" table
            modelBuilder.Entity<TodoItem>().ToTable("todo");
        }
    }
}
