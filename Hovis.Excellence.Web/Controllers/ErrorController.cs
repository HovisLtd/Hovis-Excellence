using System.Web.Mvc;

namespace Hovis.Excellence.Web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        [Route("error")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("error/not-found")]
        public ActionResult Error404()
        {
            return View();
        }

        // GET: Error
        [Route("error/not-authorised")]
        public ActionResult Error403()
        {
            return View();
        }
    }
}