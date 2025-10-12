using DIY_dotNET_001.Repositories;  // Mise à jour du namespace pour les repositories
using DIY_dotNET_001.Services;  // Mise à jour du namespace pour correspondre au projet
using Microsoft.AspNetCore.Mvc;

namespace DIY_dotNET_001.Controllers
{
    public class LoginController : Controller
    {
        private readonly AuthService _authService;

        // Injection de dépendance (via constructeur)
        public LoginController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (_authService.Login(username, password))
            {
                return RedirectToAction("Welcome");
            }

            ViewBag.Error = "Identifiants invalides";
            return View();
        }

        public IActionResult Welcome()
        {
            return View();
        }
    }
}
