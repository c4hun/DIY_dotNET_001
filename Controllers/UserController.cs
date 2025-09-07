using Microsoft.AspNetCore.Mvc;
using Todo.Models.Repositories.Interfaces;

namespace Todo.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var users = _repo.GetAllUsers();
            return View(users);
        }
    }
}
