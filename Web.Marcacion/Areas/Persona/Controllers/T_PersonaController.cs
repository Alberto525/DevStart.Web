using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;

namespace Web.Marcacion.Areas.Persona.Controllers
{
    public class T_PersonaController : Controller
    {
        // GET: Perfil/T_Perfil
        private StoreContext db = new StoreContext();


        public ActionResult Index()
        {
            List<T_Persona> lista = null;
            using (StoreContext db = new StoreContext())
            {
                lista = db.T_Personas.Where(x => x.Estado).ToList();
            }
            return View(lista);
        }
        public ActionResult Create()
        {


           ViewBag.ID_TipoDocumento = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion");
       //    ViewBag.ID_Empresa = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion");
            return View();


           

        }
       
        [HttpPost]
        public ActionResult Create(T_Persona persona)
        {
            persona.Estado = true;
            persona.FechaCreacion = DateTime.Now;
            persona.UsuarioCreacion = 2;
         
            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.T_Personas.Add(persona);
                        db.SaveChanges();
                        return RedirectToAction("CreateUsuario", new { id = persona.ID_Persona });
                    }
                    ViewBag.ID_TipoDocumento = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion");
                //    ViewBag.ID_Empresa = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion");
                    return View(persona);
                }
                catch (Exception)
                {
                    return View();
                }
            }
        }

        public ActionResult CreateUsuario(int id)
        {
            T_Usuario usu = new T_Usuario();
            usu.ID_Persona = id;
           // usu.ID_Perfil = 2;

            return View(usu);
        }

        [HttpPost]
        public ActionResult CreateUsuario(T_Usuario usu)
        {

            usu.ID_Perfil = 1;
            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.t_Usuarios.Add(usu);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(usu);
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
                var data = db.T_Personas.Find(id);
                if (data == null) // si el perfil existe
                {
                    return HttpNotFound();
                }

                ViewBag.ID_TipoDocumento = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion");
                return View(data);
            }
        }
        public ActionResult Edit(int? id)
        {
            using (StoreContext db = new StoreContext())
            {
                var data = db.T_Personas.Find(id);
                if (data == null) // si el perfil existe
                {
                    return HttpNotFound();
                }

                ViewBag.ID_TipoDocumento = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion", data.ID_TipoDocumento);
            //    ViewBag.ID_Empresa = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion", data.ID_Empresa);
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Edit(T_Persona persona)
        {


            using (StoreContext db = new StoreContext())
            {
                try
                {
                    persona.FechaModificacion = DateTime.Now;
                    persona.UsuarioModificacion = 2;
                    if (ModelState.IsValid)
                    {
                        db.Entry(persona).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    ViewBag.ID_TipoDocumento = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion",persona.ID_TipoDocumento);
                //    ViewBag.ID_Empresa = new SelectList(db.T_TipoDocumentos.ToList(), "ID_TipoDocumento", "Descripcion", persona.ID_Empresa);



                    return View(persona);
            }
                catch (Exception)
                {
                    return HttpNotFound();
                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int? id)
          {
            using (StoreContext db = new StoreContext())
            {
            var data =  db.T_Personas.Find(id);
                if (data == null) // si el perfil existe
                {
                    return HttpNotFound();
                }
                return View(data);
            }
        }
        [HttpPost]
        public ActionResult Delete(int id ,T_Persona persona)
        {
            using (StoreContext db = new StoreContext())
            {
                persona = db.T_Personas.Find(id);
                if (persona == null)
                {
                    return HttpNotFound();
                }
                db.T_Personas.Remove(persona);
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