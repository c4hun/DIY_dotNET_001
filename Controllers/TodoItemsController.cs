using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Todo.Data;
using Todo.Models;
using Todo.Models.ViewModels;

namespace Todo.Controllers
{
    public class TodoItemsController : Controller
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        { 
            _context = context;
        }

        // GET: TodoItems
        public async Task<IActionResult> Index()
        {
            // Get the list of TodoItems
            var todoItems = await _context.TodoItem.ToListAsync();

            // Create a new TodoViewModel
            var viewModel = new TodoViewModel
            {
                TodoList = todoItems,   // Populate the TodoList
                Todo = new TodoItem()   // Initialize the Todo for potential use (e.g., for adding or editing a TodoItem)
            };

            // Pass the TodoViewModel to the view
            return View(viewModel);
         
        }

        /* public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return View(todoItem);
        } */

        // GET: TodoItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TodoItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedAt,IsEnded")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                todoItem.CreatedAt = DateTime.Now;
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to Index page after creation
            }
            return View(todoItem);
        }

        // GET: TodoItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            // Construct the TodoViewModel for the Edit view
            var viewModel = new TodoViewModel
            {
                TodoList = await _context.TodoItem.ToListAsync(),
                Todo = todoItem  // Set the current TodoItem to be edited
            };

            return Json(new { id = todoItem.Id, name = todoItem.Name });
        }


        // POST: TodoItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,Name")] TodoItem todoItem)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoItemExists(todoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);

        }

        // GET: TodoItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoItem = await _context.TodoItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }

            // Create the TodoViewModel and set the TodoList for Index view
            var viewModel = new TodoViewModel
            {
                TodoList = await _context.TodoItem.ToListAsync(),
                Todo = todoItem // Pass the single TodoItem to the ViewModel
            };

            return View(viewModel);
        }

        // POST: TodoItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItem.Remove(todoItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TodoItemExists(int id)
        {
            return _context.TodoItem.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<JsonResult> populateForm(int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return Json(new { error = "Item not found" });
            }
            return Json(todoItem);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleChange(int id)
        {
            var todoItem = await _context.TodoItem.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.IsEnded = !todoItem.IsEnded;
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

    }
}
