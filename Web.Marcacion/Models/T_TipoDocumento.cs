using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{
    public class T_TipoDocumento
    {
        [Key]
        public int ID_TipoDocumento { get; set; }
        public string Descripcion { get; set; }

    }
}