using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Microsoft.Extensions.Localization;
using Microsoft.Data.Sqlite;
using Todo.Models.ViewModels;

namespace Todo.Controllers;

public class HomeController() : Controller
{
    // private readonly ILogger<HomeController> _logger;

    // public HomeController(ILogger<HomeController> logger)
    // {
    //    _logger = logger;
    // }

    public IActionResult Index()
    {
        var todoListViewModel = GetAllTodos();
        return View(todoListViewModel);
    }

    [HttpGet]
    public JsonResult PopulateForm(int id)
    {
        var todo = GetById(id);
        return Json(todo);
    }

    internal TodoViewModel GetAllTodos()
    {
        List<TodoItem> todoList = new();

        using (SqliteConnection con = 
            new SqliteConnection("Data Source=sqlite.db"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = "SELECT * FROM todo";

                    // And on our database we have to create a reader, so that is an instance of the sqlite reader class
                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                todoList.Add(
                                    new TodoItem
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        CreatedAt = reader.GetDateTime(2),
                                        IsEnded = reader.GetBoolean(3)
                                    }
                                );
                            }
                        }
                        else{
                            return new TodoViewModel
                            {
                                TodoList = todoList
                            };
                        }
                    };
                }
                // Don't Forget to return a TodoViewModel with our todolist
                return new TodoViewModel
                {
                    TodoList = todoList
                };
            };
    }


    internal TodoItem GetById(int id)
    {
        TodoItem todo = new();

        using (var connection = 
            new SqliteConnection("Data Source=sqlite.db"))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT * FROM todo Where Id = '{id}'";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            todo.Id = reader.GetInt32(0);
                            todo.Name = reader.GetString(1);
                            todo.CreatedAt = reader.GetDateTime(2);
                        }
                        else
                        {
                            return todo;
                        }
                    };
                }
            }
        return todo;
    }

    [HttpPost]
    public IActionResult Update(TodoItem todo)
    {
        using (SqliteConnection con = 
            new SqliteConnection("Data Source=sqlite.db")) {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"UPDATE todo SET name = '{todo.Name}' WHERE Id = '{todo.Id}'";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex){
                        Console.WriteLine(ex.Message); }
                }
            }
        return RedirectToAction("Index");
    }

    public IActionResult Insert(TodoItem todo)
    {
        using (SqliteConnection con = 
            new SqliteConnection("Data Source=sqlite.db")) {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"INSERT INTO todo (name) VALUES ('{todo.Name}')";
                    try
                    {
                        tableCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex){
                        Console.WriteLine(ex.Message); }
                }
            }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public JsonResult Delete(int id)
    {
        using (SqliteConnection con = 
            new SqliteConnection("Data Source=sqlite.db"))
            {
                using (var tableCmd = con.CreateCommand())
                {
                    con.Open();
                    tableCmd.CommandText = $"DELETE from todo WHERE Id = '{id}'";
                    tableCmd.ExecuteNonQuery();
                }
            }
        return Json(new {});
    }

    [HttpPost]
    public IActionResult ToggleChange(int id)
    {
        using (var connection = new SqliteConnection("Data source=sqlite.db"))
        {
            using (var command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "UPDATE todo SET IsEnded = NOT IsEnded WHERE Id = @Id"; // Inverse the value of IsEnded
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }


}
