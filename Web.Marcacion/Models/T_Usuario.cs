using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Marcacion.Models
{
    public class T_Usuario
    {
        [Key]
        public int ID_Usuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int ID_Persona { get; set; }
        public bool Estado { get; set; }
        public int ID_Perfil { get; set; }          
     
       

    }

    public class T_Usuario_Reg
    {
        [Key]
        public int ID_Usuario { get; set; }
        public string Correo { get; set; }
        public string RazonSocial { get; set; }
        public string NumeroDocumento { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int ID_Persona { get; set; }
        public bool Estado { get; set; }
        public int ID_Perfil { get; set; }
        public int ID_TipoJornada { get; set; }



    }


}