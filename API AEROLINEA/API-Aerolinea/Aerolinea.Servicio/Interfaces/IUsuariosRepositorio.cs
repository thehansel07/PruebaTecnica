using Aerolinea.Entidades.Usuarios;
using Aerolinea.Entidades.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aerolinea.Servicio.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<IEnumerable<Usuario>> ObtenerTodoAsync();
        Task<bool> AgregarAsync(Usuario usuario);
        Task<Usuario> ObtenerAsync(Expression<Func<Usuario, bool>> query);
        Task<bool> ActualizarAsync(Usuario usuario);
        Task<bool> EliminarAync(Usuario usuario);
        Task<Usuario> IniciarSesion(LoginViewModel usuario);




    }
}
