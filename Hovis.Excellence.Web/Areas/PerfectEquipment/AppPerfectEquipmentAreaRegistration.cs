using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.PerfectEquipment
{
    public class AppPerfectEquipmentAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "PerfectEquipment"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PerfectEquipment_default",
                "perfect-equipment/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}