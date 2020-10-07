using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;

namespace Web.Marcacion.Areas.Perfil.Controllers
{
    public class T_PerfilController : Controller
    {
        // GET: Perfil/T_Perfil
        public ActionResult Index()
        {
            List<T_Perfil> lista = null;
            using (StoreContext db = new StoreContext())
            {
                lista = db.t_Perfils.Where(x => x.Estado).ToList();
            }
            return View(lista);
        }
        public ActionResult Create()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Create(T_Perfil t_Perfil)
        {
            t_Perfil.Estado = true;

            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.t_Perfils.Add(t_Perfil);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(t_Perfil);
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
                var data = db.t_Perfils.Find(id);
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
                var data = db.t_Perfils.Find(id);
                if (data == null) // si el perfil existe
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Edit(T_Perfil t_Perfil)
        {
            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(t_Perfil).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                return View(t_Perfil);
            }
                catch (Exception)
                {
                    return View(t_Perfil);
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int? id)
          {
            using (StoreContext db = new StoreContext())
            {
            var data =  db.t_Perfils.Find(id);
                if (data == null) // si el perfil existe
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id ,T_Perfil t_Perfil)
        {
            using (StoreContext db = new StoreContext())
            {
                t_Perfil = db.t_Perfils.Find(id);
                if (t_Perfil == null)
                {
                    return HttpNotFound();
                }
                db.t_Perfils.Remove(t_Perfil);
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