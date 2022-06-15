using Aerolinea.Datos.Mapeo.Usuarios;
using Aerolinea.Entidades.PaisA;
using Aerolinea.Entidades.Usuarios;
using Aerolinea.Entidades.VuelosA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aerolinea.Datos
{
    public class AerolineaDbContext : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Vuelos> Vuelos { get; set; }
        public DbSet<Pais> Pais { get; set; }

        public AerolineaDbContext(DbContextOptions<AerolineaDbContext> options) : base(options)
        {



        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RolesMap());
            modelBuilder.ApplyConfiguration(new UsuariosMap());
        }

    }
}
