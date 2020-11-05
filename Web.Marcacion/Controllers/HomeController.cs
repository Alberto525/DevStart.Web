using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;
using Web.Marcacion.Seguridad;

using Web.Marcacion.Ayuda;

namespace Web.Marcacion.Controllers
{
    public class HomeController : Controller
    {
        StoreContext db = new StoreContext();

        public ActionResult Index()
        {
            //string DomainName = HttpContext.Request.Url.Host;
            //ViewBag.localhost = DomainName;
            return View();
        }


        public string Ingresar(string us, string pw)
        {
            string Respuesta = "";
            var user = db.t_Usuarios.FirstOrDefault(x => x.Usuario == us && x.Clave == pw);
            if (user != null)
            {
                SessionHelp.AddUserToSession(us);
                Respuesta = "Correcto_" + user;
   

            }
            else
            {
                Respuesta = "Incorrecto_" + user;
            }
            return Respuesta;
        }




        public ActionResult About()
        {
            ViewBag.Message = "Sample app for integrating MDBootstrap into ASP.NET MVC.";

            return View();
        }

        [HttpPost]
        public string Encripta(string valor)
        {
            return Seguridad.Seguridad.Encriptar(valor);
        }


        public ActionResult TerminoDeSesion()
        {

            return View();


        }

        [HttpPost]
        public string Desencripta(string valor)
        {
            return Seguridad.Seguridad.Desencriptar(valor);
        }


      

        public ActionResult Contact()
        {
            ViewBag.Message = "You can contact me through MDBootstrap or GitHub.";

            return View();
        }

        public ActionResult Containers()
        {
            ViewBag.Title = "Containers.";

            return View();
        }

        public ActionResult Grid()
        {
            ViewBag.Title = "Grid.";

            return View();
        }

        public ActionResult Buttons()
        {
            ViewBag.Title = "Buttons.";

            return View();
        }
    }
}