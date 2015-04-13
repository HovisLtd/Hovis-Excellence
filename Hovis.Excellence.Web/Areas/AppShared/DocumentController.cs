using Hovis.Excellence.Web.Controllers;
using System.Linq;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.AppShared
{
    public abstract class DocumentController : HovisExcellenceController
    {
        public virtual ActionResult Index(int id)
        {
            var document = _db.Documents.SingleOrDefault(x => x.Id.Equals(id));

            if (document == null)
                return new HttpNotFoundResult();

            return View(document);
        }
    }
}