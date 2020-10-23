using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Marcacion.Context;
using Web.Marcacion.Models;

namespace Web.Marcacion.Repository
{
    public class AsistenciaRepository
    {
        public List<T_Asistencia> Listar()
        {
            using (var db = new StoreContext())
            {
                return db.Database.SqlQuery<T_Asistencia>("sp_ListarAsistencia").ToList();
            }
        }
    }
}