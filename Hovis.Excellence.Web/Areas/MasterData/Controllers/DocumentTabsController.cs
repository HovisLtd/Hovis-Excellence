using Hovis.Excellence.Web.Controllers;
using Hovis.Excellence.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DocumentTabsController : HovisExcellenceController
    {
        public ActionResult Index()
        {
            var documentTabs = _db.DocumentTabs
                .OrderBy(x => x.Name)
                .ToList();

            return View(documentTabs);
        }

        public ActionResult New()
        {
            return View("NewOrEdit", new DocumentTabs());
        }

        public async Task<ActionResult> Edit(int id)
        {
            var documentTabs = await _db.DocumentTabs
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentTabs == null)
                return new HttpNotFoundResult();

            return View("NewOrEdit", documentTabs);
        }

        [HttpPost]
        public ActionResult Save(DocumentTabs documentTabs)
        {
            if (documentTabs.Id != 0)
            {
                _db.DocumentTabs.Attach(documentTabs);
                _db.Entry(documentTabs).State = EntityState.Modified;
            }
            else
                _db.DocumentTabs.Add(documentTabs);

            _db.SaveChanges();

            TempData["success"] = "Document tab " + documentTabs.Name + " saved";

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var documentTabs = await _db.DocumentTabs
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentTabs == null)
                return new HttpNotFoundResult();

            return View(documentTabs);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var documentTabs = await _db.DocumentTabs
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentTabs == null)
                return new HttpNotFoundResult();

            if (documentTabs.Documents.Count == 0)
            {
                _db.DocumentTabs.Remove(documentTabs);
                _db.SaveChanges();

                TempData["success"] = "Document Tab " + documentTabs.Name + " deleted successfully";
            }
            else
            {
                TempData["error"] = "Document Tab " + documentTabs.Name + " has Documents associated to it so it cannot be deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}