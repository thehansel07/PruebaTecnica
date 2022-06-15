using Aerolinea.Datos;
using Aerolinea.Entidades.Usuarios;
using Aerolinea.Entidades.ViewModels;
using Aerolinea.Servicio.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Aerolinea.Servicio.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly AerolineaDbContext _context;
        protected List<Expression<Func<Usuario, object>>> Includes { get; } = new List<Expression<Func<Usuario, object>>>();

        public UsuariosRepositorio(AerolineaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarAsync(Usuario usuario)
        {
            bool existo = true;

            try
            {
                if (usuario != null)
                {
                    await _context.Usuario.AddAsync(usuario);
                    await _context.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                existo = false;
                throw new Exception($"Error al insertar Usuario {ex}");
            }
            return existo;

        }

        public Task<Usuario> ObtenerAsync(Expression<Func<Usuario, bool>> query)
        {
            IQueryable<Usuario> currentQuery = ImplementIncludes(_context.Usuario);
            return currentQuery.FirstOrDefaultAsync(query);
        }
        public virtual Usuario GetFirst(Expression<Func<Usuario, bool>> query)
        {
            IQueryable<Usuario> currentQuery = ImplementIncludes(_context.Usuario);
            return currentQuery.FirstOrDefault(query);
        }
        public IQueryable<Usuario> ImplementIncludes(IQueryable<Usuario> IncludedQuery)
        {
            IQueryable<Usuario> currentQuery = IncludedQuery;
            foreach (var include in Includes)
                currentQuery = currentQuery.Include(include);

            return currentQuery;
        }

        public async Task<IEnumerable<Usuario>> ObtenerTodoAsync()
        {
            try
            {
                return await _context.Usuario.Include(a => a.Roles).ToListAsync();

            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido un error" + ex);
            }
        }
        public async Task<bool> ActualizarAsync(Usuario usuario)
        {
            bool exito = true;

            try
            {
                _context.Entry(usuario).State = EntityState.Modified;
                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                exito = false;

                throw new Exception($"Ha ocurrido un error {ex}");
            }

            return exito;

        }

        public async Task<bool> EliminarAync(Usuario usuario)
        {
            bool existo = true;
            try
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                existo = false;
                throw new Exception($"Ha ocurrido un error {ex}");
            }
            return existo;
        }

        public async Task<Usuario> IniciarSesion(LoginViewModel usuario)
        {

            if (usuario == null)
            {
                return null;

            }
            var modelo = await _context.Usuario.Where
                                (x => x.Usucondicion == true
                                 ).Include(e => e.Roles).FirstOrDefaultAsync(u => u.UsuEmail == usuario.Correo);
            return modelo;
        }
    }
}
