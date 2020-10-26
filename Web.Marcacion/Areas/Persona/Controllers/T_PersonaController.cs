using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;
using Web.Marcacion.Repository;

namespace Web.Marcacion.Areas.Persona.Controllers
{
    public class T_PersonaController : Controller
    {
        // GET: Perfil/T_Perfil
        private StoreContext db = new StoreContext();
  //      PersonaMetodos metodos = new PersonaMetodos();

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
            ViewBag.ID_Cargo = new SelectList(db.T_Cargos.ToList(), "ID_Cargo", "Descripcion");

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
                    ViewBag.ID_Cargo = new SelectList(db.T_Cargos.ToList(), "ID_Cargo", "Descripcion");
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
            T_Persona perso = db.T_Personas.Find(id);

         //   T_Usuario usuario = new T_Usuario();
            T_Usuario_Reg usu = new T_Usuario_Reg();

            usu.ID_Persona = id;
            usu.Estado = true;
            usu.RazonSocial = perso.Apellido + " " + perso.Nombre;
            usu.NumeroDocumento = perso.NumeroDocumento ;
            ViewBag.ID_Perfil = new SelectList(db.t_Perfils.ToList(), "ID_Perfil", "Perfil");
            ViewBag.ID_TipoJornada = new SelectList(db.t_TipoJornadas.ToList(), "ID_TipoJornada", "Descripcion");
            // usu.ID_Perfil = 2;

            return View(usu);
        }

        [HttpPost]
        public ActionResult CreateUsuario(T_Usuario_Reg usu)
        {

            EmpleadoJornadaRepository empleadoJornadaRepository = new EmpleadoJornadaRepository();
            using (StoreContext db = new StoreContext())
            {
                try
                {

                    T_Usuario usuario = new T_Usuario();
                    usuario.ID_Perfil = usu.ID_Perfil;
                    usuario.ID_Persona = usu.ID_Persona;
                    usuario.Usuario = usu.Usuario;
                    usuario.Estado = usu.Estado;
                    usuario.Clave = usu.Clave;

                    if (ModelState.IsValid)
                    {
                        db.t_Usuarios.Add(usuario);
                      db.SaveChanges();
                        empleadoJornadaRepository.iNSERTARJornadaEmpleado(1, usuario.ID_Usuario, usu.ID_TipoJornada);
                        return RedirectToAction("Index");
                    }
                    ViewBag.ID_Perfil = new SelectList(db.t_Perfils.ToList(), "ID_Perfil", "Perfil", usu.ID_Perfil);
                    ViewBag.ID_TipoJornada = new SelectList(db.t_TipoJornadas.ToList(), "ID_TipoJornada", "Descripcion", usu.ID_TipoJornada);

                    return View(usu);
                }
                catch (Exception)
                {
                    return View(usu);
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

                var query = db.T_Personas.Join(db.T_Cargos, p => p.ID_Cargo, c => c.ID_Cargo, (p, c) => new { c.Descripcion});
                

                // var idcargo = db.T_Personas.Where(x => x.ID_Persona == id).Select(x => x.ID_Cargo);
                // int codigo = Convert.ToInt32(idcargo);
                ViewBag.Cargo = query.ToString();
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
                ViewBag.ID_Cargo = new SelectList(db.T_Cargos.ToList(), "ID_Cargo", "Descripcion",data.ID_Cargo);
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
                    ViewBag.ID_Cargo = new SelectList(db.T_Cargos.ToList(), "ID_Cargo", "Descripcion", persona.ID_Cargo);



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
        public ActionResult Delete(int id)
        {
            using (StoreContext db = new StoreContext())
            {
            T_Persona   persona = db.T_Personas.Find(id);
                if (persona == null)
                {
                    return HttpNotFound();
                }
                persona.Estado = false;
                db.Entry(persona).State = EntityState.Modified;
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