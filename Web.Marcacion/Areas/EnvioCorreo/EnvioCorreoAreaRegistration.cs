using System.Web.Mvc;

namespace Web.Marcacion.Areas.EnvioCorreo
{
    public class EnvioCorreoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EnvioCorreo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EnvioCorreo_default",
                "EnvioCorreo/{controller}/{action}/{id}",
                new { action = "EnvioCorreo", id = UrlParameter.Optional }
            );
        }
    }
}