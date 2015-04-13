using System.Web.Mvc;
using Hovis.Excellence.Web.Controllers;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    public class HomeController : HovisExcellenceController
    {
        // GET: MasterData/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}