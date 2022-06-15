using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aerolinea.Models.Mantenimiento
{
    public class VuelosViewModel
    {

        [Required]
        public int idVuelos { get; set; }
        public int idPaisOrigenVuelo { get; set; }
        public int idPaisDestinoVuelo { get; set; }
        public int cantPersonasVuelo { get; set; }
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }
        public int UsuarioIdUsuario { get; set; }
        public string PaisOrigenVuelo { get; set; }
        public string PaisDestinoVuelo { get; set; }
        public int idUsuario { get; set; }
        public Guid Rowguid { get; set; }
    }
}
