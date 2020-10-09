using System.Web.Mvc;

namespace Web.Marcacion.Areas.EmpleadoJornada
{
    public class EmpleadoJornadaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EmpleadoJornada";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EmpleadoJornada_default",
                "EmpleadoJornada/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}