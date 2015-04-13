using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.OperationsExcellence
{
    public class AppOperationsExcellenceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "OperationsExcellence"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "OperationsExcellence_default",
                "operations-excellence/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}