using System.Collections.Generic;

namespace UserControl.Models
{
    public interface IUsuarioRepository
    {
        bool Existe(string login, string senha);
        Usuario ObterUsuarioPorLogin(string login);
        Usuario ObterUsuarioPorId(int id);
        List<Usuario> ObterUsuarios();
        void Update(Usuario usuario);
        void Salvar(Usuario usuario);
    }
}
