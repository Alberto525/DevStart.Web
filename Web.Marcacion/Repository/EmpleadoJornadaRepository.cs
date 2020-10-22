using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Marcacion.Context;
using Web.Marcacion.Models;

namespace Web.Marcacion.Repository
{
    public class EmpleadoJornadaRepository
    {
        public List<T_EmpleadoJornada> Lista()
        {
            using (var db = new StoreContext())
            {
                return db.Database.SqlQuery<T_EmpleadoJornada>("ListaEmpleadoJornada").ToList();
            }
        }

        public T_EmpleadoJornada ListarXid(int id)
        {
            using (var db = new StoreContext())
            {
                return db.Database.SqlQuery<T_EmpleadoJornada>("ListaxIDEmpleadoJornada @p0", id).SingleOrDefault();
            }
        }

        public List<T_EmpleadoJornada> ListarXNombre(string nombre, string apellido, string jornada)
        {
            using (var db = new StoreContext())
            {
                return db.Database.SqlQuery<T_EmpleadoJornada>("sp_ListarXNombre @p0,@p1,@p2", nombre,apellido,jornada).ToList();
            }
        }

        public void ActualizarJornadaEmpleado(int id, int idtipojornada)
        {
            using (var db = new StoreContext())
            {
                db.Database.ExecuteSqlCommand("UpdateJornadaEmpleado @p0,@p1", id, idtipojornada);
            }


        }
        public void iNSERTARJornadaEmpleado(int idempresa, int id_usuario, int id_tipojornada)
        {
            using (var db = new StoreContext())
            {
                db.Database.ExecuteSqlCommand("sp_InsertJornadaEmpleado @p0,@p1,@p2", idempresa, id_usuario, id_tipojornada);
            }


        }

    }
}