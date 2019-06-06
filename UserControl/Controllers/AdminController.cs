using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserControl.Models;

namespace UserControl.Controllers
{
    public class AdminController : Controller
    {
        private IUsuarioRepository _usuarioRepository;
        private IPerfilRepository _perfilRepository;

        public AdminController(
            IUsuarioRepository usuarioRepository,
            IPerfilRepository perfilRepository)
        {
            _usuarioRepository = usuarioRepository;
            _perfilRepository = perfilRepository;
        }

        public IActionResult Index()
        {
            var logado = HttpContext.Session.GetString("LogarAdm");

            if (logado == null || logado.ToString() != "Administrador")
            {
                TempData["LoginErro"] = "Login ou senha incorretos.";
                return RedirectToAction("Index", "Home");
            }

            var listaDeUsuarios = _usuarioRepository.ObterUsuarios();

            var viewModelLista = new List<ExibirUsuariosViewModel>();

            foreach (var usuario in listaDeUsuarios)
            {
                if (usuario.Estado == true)
                {
                    var usuarioViewModel = new ExibirUsuariosViewModel()
                    {
                        id = usuario.Id,
                        login = usuario.Login,
                        senha = usuario.Senha,
                        perfil = usuario.Perfil.Nome
                    };
                    viewModelLista.Add(usuarioViewModel);
                }
            }
            return View(viewModelLista);
        }

        [HttpGet]
        public IActionResult EditarUsuario(int id)
        {
            var usuario = _usuarioRepository.ObterUsuarioPorId(id);

            if(usuario.Id <= 0)
            {
                TempData["NaoExiste"] = "Usuario Inexistente.";
                return RedirectToAction("Index");
            }

            var viewModelUpdate = new UsuarioUpdateViewModel()
            {
                Id = usuario.Id,
                Login = usuario.Login,
                Senha = usuario.Senha,
                PerfilId = usuario.Perfil.Id,
                PerfilNome = usuario.Perfil.Nome
            };

            return View(viewModelUpdate);
        }

        [HttpPost]
        public IActionResult EditarUsuario(UsuarioUpdateViewModel usuarioUpdateView)
        {
            var usuario = _usuarioRepository.ObterUsuarioPorId(usuarioUpdateView.Id);
            var perfil = _perfilRepository.ObterPerfilPorId(usuarioUpdateView.PerfilId);
            usuario.Login = usuarioUpdateView.Login;
            usuario.Senha = usuarioUpdateView.Senha;
            usuario.Perfil = perfil;
            

            if (string.IsNullOrEmpty(usuario.Login) || string.IsNullOrEmpty(usuario.Senha) ||
                usuario.Perfil.Id <= 0 || usuario.Perfil.Id > 2)
            {
                TempData["EditarErro"] = "Verifique se as informações estão corretas";
                return RedirectToAction("EditarUsuario", "Admin", new { id = usuario.Id });
            }
            else
            {
                _usuarioRepository.Update(usuario);
                TempData["EditarSucesso"] = "Usuario editado com sucesso!";
                return RedirectToAction("EditarUsuario", "Admin", new { id = usuario.Id });
            }

        }

        [HttpPost]
        public IActionResult DeletarUsuario(int id)
        {
            if(id <= 0)
            {
                TempData["NaoExiste"] = "Usuario Inexistente.";
                return RedirectToAction("Index", "Admin");
            }

            var usuario = _usuarioRepository.ObterUsuarioPorId(id);
            usuario.Estado = false;
            _usuarioRepository.Update(usuario);
            TempData["DeletarSucesso"] = "Usuario deletado com sucesso!";
            return RedirectToAction("Index", "Admin");
        }
    }
}