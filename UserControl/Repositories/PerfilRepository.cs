using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserControl.Models;

namespace UserControl.Repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private UserControlContext _context;

        public PerfilRepository(UserControlContext contexto)
        {
            _context = contexto;
        }

        public Perfil ObterPerfilPorId(int id)
        {
            var perfil = _context.Perfis.Single(u => u.Id == id);

            return perfil;
        }
    }
}
