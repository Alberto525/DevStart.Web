using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;

namespace Web.Marcacion.Areas.Empresa.Controllers
{
    public class EmpresaController : Controller
    {
        // GET: Empresa/Empresa
        public ActionResult Index()
        {
            List<T_Empresa> lista = null;
            using (StoreContext db = new StoreContext())
            {
                lista = db.t_empresas.Where(x => x.Estado).ToList();
            }
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(T_Empresa t_empresa)
        {
            t_empresa.Estado = true;

            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.t_empresas.Add(t_empresa);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(t_empresa);
                }
                catch (Exception)
                {
                    return View();
                }
            }
        }
        public ActionResult Detail(string ID)
        {
            using (StoreContext db = new StoreContext())
            {
                var NuevoID = Seguridad.Seguridad.Desencriptar(ID);
                var data = db.t_empresas.Find(int.Parse(NuevoID));
                if (data == null)
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
                var data = db.t_empresas.Find(id);
                if (data == null)
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Edit(T_Empresa t_empresa)
        {
            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(t_empresa).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(t_empresa);
                }
                catch (Exception)
                {
                    return View(t_empresa);
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            using (StoreContext db = new StoreContext())
            {
                var data = db.t_empresas.Find(id);
                if (data == null)
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id, T_Empresa t_empresa)
        {
            using (StoreContext db = new StoreContext())
            {
                t_empresa = db.t_empresas.Find(id);
                if (t_empresa == null)
                {
                    return HttpNotFound();
                }
                db.t_empresas.Remove(t_empresa);
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