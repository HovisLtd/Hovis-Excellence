using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.Environment
{
    public class AppEnvironmentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "Environment"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Environment_default",
                "environment/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}