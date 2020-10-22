﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{
    public class T_TipoJornada
    {
        [Key]
        public int ID_TipoJornada { get; set; }
        public string Descripcion { get; set; }
        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSalida { get; set; }
        public bool Estado { get; set; }

    }
}