using System.Web.Mvc;

namespace Web.Marcacion.Areas.TipoJornada
{
    public class TipoJornadaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TipoJornada";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TipoJornada_default",
                "TipoJornada/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}