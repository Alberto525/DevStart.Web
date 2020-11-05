using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{
    public class T_Perfil
    {
        [Key]
        public int ID_Perfil { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

    }

    public class PerfilExcel
    { 
        public string Descripcion { get; set; }

    }
}