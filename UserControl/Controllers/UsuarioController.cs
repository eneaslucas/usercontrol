using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserControl.Models;

namespace UserControl.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            var logado = HttpContext.Session.GetString("LogarUser");
            TempData["BemVindo"] = "Bem Vindo!";

            if (logado == null || logado.ToString() != logado.ToString())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LogarUser");
            return RedirectToAction("Index", "Home");
        }
    }
}