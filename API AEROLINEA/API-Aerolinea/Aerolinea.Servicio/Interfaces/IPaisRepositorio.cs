using Aerolinea.Entidades.PaisA;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aerolinea.Servicio.Interfaces
{
    public interface IPaisRepositorio
    {
        Task<IEnumerable<Pais>> ObtenerTodoAsync();
        Task<Pais> ObtenerAsync(Expression<Func<Pais, bool>> query);


    }
}
