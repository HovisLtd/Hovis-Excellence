using System.Web.Mvc;

namespace Hovis.Excellence.Web
{
    //created this to overcome a failing of the built in attribute that doesn't properly redirect 403
    //see here for more info http://stackoverflow.com/a/2578795/131809
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var result = new HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);

                //for some reason it wasn't getting handled by the custom error module in web.config?
                if (result.StatusCode == 403)
                {
                    filterContext.Result = new RedirectResult("/error/not-authorised");
                    return;
                }

                filterContext.Result = result;
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}