using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{
    public class T_Persona
    {
        [Key]
        public int ID_Persona { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int ID_TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int ID_Empresa { get; set; }
        public string Sexo { get; set; }
        public string Correo { get; set; }
        public string Movil { get; set; }
        public string Direccion { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Cargo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
        public int UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
    }
}