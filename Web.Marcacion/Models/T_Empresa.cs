using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{
    public class T_Empresa
    {
        [Key]
        public int ID_Empresa { get; set; }
        public string RazonSocial { get; set; }
        public string RUC { get; set; }
        public string Grupo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }



    }
}