using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Web.Marcacion.Context;
using Web.Marcacion.Models;

namespace Web.Marcacion.Repository
{
    public class Metodos
    {
        
        public List<String> VariosMsj { get; set; }

        public List<string> MsjeUno = new List<string>();
        public List<string> MsjeErr = new List<string>();
        public bool Ok { get; set; }

        public bool ValidarImportación(ImportarEx i)
        {
            bool Oki = true;
             
            if (!EsCorrecto(i.file))//Revisa que sea exel
            {
                Oki = false;
                VariosMsj = MsjeUno.ToList();
            }
            else
            {
                if (GrabarFile(i.file))//Mueve el documento a carepta migrados
                {

                    int t = 3;
                    if (Importar(i.file.FileName, t))//Verifica que tengo las files y columnas correctas
                    {

                        ImportarPerfil(i.file.FileName);///Verifica Campos he inserta
                        if (!Ok)
                        {
                            Oki = false;
                            VariosMsj.Add("Error: En el archivo cargado. Por favor revise el log descargado");
                        }
                    }
                    else
                    {
                        Oki = false;
                        VariosMsj = MsjeUno.ToList();
                    }
                }

            }
            return (Oki);
        }


        public bool EsCorrecto(HttpPostedFileBase f)
        {
            bool Ok = false;

            if (f != null && f.ContentLength > 0)
            {
                if (f.FileName.EndsWith(".xls") || f.FileName.EndsWith(".XLS") || f.FileName.EndsWith(".xlsx") || f.FileName.EndsWith(".XLSX"))
                {
                    Ok = true;
                }
                else
                    MsjeUno.Add("Error: La extensión del archivo debe ser .xls o .xlsx");
            }
            else
                MsjeUno.Add("Error: Debe ingresar un archivo.");


            return (Ok);
        }

        public bool GrabarFile(HttpPostedFileBase f)
        {
            try
            {
                string path = HostingEnvironment.MapPath("~");
                string FileName = System.IO.Path.GetFileName(f.FileName);
                string destino = path + @"\UploadExcel\";
                if (!(Directory.Exists(destino)))
                    Directory.CreateDirectory(destino);

                f.SaveAs(Path.Combine(destino, FileName));

                return (true);
            }
            catch (RetryLimitExceededException)
            {
                return (false);
            }
        }

        public bool Importar(string n, int t)
        {
            bool Ok = true;

            DataTable de = ImportarExcel(n);

            if (t != 0)
            {
                if (de.Columns.Count != t)
                {
                    MsjeUno.Add("Error: Se esperaba " + t + " columnas y solo se encontraron " + de.Columns.Count + " Columnas");
                    Ok = false;
                }
            }

            if (de.Rows.Count <= 1)
            {
                MsjeUno.Add("Error: El documento no tiene Datos");
                Ok = false;
            }

            return (Ok);

        }

        public DataTable ImportarExcel(string n)
        {
            string path = HostingEnvironment.MapPath("~");
            string fileName = System.IO.Path.GetFileName(n);
            string destino = path + @"\UploadExcel\";
            string archivo = destino + fileName;

            DataTable dt = new DataTable();

            using (XLWorkbook wb = new XLWorkbook(archivo))
            {
                IXLWorksheet wsh = wb.Worksheet(1);

                bool firsRw = true;

                foreach (IXLRow rw in wsh.Rows())
                {
                    if (firsRw)
                    {
                        foreach (IXLCell cll in rw.Cells())
                        {
                            dt.Columns.Add(cll.Value.ToString());
                        }
                        firsRw = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cll in rw.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cll.Value.ToString();
                            i++;
                        }
                    }
                }
            }

            return (dt);
        }

        public int GetPerfil(string c)
        {
            StoreContext db = new StoreContext();
            int lori = 0;

            lori = (from p in db.t_Perfils
                    where p.ID_Perfil.Equals(c)
                    select p.ID_Perfil).FirstOrDefault();
            return (lori);

        }

        public void ImportarPerfil(string n)
        {
            string path = HostingEnvironment.MapPath("~");
            string fileName = System.IO.Path.GetFileName(n);
            string destino = path + @"\Documentos\Migrados\";
            string archivo = destino + fileName;

            #region DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("ID_Perfil", typeof(String)); //1
            dt.Columns.Add("Perfil", typeof(String)); //2
            dt.Columns.Add("Estado", typeof(String)); //3

            dt.Columns.Add("EsNuevo", typeof(String)); //4  
            dt.Columns.Add("PerfilID", typeof(int)); //5
            #endregion

            using (XLWorkbook wb = new XLWorkbook(archivo))
            {
                IXLWorksheet wsh = wb.Worksheet(1);

                bool firsRw = true;

                foreach (IXLRow rw in wsh.Rows(wsh.FirstRowUsed().RowNumber(), wsh.LastRowUsed().RowNumber()))
                {
                    if (firsRw)
                    {
                        firsRw = false;
                    }
                    else
                    {
                        dt.Rows.Add();
                        int i = 0;
                        foreach (IXLCell cll in rw.Cells())
                        {
                            dt.Rows[dt.Rows.Count - 1][i] = cll.Value.ToString();
                            i++;
                        }
                    }
                }
            }
            bool paso = true;
            string cFill = "-";
            if (dt.Rows.Count > 0)
            {
                int fila = 2;
                foreach (DataRow r in dt.Rows)
                {
                    {
                        int col = 1;
                        foreach (DataColumn c in dt.Columns)
                        {
                            if (c.Caption == "ID_Perfil")
                            {
                                string o = r["ID_Perfil"].ToString();
                                if (o.Length != 0 && o.Length < 30 && o.Length > 1)
                                {
                                    int i = GetPerfil(o);
                                    if (i == 0)
                                    {
                                        r["EsNuevo"] = "S";
                                    }
                                    else
                                    {
                                        r["EsNuevo"] = "N";
                                        r["PerfilID"] = i;
                                    }
                                }
                                else
                                {
                                    MsjeErr.Add("Fila:" + fila.ToString() + "/Col:" + col.ToString() + "- Verifique que el Campo Codigo no este vacio  ");
                                    paso = false;
                                }

                            }
                            if (c.Caption == "Perfil")
                            {
                                string nom = r[c].ToString();

                                if (String.IsNullOrEmpty(nom))
                                {
                                    MsjeErr.Add("Fila:" + fila.ToString() + "/Col:" + col.ToString() + "- Debe ingresar la descripcion del almacén");
                                    paso = false;
                                }

                            }

                            if (c.Caption == "Estado")
                            {
                                string o = r[c].ToString();

                                if (o.Length == 0)
                                {
                                    MsjeErr.Add("Fila:" + fila.ToString() + "/Col:" + col.ToString() + "- Debe indicar el Estado del almacen S/N");
                                    paso = false;
                                }

                            }
                            col++;
                        }
                        fila++;
                    }
                }

                if (paso)
                {
                    Ok = true;
                    StoreContext db = new StoreContext();
                    foreach (DataRow r in dt.Rows)
                    {
                        string tipo = r["EsNuevo"].ToString();
                        T_Perfil cab = new T_Perfil();
                        if (tipo.Equals("S"))
                        {
                            cab.ID_Perfil =int.Parse(r["ID_Perfil"].ToString());
                        }
                        else
                        {
                            cab = db.t_Perfils.Find(Int32.Parse(r["PerfilID"].ToString()));
                        }
                        if (!String.IsNullOrEmpty(r["Perfil"].ToString()) && !r["Perfil"].ToString().Trim().Equals(cFill))
                        {
                            cab.Perfil = r["Perfil"].ToString();
                        }
                        if (!String.IsNullOrEmpty(r["Estado"].ToString()) && !r["Estado"].ToString().Trim().Equals(cFill))
                        {
                            cab.Estado = r["Estado"].ToString().Equals("N") ? false : true;
                        }

                        if (tipo.Equals("S"))
                            db.t_Perfils.Add(cab);
                        else
                            db.Entry(cab).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    string fileOut = "CPL_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"; ;
                    GenerarErrorTxt(fileOut, MsjeErr);
                    Ok = false;
                }
            }
        }


        public void GenerarErrorTxt(string file, List<string> err)
        {
            string directorio = HostingEnvironment.MapPath("~") + @"\Documentos\Errors\";
            if (!(Directory.Exists(directorio)))
                Directory.CreateDirectory(directorio);
            string ruta = directorio + file;
            using (StreamWriter p = new System.IO.StreamWriter(ruta))
            {
                foreach (var o in err)
                {
                    string reg = o;
                    p.WriteLine(reg);
                }
                p.Flush();
                p.Close();
            }
            BajarFiles(ruta, file);
        }

        public void BajarFiles(string ruta, string filename)
        {
            string FileName = filename;
            string FileRuta = @ruta;

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "application/x-unknown";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(FileRuta);
            response.Flush();
            response.End();
        }


    }
}