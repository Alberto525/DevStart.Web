using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{
    public class T_Asistencia
    {
        [Key]
        public int ID_Asistencia { get; set; }
        public string Apellido { get; set; }
        public string NumeroDocumento { get; set; }
        public string Nombre { get; set; }
        public int ID_Usuario { get; set; }
        public string Fecha { get; set; }
        public string HoraIngreso { get; set; }
        public DateTime? HoraSalida { get; set; } //nombre de la jornada
        public string Retraso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? UsuarioModificacion { get; set; }
        public bool Estado { get; set; }
        public string Ubicacion { get; set; }

    }
}