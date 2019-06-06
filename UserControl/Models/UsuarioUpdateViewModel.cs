using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace UserControl.Models
{
    public class UsuarioUpdateViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Perfil { get; set; }
        public int PerfilId { get; set; }
        public string PerfilNome { get; set; }
    }
}
