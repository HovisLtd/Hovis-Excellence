using Hovis.Excellence.Web.Areas.AppShared;
using Hovis.Excellence.Web.Areas.MasterData.ViewModels;
using Hovis.Excellence.Web.Controllers;
using Hovis.Excellence.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.CustomerLoyalty.Controllers
{
    public class HomeController : HovisExcellenceController
    {
        public ActionResult Index()
        {
            // get the number of tabs by quering the documents table and field documenttabs
            ViewData["DocumentTabs"] = (from document in _db.Documents
                                        where document.ApplicationId.Equals(2)
                                        group document by new { document.DocumentTabs } into ListDocuments
                                        select new ActTabsViewModelV4 { TabName = ListDocuments.Key.DocumentTabs.Name }
                ).ToList();

            var CheckTabs = (from document in _db.Documents
                                    where document.ApplicationId.Equals(2)
                                    group document by new { document.DocumentTabs } into ListDocuments
                                    select new { TabName = ListDocuments.Key.DocumentTabs.Name });
            var counttabs = CheckTabs.Count();

            if (counttabs == 0)
            {
                // load a default if no data to documenttabs
                var tabitems = new[]
                        {
                            new ActTabsViewModelV4 { TabName = "Documents"}
                        };
             
                ViewData["DocumentTabs"] = tabitems.ToList();

                // load a default if no data to documentcats yep
                var catitems = new[]
                        {
                            //new ActTabsViewModelV6 { TabName = "Documents"},
                            //new ActTabsViewModelV6 { CatName = "NoDocumentsfound"}
                            new ActTabsViewModelV6 { TabName = "Documents",
                                                     CatName = "No Documents found" }
                        };

                ViewData["DocumentCats"] = catitems.ToList();

                //Just choose first document in the database as will not be shown
                //var docitems = new[]
                //        {
                //            new Document { Id = 1,
                //             Title = "test",
                //             IssueNumber = "test",
                //             IssueDate = System.DateTime.Now,
                //             Description = "test",
                //             Owner = "test",
                //             ReviewPeriodInMonths = 1,
                //             ApplicationId = 1,
                //             DocumentTypeId = 1,
                //             DocumentCategoryId = 1,
                //             DocumentTabsId = 1,
                //             DateCreated = System.DateTime.Now }
                //        };
                ViewData["Documents"] = (from document in _db.Documents
                                         where document.Id.Equals(1)
                                         select document)
                             .ToList();

                //ViewData["Documents"] = docitems.ToList();

            }
            else
            {
                //This time get the documenttabs and documentcat data from documents
                ViewData["DocumentCats"] = (from document in _db.Documents
                                            where document.ApplicationId.Equals(2)
                                            group document by new { document.DocumentTabs, document.DocumentCategory } into ListDocuments
                                            select new ActTabsViewModelV6 { TabName = ListDocuments.Key.DocumentTabs.Name, CatName = ListDocuments.Key.DocumentCategory.Name }
                    ).ToList();

                //This gets the document data
                ViewData["Documents"] = (from document in _db.Documents
                                         where document.ApplicationId.Equals(2)
                                         select document)
                                             .ToList();
            }
            return View();
        }

        public ActionResult Delete(int? id, string fromarea)
        {
            ViewBag.fromarea = fromarea;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = _db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: MasterData/DocumentLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string fromarea)
        {
            ViewBag.fromarea = fromarea;

            //remove document attachments first
            IQueryable<DocumentLinks> deleteDocumentLinks = _db.DocumentLinks
                    .Where(c => c.DocID == id);

            foreach (var deletedocumentlink in deleteDocumentLinks)
            {
                _db.DocumentLinks.Remove(deletedocumentlink);
            }
            _db.SaveChanges();

            //Then delete the document itself
            IQueryable<Document> deleteDocuments = _db.Documents
                    .Where(c => c.Id == id);

            foreach (var deletedocument in deleteDocuments)
            {
                _db.Documents.Remove(deletedocument);
            }
            _db.SaveChanges();


            //DocumentLinks documentLinks = _db.DocumentLinks.Find(id);
            //var docidval = documentLinks.DocID;
            //_db.DocumentLinks.Remove(documentLinks);
            //_db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Index", "Home", new { area = fromarea });

        }
    }
}