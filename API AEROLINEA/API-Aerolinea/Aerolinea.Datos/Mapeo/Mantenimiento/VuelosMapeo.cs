using Aerolinea.Entidades.VuelosA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aerolinea.Datos.Mapeo.Mantenimiento
{
    public class VuelosMapeo : IEntityTypeConfiguration<Vuelos>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Vuelos> builder)
        {
            builder.ToTable("Vuelos")
               .HasKey(r => r.idVuelos);
        }
    }
}
