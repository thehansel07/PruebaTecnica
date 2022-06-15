using Aerolinea.Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Aerolinea.Datos.Mapeo.Usuarios
{
    public class RolesMap: IEntityTypeConfiguration<Roles>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Roles")
               .HasKey(r => r.RolId);
        }

    }
}
