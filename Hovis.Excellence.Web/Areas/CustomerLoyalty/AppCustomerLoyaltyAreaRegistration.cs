using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.CustomerLoyalty
{
    public class AppCustomerLoyaltyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get { return "CustomerLoyalty"; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CustomerLoyalty_default",
                "customer-loyalty/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                );
        }
    }
}