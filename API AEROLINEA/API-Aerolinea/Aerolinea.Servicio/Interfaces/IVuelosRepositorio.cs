using Aerolinea.Entidades.VuelosA;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aerolinea.Servicio.Interfaces
{
    public interface IVuelosRepositorio
    {
        Task<IEnumerable<Vuelos>> ObtenerTodoAsync();
        Task<bool> AgregarAsync(Vuelos vuelo);
        Task<Vuelos> ObtenerAsync(Expression<Func<Vuelos, bool>> query);
        Task<bool> ActualizarAsync(Vuelos vuelo);
        Task<bool> EliminarAync(Vuelos vuelo);
    }
}
