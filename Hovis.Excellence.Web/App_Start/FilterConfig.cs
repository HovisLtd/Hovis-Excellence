using System.Web.Mvc;
using AuthorizeAttribute = Hovis.Excellence.Web.AuthorizeAttribute;

namespace Hovis.Excellence.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //this makes the whole site require authentication
            filters.Add(new AuthorizeAttribute());
        }
    }
}