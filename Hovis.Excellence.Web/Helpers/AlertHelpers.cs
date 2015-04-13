using System;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Helpers
{
    public static class AlertHelpers
    {
        public static MvcHtmlString Alerts(this HtmlHelper helper)
        {
            if (helper.ViewContext.TempData.ContainsKey("success"))
                return MvcHtmlString.Create(GenerateMessage("success", helper.ViewContext.TempData["success"].ToString()));

            if (helper.ViewContext.TempData.ContainsKey("error"))
                return MvcHtmlString.Create(GenerateMessage("danger", helper.ViewContext.TempData["error"].ToString()));

            if (helper.ViewContext.TempData.ContainsKey("warning"))
                return MvcHtmlString.Create(GenerateMessage("warning", helper.ViewContext.TempData["warning"].ToString()));

            return null;
        }

        private static string GenerateMessage(string type, string message)
        {
            var str = String.Format(@"<div class=""alert alert-{0}"">", type);
            str += message;
            str += "</div>";

            return str;
        }
    }
}