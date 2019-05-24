using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserControl.Models;

namespace UserControl.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index(Usuario usuario)
        {
            var logado = HttpContext.Session.GetString("LogarUser");

            if (logado == null || logado.ToString() != logado.ToString())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}