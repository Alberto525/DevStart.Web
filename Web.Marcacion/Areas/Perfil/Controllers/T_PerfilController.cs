using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Context;
using Web.Marcacion.Models;
using Web.Marcacion.Repository;
using Excel = Microsoft.Office.Interop.Excel;

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

        public ActionResult ImportarExcel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ImportarExcel(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Please select  a excel file";
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
                    //-----
                    //-----


                    //Read data from excel file
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;
                    List<T_Perfil> listPerfil = new List<T_Perfil>();
                    for(int row = 3; row <= range.Rows.Count; row++)
                    {
                        T_Perfil p = new T_Perfil();
                        p.ID_Perfil = int.Parse(((Excel.Range)range.Cells[row, 1]).Text);
                        p.Descripcion = ((Excel.Range)range.Cells[row, 2]).Text;
                        listPerfil.Add(p);
                    }
                    ViewBag.ListPerfil = listPerfil.ToList();
                    return RedirectToAction("Success");
                }
                else
                {
                    ViewBag.Error = "File type is incorrect<br>";
                    return View();
                }
            }
        }


        public ActionResult Importar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Importar(ImportarEx file)
        {
            Metodos obj = new Metodos();

            if (!obj.ValidarImportación(file))
            {
                foreach (var m in obj.VariosMsj)
                {
                    ModelState.AddModelError("", m);
                }
                return View(file);
            }
            else
                return RedirectToAction("Index");
            
        }

        public ActionResult Success()
        {
            return View();
        }

    }
}