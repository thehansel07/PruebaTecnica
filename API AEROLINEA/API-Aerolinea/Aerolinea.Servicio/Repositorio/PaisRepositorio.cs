using Aerolinea.Datos;
using Aerolinea.Entidades.PaisA;
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
    public class PaisRepositorio : IPaisRepositorio
    {
        private readonly AerolineaDbContext _context;
        public PaisRepositorio(AerolineaDbContext context)
        {
            _context = context;
        }
        protected List<Expression<Func<Pais, object>>> Includes { get; } = new List<Expression<Func<Pais, object>>>();

        public Task<Pais> ObtenerAsync(Expression<Func<Pais, bool>> query)
        {
            IQueryable<Pais> currentQuery = ImplementIncludes(_context.Pais);
            return currentQuery.FirstOrDefaultAsync(query);
        }

        public async Task<IEnumerable<Pais>> ObtenerTodoAsync()
        {
            try
            {
                return await _context.Pais.ToListAsync();

            }
            catch (Exception ex)
            {

                throw new Exception("Ha ocurrido un error" + ex);
            }
        }
        public virtual Pais GetFirst(Expression<Func<Pais, bool>> query)
        {
            IQueryable<Pais> currentQuery = ImplementIncludes(_context.Pais);
            return currentQuery.FirstOrDefault(query);
        }
        public IQueryable<Pais> ImplementIncludes(IQueryable<Pais> IncludedQuery)
        {
            IQueryable<Pais> currentQuery = IncludedQuery;
            foreach (var include in Includes)
                currentQuery = currentQuery.Include(include);

            return currentQuery;
        }
    }
}
