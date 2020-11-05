using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;
using Web.Marcacion.Seguridad;
using Web.Marcacion.Repository;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace Web.Marcacion.Areas.Perfil.Controllers
{
    public class T_PerfilController : Controller
    {
        // GET: Perfil/T_Perfil


        public ActionResult Index()
        {
            return View();
        }


        //Metodo MARCOS JSON
        #region["Listar Perfil"]
        public JsonResult ListadoPerfil()
        {

            List<T_Perfil> lPerfil = null;
            using (StoreContext SC = new StoreContext())
            {
                lPerfil = SC.t_Perfils.Where(x => x.Estado).ToList();
            }
            object Listado = lPerfil;

            return Json(Listado, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult Create()
        {
            return View();
        }


        public string Crear(T_Perfil bePerfil)
        {
            bePerfil.Estado = true;
            string Respuesta = "";
            using (StoreContext SC = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        SC.t_Perfils.Add(bePerfil);
                        SC.SaveChanges();
                        Respuesta = "Correcto_";
                    }
                }
                catch (Exception ex)
                {
                    Respuesta = "Error_" + ex.Message;
                }
            }
            return Respuesta;
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
        public ActionResult Edit()
        {

            return View();
        }

        public JsonResult MostrarDataPerfil(string ID_Perfil)
        {
            object Data = null;
           
            using (StoreContext SC = new StoreContext())
            {

                var IDDescrypt = Seguridad.Seguridad.Desencriptar(ID_Perfil);
                int ID = int.Parse(IDDescrypt);
                var Entidades = SC.t_Perfils.Find(ID);
                Data = Entidades;
             
            }

       
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public string Editar(T_Perfil bePerfil) {
            string Respuesta = "";

            using (StoreContext SC = new StoreContext())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                     
                        SC.Entry(bePerfil).State = EntityState.Modified;
                        SC.SaveChanges();
                        Respuesta = "Correcto_";
                    }
                    else
                    {
                        Respuesta = "Error_" + "No se pudo hacer el guardado";
                    }
                }
                catch (Exception ex)
                {

                    Respuesta = "Error_" + ex.Message;
                }
            }


            return Respuesta;
        }
        
        [HttpGet]
        public ActionResult Delete(int? id)
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
        public ActionResult Delete(int id, T_Perfil t_Perfil)
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
                    List<PerfilExcel> listPerfil = new List<PerfilExcel>();
                    for(int row = 3; row <= range.Rows.Count; row++)
                    {
                        PerfilExcel p = new PerfilExcel();
                        p.Descripcion = ((Excel.Range)range.Cells[row, 1]).Text;
                        if (!string.IsNullOrEmpty(p.Descripcion))
                            listPerfil.Add(p);
                    }
                    workbook.Save();
                    workbook.Close(true);
                    application.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    if (listPerfil != null && listPerfil.Count > 0)
                    {
                        System.Web.HttpContext.Current.Session["ListPerfilExcel"] = listPerfil.ToList();
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
            ViewBag.ListPerfil = System.Web.HttpContext.Current.Session["ListPerfilExcel"] as List<PerfilExcel>;
            return View();
        }

        [HttpGet]
        public ActionResult InsertarExcel()
        {
            List<PerfilExcel> data = new List<PerfilExcel>();
            data = System.Web.HttpContext.Current.Session["ListPerfilExcel"] as List<PerfilExcel>;
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
            using (StoreContext db = new StoreContext())
            {
                var parametros = new SqlParameter("@lstPerfil", SqlDbType.Structured);
                parametros.Value = dt;
                parametros.TypeName = "dbo.DatosPerfil";

                db.Database.ExecuteSqlCommand("exec spInsertarExcelPerfil @lstPerfil", parametros);
                return RedirectToAction("Index");
            }
        }


    }
}