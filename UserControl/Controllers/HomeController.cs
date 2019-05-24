using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserControl.Models;

namespace UserControl.Controllers
{
    public class HomeController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public HomeController(
            IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string senha)
        {
            var usuario = new Usuario(login, senha);

            if (usuario.ValidaLogin() == false)
            {
                //mensagem de erro
                return View();
            }

            if (_usuarioRepository.Existe(login, senha))
            {
                usuario = _usuarioRepository.ObterUsuarioPorLogin(login);
                if (usuario.Estado == false)
                {
                    //mensagem de erro
                    return View();
                }

                if (usuario.Perfil.Id == 1)
                {
                    HttpContext.Session.SetString("LogarAdm", "Administrador");
                    return RedirectToAction("Index", "Admin");
                }
                if (usuario.Perfil.Id == 2)
                {
                    HttpContext.Session.SetString("LogarUser", "Usuario");
                    return RedirectToAction("Index", "Usuario");
                }
                else
                {
                    //mensagem de erro
                    return View();
                }
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
