using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{
    public class T_EmpleadoJornada
    {

        [Key]
        public int ID_EmpleadoJornada { get; set; }
        public int ID_Empresa { get; set; }
        public int ID_Usuario { get; set; }
        public int ID_TipoJornada { get; set; }
        public string Descripcion { get; set; } //nombre de la jornada
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }

    }
}