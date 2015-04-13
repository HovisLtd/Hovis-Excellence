using Hovis.Excellence.Web.Controllers;
using Hovis.Excellence.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DocumentCategoryController : HovisExcellenceController
    {
        public ActionResult Index()
        {
            var documentCategories = _db.DocumentCategories
                .OrderBy(x => x.Name)
                .ToList();

            return View(documentCategories);
        }

        public ActionResult New()
        {
            return View("NewOrEdit", new DocumentCategory());
        }

        public async Task<ActionResult> Edit(int id)
        {
            var documentCategory = await _db.DocumentCategories
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentCategory == null)
                return new HttpNotFoundResult();

            return View("NewOrEdit", documentCategory);
        }

        [HttpPost]
        public ActionResult Save(DocumentCategory documentCategory)
        {
            if (documentCategory.Id != 0)
            {
                _db.DocumentCategories.Attach(documentCategory);
                _db.Entry(documentCategory).State = EntityState.Modified;
            }
            else
                _db.DocumentCategories.Add(documentCategory);

            _db.SaveChanges();

            TempData["success"] = "Document Category " + documentCategory.Name + " saved";

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var documentCategory = await _db.DocumentCategories
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentCategory == null)
                return new HttpNotFoundResult();

            return View(documentCategory);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var documentCategory = await _db.DocumentCategories
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentCategory == null)
                return new HttpNotFoundResult();

            if (documentCategory.Documents.Count == 0)
            {
                _db.DocumentCategories.Remove(documentCategory);
                _db.SaveChanges();

                TempData["success"] = "Document Category " + documentCategory.Name + " deleted successfully";
            }
            else
            {
                TempData["error"] = "Document Category " + documentCategory.Name + " has Dcouments associated to it so it cannot be deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}