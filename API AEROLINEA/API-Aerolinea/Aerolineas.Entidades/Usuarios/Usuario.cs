using Aerolinea.Entidades.VuelosA;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aerolinea.Entidades.Usuarios
{
    public class Usuario
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string UsuNombre { get; set; }
        public string UsuApellidos { get; set; }
        public string UsuNumDocumento { get; set; }
        public string UsuDireccion { get; set; }
        public string Usutelefono { get; set; }

        [Required]
        public string UsuEmail { get; set; }

        [Required]
        public byte[] UsuContraseña { get; set; }

        [Required]
        public byte[] UsuContraseñaConfirma { get; set; }

        public bool Usucondicion { get; set; }
        public int RolesRolId { get; set; }
        public Guid Rowguid { get; set; }

        [NotMapped]
        public string Contraseña { get; set; }

        public Roles Roles { get; set; }
        //public ICollection<Vuelos> Vuelos { get; set; }





    }
}
