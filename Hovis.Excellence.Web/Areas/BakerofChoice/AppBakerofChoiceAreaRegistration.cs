using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.BakerofChoice
{
    public class AppBakerofChoiceAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "BakerofChoice"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "BakerofChoice_default",
                "baker-of-choice/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}