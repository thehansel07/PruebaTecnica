using Aerolinea.Entidades.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aerolinea.Entidades.Usuarios
{
    public class Roles
    {
        public int RolId { get; set; }
        public string RolDescripcion { get; set; }
        public bool RolCondicion { get; set; }
        public Guid Rowguid { get; set; }
        public ICollection<Usuario> usuarios { get; set; }


    }
}
