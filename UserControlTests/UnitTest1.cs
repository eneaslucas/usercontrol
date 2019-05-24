using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using UserControl.Controllers;
using UserControl.Models;
using Xunit;

namespace UserControlTests
{
    public class UsuarioTests
    {
        internal AdminController sut;
        internal Mock<IUsuarioRepository> usuarioRepositoryMock;

        public void CriarMock()
        {
            this.usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        }

        public void CriarController()
        {
            sut = new AdminController(usuarioRepositoryMock.Object);
        }
        
        [Trait("UsuarioController", "Listar Usuarios")]
        [Fact(DisplayName ="Deveria Retornar Lista De Usuarios")]
        public void DeveriaRetornarListaDeUsuarios()
        {
            //arrange
            CriarMock();

            //act
            CriarController();

            var usuario = new Usuario("Eneas", "Eneas1");
            var perfil = new Perfil();
            perfil.Nome = "Usuario";
            perfil.Id = 2;
            usuario.Estado = true;
            usuario.Id = 1;
            usuario.Perfil = perfil;
            var lista = new List<Usuario>();
            lista.Add(usuario);

            usuarioRepositoryMock.Setup(x => x.ObterUsuarios()).Returns(new List<Usuario>(lista));

            IActionResult result = sut.ListarUsuarios();

            //assert
            Object.Equals(result, typeof(ViewResult));
        }

    }
}
