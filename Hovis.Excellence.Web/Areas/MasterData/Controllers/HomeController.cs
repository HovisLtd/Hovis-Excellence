using Hovis.Excellence.Web.Areas.MasterData.ViewModels;
using Hovis.Excellence.Web.Controllers;
using Hovis.Excellence.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    public class HomeController : HovisExcellenceController
    {
        // GET: MasterData/Home
        public ActionResult Index()
        {
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
            return RedirectToAction("Index", "DocumentV2", new { area = fromarea });

        }
    }
}