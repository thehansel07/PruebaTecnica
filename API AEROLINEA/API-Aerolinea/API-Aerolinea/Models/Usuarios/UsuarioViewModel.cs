using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aerolinea.Models.Usuarios
{
    public class UsuarioViewModel
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string UsuNombre { get; set; }
        public string UsuNumDocumento { get; set; }
        public string UsuDireccion { get; set; }
        public string Usutelefono { get; set; }
        public string UsuApellidos { get; set; }


        [Required]
        public string UsuEmail { get; set; }

        [Required]
        public string UsuContraseña { get; set; }

        [Required]
        public string UsuContraseñaConfirma { get; set; }

        public string DescricionRol { get; set; }

        public bool Usucondicion { get; set; }
        public int RolesRolId { get; set; }
        public Guid Rowguid { get; set; }
    }
}
