using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;
using Web.Marcacion.Repository;
using Excel = Microsoft.Office.Interop.Excel;

namespace Web.Marcacion.Areas.Persona.Controllers
{
    public class T_PersonaController : Controller
    {
        // GET: Perfil/T_Perfil
        //private StoreContext db = new StoreContext();
  //      PersonaMetodos metodos = new PersonaMetodos();

        public ActionResult Index()
        {
            List<T_Persona> lista = null;
            using (StoreContext db = new StoreContext())
            {
                lista = db.T_Personas.Where(x => x.Estado).ToList();
            }
            return View(lista);
          //  return RedirectToAction("CreateUsuario");
        }
        public ActionResult Create()
        {

            StoreContext db = new StoreContext();
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
            StoreContext db = new StoreContext();
            T_Persona perso = db.T_Personas.Find(id);

         //   T_Usuario usuario = new T_Usuario();
            T_Usuario_Reg usu = new T_Usuario_Reg();

            usu.ID_Persona = id;
            usu.Estado = true;
            usu.RazonSocial = perso.Apellido + " " + perso.Nombre;
            usu.NumeroDocumento = perso.NumeroDocumento;
            usu.Correo = perso.Correo;
            // ViewBag.Correo = perso.Correo;
            ViewBag.ID_Perfil = new SelectList(db.t_Perfils.ToList(), "ID_Perfil", "Descripcion");
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
                    usuario.Usuario = usu.Correo;
                    usuario.Estado = usu.Estado;
                    usuario.Clave = usu.Clave;

                    if (ModelState.IsValid)
                    {
                        db.t_Usuarios.Add(usuario);
                      db.SaveChanges();
                        empleadoJornadaRepository.iNSERTARJornadaEmpleado(1, usuario.ID_Usuario, usu.ID_TipoJornada);
                        return RedirectToAction("Index");
                    }
                    ViewBag.ID_Perfil = new SelectList(db.t_Perfils.ToList(), "ID_Perfil", "Descripcion", usu.ID_Perfil);
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

                var query = db.T_Personas.Join(db.T_Cargos, p => p.ID_Cargo, c => c.ID_Cargo, (p, c) => new { c.Descripcion , p.ID_Persona});
                var result = query.Where(x => x.ID_Persona == id).Select(x => x.Descripcion).FirstOrDefault();

                // var idcargo = db.T_Personas.Where(x => x.ID_Persona == id).Select(x => x.ID_Cargo);
                // int codigo = Convert.ToInt32(idcargo);
                ViewBag.Cargo = result;
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

        //IMPORTAR EXCEL A LA BD

        public ActionResult ImportarExcel()
        {
            return View();
        }


        [HttpPost]
        public ActionResult ImportarExcel(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Seleccione un archivo de Excel !";
                return View();
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    StoreContext db = new StoreContext();

                    string path = Server.MapPath("~/UploadExcel/" + excelfile.FileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    excelfile.SaveAs(path);
                    //LEER DATOS DEL ARCHIVO EXCEL
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    List<PersonaExcel> listPersona = new List<PersonaExcel>();
                    for (int row = 3; row <= range.Rows.Count; row++)
                    {
                        PersonaExcel p = new PersonaExcel();
                        p.Nombre = ((Excel.Range)range.Cells[row, 1]).Text;
                        p.Apellido = ((Excel.Range)range.Cells[row, 2]).Text;
                        p.ID_TipoDocumento = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                        p.NumeroDocumento = ((Excel.Range)range.Cells[row, 4]).Text;
                        p.Sexo = ((Excel.Range)range.Cells[row, 5]).Text;
                        p.Correo = ((Excel.Range)range.Cells[row, 6]).Text;
                        p.Movil = ((Excel.Range)range.Cells[row, 7]).Text;
                        p.Direccion = ((Excel.Range)range.Cells[row, 8]).Text;
                        p.FechaNacimiento =  DateTime.Parse(((Excel.Range)range.Cells[row, 9]).Text);
                        p.ID_Cargo = int.Parse(((Excel.Range)range.Cells[row, 10]).Text);
                        //  if (!string.IsNullOrEmpty(p.Descripcion))
                        listPersona.Add(p);
                    }
                    workbook.Save();
                    workbook.Close(true);
                    application.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    if (listPersona != null && listPersona.Count > 0)
                    {
                        System.Web.HttpContext.Current.Session["ListPersonaExcel"] = listPersona.ToList();
                        return RedirectToAction("Success");
                    }
                    return View();
                }
                else
                {
                    ViewBag.Error = "El tipo de archivo es incorrecto ! <br>";
                    return View();
                }
            }
        }

        public ActionResult Success()
        {
            ViewBag.ListPersona = System.Web.HttpContext.Current.Session["ListPersonaExcel"] as List<PersonaExcel>;
            return View();
        }

        [HttpGet]
        public ActionResult InsertarExcel()
        {
            List<PersonaExcel> data = new List<PersonaExcel>();
            data = System.Web.HttpContext.Current.Session["ListPersonaExcel"] as List<PersonaExcel>;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            using (StoreContext db = new StoreContext())
            {
                var parametros = new SqlParameter("@lstPersona", SqlDbType.Structured);
                parametros.Value = dt;
                parametros.TypeName = "dbo.DatosPersona";

                db.Database.ExecuteSqlCommand("exec spInsertarExcelPersona @lstPersona", parametros);
                return RedirectToAction("Index");
            }
        }


    }
}