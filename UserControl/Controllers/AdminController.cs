using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserControl.Models;

namespace UserControl.Controllers
{
    public class AdminController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public AdminController(
            IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Index()
        {
            var logado = HttpContext.Session.GetString("LogarAdm");

            if (logado == null || logado.ToString() != "Administrador")
            {
                //mensagem de erro
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public IActionResult ListarUsuarios()
        {
            var listaDeUsuarios = _usuarioRepository.ObterUsuarios();

            var viewModelLista = new List<ExibirUsuariosViewModel>();

            foreach(var usuario in listaDeUsuarios)
            {
                if(usuario.Estado == true)
                {
                    var usuarioViewModel = new ExibirUsuariosViewModel()
                    {
                        login = usuario.Login,
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
                //mensagem de erro
                //return para pagina de Listar usuarios
            }

            var viewModelUpdate = new UsuarioUpdateViewModel()
            {
                id = usuario.Id,
                login = usuario.Login,
                senha = usuario.Senha,
                perfilNome = usuario.Perfil.Nome
            };

            return View(viewModelUpdate);
        }

        [HttpPost]
        public IActionResult EditarUsuario(UsuarioUpdateViewModel usuarioUpdateView)
        {
            var usuario = _usuarioRepository.ObterUsuarioPorId(usuarioUpdateView.id);
            usuario.Login = usuarioUpdateView.login;
            usuario.Senha = usuarioUpdateView.senha;
            usuario.Perfil.Id = usuarioUpdateView.perfilId;

            if(string.IsNullOrEmpty(usuario.Login) || string.IsNullOrEmpty(usuario.Senha) ||
                usuario.Perfil.Id <= 0 || usuario.Perfil.Id > 2)
            {
                //mensagem de erro
                return RedirectToAction("EditarUsuario", "Admin", new { id = usuario.Id });
            }
            else
            {
                _usuarioRepository.Update(usuario);
                //mensagem de confirmação
                return RedirectToAction("EditarUsuario", "Admin", new { id = usuario.Id });
            }

        }

        [HttpPost]
        public IActionResult DeletarUsuario(int id)
        {
            if(id <= 0)
            {
                //mensagem de erro
                return RedirectToAction("EditarUsuario", "Admin");
            }

            var usuario = _usuarioRepository.ObterUsuarioPorId(id);
            usuario.Estado = false;
            _usuarioRepository.Update(usuario);
            //mensagem de sucesso
            return RedirectToAction("EditarUsuario", "Admin");
        }
    }
}