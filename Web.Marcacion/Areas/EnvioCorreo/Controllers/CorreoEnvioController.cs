using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Web.Marcacion.Models;

namespace Web.Marcacion.Areas.EnvioCorreo.Controllers
{
    public class CorreoEnvioController : Controller
    {
       
        public ActionResult EnvioCorreo()
        {
            return View();
        }


        public string Enviar(E_Correo beCorreo)
        {
            string Respuesta = "";
            try
            {
                var Mensaje = new MailMessage();
                Mensaje.Subject = beCorreo.Asunto;
                Mensaje.Body = beCorreo.Mensaje;
                Mensaje.To.Add(beCorreo.Destino);

                Mensaje.IsBodyHtml = true;

                var SMTP = new SmtpClient();
                SMTP.Send(Mensaje);
                Respuesta =  "Correcto_" + "Mensaje enviado correctamente";
            }
            catch (Exception ex)
            {
                Respuesta = "Error_" + ex.Message;
            }
            return Respuesta;

        }


        public ActionResult Salir()
        {
            return RedirectToAction("Index");
        }
    }
}