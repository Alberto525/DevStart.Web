using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Repository;

namespace Web.Marcacion.Areas.EmpleadoJornada.Controllers
{
    public class T_EmpleadoJornadaController : Controller
    {
        // GET: EmpleadoJornada/T_EmpleadoJornada
        public ActionResult Index()
        {
            EmpleadoJornadaRepository empleadoJornadaRepository = new EmpleadoJornadaRepository();

            var response = empleadoJornadaRepository.Lista();

            return View(response);
        }
        
        public ActionResult Detail(int id)
        {
            EmpleadoJornadaRepository empleadoJornadaRepository = new EmpleadoJornadaRepository();
            
                var response = empleadoJornadaRepository.ListarXid(id);
                if (response == null) // si el perfil existe
                {
                    return HttpNotFound();
                }
                return View(response);
            
        }

        public ActionResult Salir()
        {
            return RedirectToAction("Index");
        }
    }
}