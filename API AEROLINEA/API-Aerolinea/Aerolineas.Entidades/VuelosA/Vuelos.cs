using Aerolinea.Entidades.PaisA;
using Aerolinea.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aerolinea.Entidades.VuelosA
{
    public class Vuelos
    {
        [Key]
        public int idVuelos { get; set; }
        public int idPaisOrigenVuelo { get; set; }
        public int idPaisDestinoVuelo { get; set; }
        public int cantPersonasVuelo { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
        public int idUsuario { get; set; }
        public Guid Rowguid { get; set; }

    }
}
