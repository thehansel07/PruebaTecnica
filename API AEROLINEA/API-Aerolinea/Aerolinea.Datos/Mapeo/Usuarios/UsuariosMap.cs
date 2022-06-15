using Aerolinea.Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Aerolinea.Datos.Mapeo.Usuarios
{
    public class UsuariosMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario")
               .HasKey(r => r.IdUsuario);
        }
    }
}
