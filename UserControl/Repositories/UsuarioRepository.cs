using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UserControl.Models;

namespace UserControl.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private UserControlContext _context;

        public UsuarioRepository(UserControlContext contexto)
        {
            _context = contexto;
        }

        public bool Existe(string login, string senha)
        {
            bool usuarioExiste = false;

            var usuarios = _context.Usuarios.SingleOrDefault(x => x.Login == login && x.Senha == senha);

            usuarioExiste = usuarios != null;

            return usuarioExiste;
        }

        public Usuario ObterUsuarioPorLogin(string login)
        {
            var usuario = _context.Usuarios.Include(p => p.Perfil).SingleOrDefault(x => x.Login == login);

            return usuario;
        }

        public Usuario ObterUsuarioPorId(int id)
        {
            var usuario = _context.Usuarios.Include(p => p.Perfil).Single(u => u.Id == id);

            return usuario;
        }

        public List<Usuario> ObterUsuarios()
        {
            var usuarios = _context.Usuarios.Include(p => p.Perfil).Where(u => u.Estado == true).ToList();

            return usuarios;
        }

        public void Update (Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Salvar (Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }
    }
}
