using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aerolinea.Entidades.PaisA
{
    public class Pais
    {
        [Key]
        public int idPais { get; set; }
        public string paisNombre { get; set; }

    }
}
