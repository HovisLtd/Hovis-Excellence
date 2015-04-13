using Hovis.Excellence.Web.Areas.MasterData.Controllers;
using Hovis.Excellence.Web.Controllers;
using Hovis.Excellence.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DocumentTypeController : HovisExcellenceController
    {
        public ActionResult Index()
        {
            var personroles = _db.DocumentTypes
                .OrderBy(x => x.Name)
                .ToList();

            return View(personroles);
        }

        public ActionResult New()
        {
            return View("NewOrEdit", new DocumentType());
        }

        public async Task<ActionResult> Edit(int id)
        {
            var documentType = await _db.DocumentTypes
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentType == null)
                return new HttpNotFoundResult();

            return View("NewOrEdit", documentType);
        }

        [HttpPost]
        public ActionResult Save(DocumentType documentType)
        {
            if (documentType.Id != 0)
            {
                _db.DocumentTypes.Attach(documentType);
                _db.Entry(documentType).State = EntityState.Modified;
            }
            else
                _db.DocumentTypes.Add(documentType);

            _db.SaveChanges();

            TempData["success"] = "Document Type " + documentType.Name + " saved";

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var documentType = await _db.DocumentTypes
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentType == null)
                return new HttpNotFoundResult();

            return View(documentType);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var documentType = await _db.DocumentTypes
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (documentType == null)
                return new HttpNotFoundResult();

            if (documentType.Documents.Count == 0)
            {
                _db.DocumentTypes.Remove(documentType);
                _db.SaveChanges();

                TempData["success"] = "Document Type " + documentType.Name + " deleted successfully";
            }
            else
            {
                TempData["error"] = "Document Type " + documentType.Name + " has Dcouments associated to it so it cannot be deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}