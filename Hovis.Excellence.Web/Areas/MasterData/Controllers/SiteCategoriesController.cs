using Hovis.Excellence.Web.Controllers;
using Hovis.Excellence.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SiteCategoriesController : HovisExcellenceController
    {
        public ActionResult Index()
        {
            var siteCategories = _db.SiteCategories
                .OrderBy(x => x.Name)
                .ToList();

            return View(siteCategories);
        }

        public ViewResult New()
        {
            return View("NewOrEdit", new SiteCategory());
        }

        public async Task<ActionResult> Edit(int id)
        {
            var siteCategory = await _db.SiteCategories
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            //todo: if null, return 404
            return View("NewOrEdit", siteCategory);
        }

        [HttpPost]
        public ActionResult Save(SiteCategory siteCategory)
        {
            if (siteCategory.Id != 0)
            {
                _db.SiteCategories.Attach(siteCategory);
                _db.Entry(siteCategory).State = EntityState.Modified;
            }
            else
                _db.SiteCategories.Add(siteCategory);

            SaveChanges();

            TempData["success"] = "Site Category " + siteCategory.Name + " saved";

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var siteCategory = await _db.SiteCategories.SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (siteCategory == null)
                return new HttpNotFoundResult();

            return View(siteCategory);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var siteCategory = await _db.SiteCategories.SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (siteCategory == null)
                return new HttpNotFoundResult();

            if (siteCategory.Sites.Count == 0)
            {
                _db.SiteCategories.Remove(siteCategory);
                _db.SaveChanges();

                TempData["success"] = "Site Category " + siteCategory.Name + " deleted successfully";
            }
            else
            {
                TempData["error"] = "Site Category " + siteCategory.Name + " has Sites associated to it so it cannot be deleted.";
            }

            return RedirectToAction("Index");
        }
    }
}