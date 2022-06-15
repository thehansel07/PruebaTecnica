using Aerolinea.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aerolinea.Servicio.Interfaces
{
    public interface IRolesRepositorio
    {
        Task<IEnumerable<Roles>> ObtenerTodoAsync();

    }
}
