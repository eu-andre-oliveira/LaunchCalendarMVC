using System.Diagnostics;
using LaunchCalendar.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaunchCalendar.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // redireciona diretamente para o calendário (tela inicial será o calendário)
            return RedirectToAction("CalendarioSemanal", "Cadastro");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
