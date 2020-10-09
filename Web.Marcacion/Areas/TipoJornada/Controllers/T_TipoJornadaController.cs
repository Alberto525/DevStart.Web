using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;

namespace Web.Marcacion.Areas.TipoJornada.Controllers
{
    public class T_TipoJornadaController : Controller
    {
        // GET: TipoJornada/T_TipoJornada
        public ActionResult Index()
        {

            List<T_TipoJornada> lista = null;
            using (StoreContext db = new StoreContext())
            {
                lista = db.t_TipoJornadas.Where(x => x.Estado).ToList();
            }
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(T_TipoJornada t_TipoJornada)
        {
            t_TipoJornada.Estado = true;

            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.t_TipoJornadas.Add(t_TipoJornada);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(t_TipoJornada);
                }
                catch (Exception)
                {
                    return View();
                }
            }
        }
        public ActionResult Detail(int? id)
        {
            using (StoreContext db = new StoreContext())
            {
                var data = db.t_TipoJornadas.Find(id);
                if (data == null) // si el perfil existe
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        public ActionResult Edit(int? id)
        {
            using (StoreContext db = new StoreContext())
            {
                var data = db.t_TipoJornadas.Find(id);
                if (data == null) // si el perfil existe
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Edit(T_TipoJornada t_TipoJornada)
        {
            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(t_TipoJornada).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(t_TipoJornada);
                }
                catch (Exception)
                {
                    return View(t_TipoJornada);
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (StoreContext db = new StoreContext())
            {
                var data = db.t_TipoJornadas.Find(id);
                if (data == null) // si el perfil existe
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, T_TipoJornada t_TipoJornada)
        {
            using (StoreContext db = new StoreContext())
            {
                t_TipoJornada = db.t_TipoJornadas.Find(id);
                if (t_TipoJornada == null)
                {
                    return HttpNotFound();
                }
                db.t_TipoJornadas.Remove(t_TipoJornada);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Salir()
        {
            return RedirectToAction("Index");
        }


    }
}