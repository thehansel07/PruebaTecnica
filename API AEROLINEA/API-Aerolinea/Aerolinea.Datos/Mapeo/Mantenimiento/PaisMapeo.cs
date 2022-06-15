using Aerolinea.Entidades.PaisA;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aerolinea.Datos.Mapeo.Mantenimiento
{
    public class PaisMapeo : IEntityTypeConfiguration<Pais>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Pais")
               .HasKey(r => r.idPais);
        }
    }
}
