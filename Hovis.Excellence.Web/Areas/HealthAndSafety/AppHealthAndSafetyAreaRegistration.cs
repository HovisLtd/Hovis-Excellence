using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.HealthAndSafety
{
    public class AppHealthAndSafetyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "HealthAndSafety"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "HealthAndSafety_default",
                "health-and-safety/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}