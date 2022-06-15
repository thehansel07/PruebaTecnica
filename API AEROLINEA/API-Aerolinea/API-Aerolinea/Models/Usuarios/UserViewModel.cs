using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aerolinea.Models.Usuarios
{
    public class UserViewModel
    {
        public int idUsuario { get; set; }
        public string usuNombre { get; set; }
        public string usuApellido { get; set; }
        public string usuNumDocumento { get; set; }
        public string usuDireccion { get; set; }
        public string usuTelefono { get; set; }
        public string usuEmail { get; set; }
        public string usuContraseña { get; set; }
        public string usuContraseñaConfirma { get; set; }
        public bool usuCondicion { get; set; }
    }
}