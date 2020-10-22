using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Repository;

namespace Web.Marcacion.Areas.Asistencia.Controllers
{
    public class T_AsistenciaController : Controller
    {
        // GET: Asistencia/T_Asistencia
        public ActionResult Index()
        {
            AsistenciaRepository asistenciaRepository= new AsistenciaRepository();
            var response = asistenciaRepository.Listar();

            return View(response);
        }
    }
}