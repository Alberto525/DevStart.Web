using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Marcacion.Context;
using Web.Marcacion.Models;
namespace Web.Marcacion.Negocios
{
    public class PersonaMetodos
    {
        public Dictionary<int, string> PMoneda = new Dictionary<int, string>();
        public Dictionary<int, string> PSexo = new Dictionary<int, string>();
        private StoreContext db = new StoreContext();

        public void InsertEmpleadoJornada(T_Usuario_Reg usuario) {

            T_EmpleadoJornada empjor = new T_EmpleadoJornada();

            empjor.ID_Usuario = usuario.ID_Usuario;
            empjor.ID_TipoJornada = usuario.ID_TipoJornada;
            empjor.ID_Empresa = 1;

            db.t_EmpleadoJornadas.Add(empjor);
                        db.SaveChanges();
                    
            }
        }
}