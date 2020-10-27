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
        // GET: EnvioCorreo/EnvioCorreo
        public ActionResult EnvioCorreo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EnvioCorreo (E_Correo model)
        {
            try
            {
                var mensaje = new MailMessage();
                mensaje.Subject = model.Asunto;
                mensaje.Body = model.Mensaje;
                mensaje.To.Add(model.Destino);

                mensaje.IsBodyHtml = true;

                var smtp = new SmtpClient();
                smtp.Send(mensaje);
                ViewBag.Mensaje = "Mensaje enviado correctamente";


                
            }
            catch (Exception ex)
            {

                ViewBag.Error = ex.Message;
            }

            return View();

        }
    }
}