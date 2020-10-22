﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;
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

        [HttpPost]
        public ActionResult Index(string nombre, string apellido, string jornada)
        {
            try
            {
                if (nombre != "" || apellido != "" || jornada != "")
                {
                    EmpleadoJornadaRepository empleadoJornadaRepository = new EmpleadoJornadaRepository();
                    var response = empleadoJornadaRepository.ListarXNombre(nombre,apellido,jornada);
                    return View(response);

                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
           
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

        public ActionResult Edit(int id)
        {
            StoreContext db = new StoreContext();
            EmpleadoJornadaRepository empleadoJornadaRepository = new EmpleadoJornadaRepository();

            var response = empleadoJornadaRepository.ListarXid(id);

            ViewBag.Descripcion = new SelectList(db.t_TipoJornadas.Where(x => x.Estado), "ID_TipoJornada", "Descripcion");
            if (response == null) // si el perfil existe
            {
                return HttpNotFound();
            }
            return View(response);
        }

        [HttpPost]
        public ActionResult Edit(T_EmpleadoJornada t_EmpleadoJornada)
        {
            EmpleadoJornadaRepository empleadoJornadaRepository = new EmpleadoJornadaRepository();
            using (StoreContext db = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        int date = int.Parse(t_EmpleadoJornada.Descripcion);
                        empleadoJornadaRepository.ActualizarJornadaEmpleado(t_EmpleadoJornada.ID_EmpleadoJornada, date);
                        return RedirectToAction("Index");
                    }
                    return View(t_EmpleadoJornada);
                }
                catch (Exception)
                {
                    return View(t_EmpleadoJornada);
                }
            }

        }
        
        public ActionResult Salir()
        {
            return RedirectToAction("Index");
        }
    }
}