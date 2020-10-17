using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;

namespace Web.Marcacion.Controllers
{
    public class HomeController : Controller
    {
        StoreContext db = new StoreContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sample app for integrating MDBootstrap into ASP.NET MVC.";

            return View();
        }
        [HttpPost]
        public ActionResult Index(string us, string pw)
        {
            var user = db.t_Usuarios.FirstOrDefault(x => x.Usuario == us && x.Clave == pw);
            if (user != null)
            {
                //var modelPerson = db.T_Persona.FirstOrDefault(x => x.ID_Persona == user.ID_Persona);
                return RedirectToAction("Index", "T_Persona", new { area = "Persona" });
            }

            return View();
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