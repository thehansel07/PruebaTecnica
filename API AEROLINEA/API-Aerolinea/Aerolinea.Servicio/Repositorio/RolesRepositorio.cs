using Aerolinea.Entidades.Usuarios;
using Aerolinea.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aerolinea.Servicio.Repositorio
{
    public class RolesRepositorio : IRolesRepositorio
    {
        public Task<IEnumerable<Roles>> ObtenerTodoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
