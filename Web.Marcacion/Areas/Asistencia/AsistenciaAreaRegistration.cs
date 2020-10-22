using System.Web.Mvc;

namespace Web.Marcacion.Areas.Asistencia
{
    public class AsistenciaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Asistencia";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Asistencia_default",
                "Asistencia/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}