using Aerolinea.Datos;
using Aerolinea.Entidades.VuelosA;
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
    public class VuelosRepositorio : IVuelosRepositorio
    {
        private readonly AerolineaDbContext _context;
        protected List<Expression<Func<Vuelos, object>>> Includes { get; } = new List<Expression<Func<Vuelos, object>>>();

        public VuelosRepositorio(AerolineaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AgregarAsync(Vuelos vuelo)
        {
            bool existo = true;

            try
            {
                if (vuelo != null)
                {
                    await _context.Vuelos.AddAsync(vuelo);
                    await _context.SaveChangesAsync();

                }

            }
            catch (Exception ex)
            {
                existo = false;
                throw new Exception($"Error al insertar Vuelo {ex}");
            }
            return existo;
        }

        public Task<Vuelos> ObtenerAsync(Expression<Func<Vuelos, bool>> query)
        {
            IQueryable<Vuelos> currentQuery = ImplementIncludes(_context.Vuelos);
            return currentQuery.FirstOrDefaultAsync(query);
        }

        public IQueryable<Vuelos> ImplementIncludes(IQueryable<Vuelos> IncludedQuery)
        {
            IQueryable<Vuelos> currentQuery = IncludedQuery;
            foreach (var include in Includes)
                currentQuery = currentQuery.Include(include);

            return currentQuery;
        }

        public async Task<IEnumerable<Vuelos>> ObtenerTodoAsync()
        {
            try
            {
                return await _context.Vuelos.ToListAsync();

            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido un error" + ex);
            }
        }

        public async Task<bool> ActualizarAsync(Vuelos vuelo)
        {
            bool exito = true;

            try
            {
                _context.Entry(vuelo).State = EntityState.Modified;
                _context.Vuelos.Update(vuelo);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                exito = false;

                throw new Exception($"Ha ocurrido un error {ex}");
            }

            return exito;

        }
        public  async Task<bool> EliminarAync(Vuelos vuelo)
        {
            bool existo = true;
            try
            {
                _context.Vuelos.Remove(vuelo);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                existo = false;
                throw new Exception($"Ha ocurrido un error {ex}");
            }
            return existo;
        }
    }
}
