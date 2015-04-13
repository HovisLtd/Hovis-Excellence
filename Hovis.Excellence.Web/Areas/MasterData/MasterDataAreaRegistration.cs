using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.MasterData
{
    public class MasterDataAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MasterData";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MasterData_default",
                "MasterData/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Hovis.Excellence.Web.Areas.MasterData.Controllers" }
            );
        }
    }
}