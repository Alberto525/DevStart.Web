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

    }
}