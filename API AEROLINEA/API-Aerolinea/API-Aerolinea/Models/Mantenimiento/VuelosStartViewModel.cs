using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Aerolinea.Models.Mantenimiento
{
    public class VuelosStartViewModel
    {
        public int idVuelos { get; set; } = 0;
        public string idPaisOrigenVuelo { get; set; }
        public string idPaisDestinoVuelo { get; set; }
        public int cantPersonasVuelo { get; set; }
        public DateTime fechaInicial { get; set; }
        public DateTime fechaFinal { get; set; }
 
    }
}

