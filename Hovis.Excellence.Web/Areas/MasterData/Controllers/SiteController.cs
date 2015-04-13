using Hovis.Excellence.Web.Areas.MasterData.ViewModels;
using Hovis.Excellence.Web.Controllers;
using Hovis.Excellence.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SiteController : HovisExcellenceController
    {
        public ActionResult Index()
        {
            var sites = _db.Sites
                .GroupBy(x => x.SiteCategory.Name)
                .ToList()
                .OrderBy(x => x.Key)
                .Select(x => new SiteListViewModel { CategoryName = x.Key, Sites = x.ToList() });

            return View(sites);
        }

        public ActionResult New()
        {
            var siteCategories = _db.SiteCategories.ToList();

            if (!siteCategories.Any())
            {
                TempData["error"] =
                    string.Format(
                        @"No site categories have been created. <a href=""{0}"" class=""alert-link"" > Click here </a> to add a site category now.",
                        Url.Action("New", "SiteCategories"));

                return RedirectToAction("Index");
            }

            var site = new SiteAddEditViewModel
            {
                AvailableSiteCategories = siteCategories,
                Site = new Site()
            };

            return View("NewOrEdit", site);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var site = await _db.Sites
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (site == null)
                return new HttpNotFoundResult();

            var siteViewModel = new SiteAddEditViewModel
            {
                AvailableSiteCategories = _db.SiteCategories.ToList(),
                Site = site
            };

            return View("NewOrEdit", siteViewModel);
        }

        [HttpPost]
        public ActionResult Save(Site site)
        {
            if (site.Id != 0)
            {
                _db.Sites.Attach(site);
                _db.Entry(site).State = EntityState.Modified;
            }
            else
                _db.Sites.Add(site);

            //call the base controller save changes method
            SaveChanges();

            TempData["success"] = "Site '" + site.Name + "' saved";

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            var site = await _db.Sites
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (site == null)
                return new HttpNotFoundResult();

            return View(site);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(int id)
        {
            var site = await _db.Sites.SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (site == null)
                return new HttpNotFoundResult();

            _db.Sites.Remove(site);
            _db.SaveChanges();

            TempData["success"] = "Site '" + site.Name + "' deleted successfully";

            return RedirectToAction("Index");
        }
    }
}