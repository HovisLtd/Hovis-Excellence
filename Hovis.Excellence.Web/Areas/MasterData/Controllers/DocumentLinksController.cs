using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hovis.Excellence.Web;
using Hovis.Excellence.Web.Models;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    public class DocumentLinksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MasterData/DocumentLinks
        public ActionResult Index(long? id, string fromarea, string fromdocument)
        {
            // Work out if we have just the one attachment to view
            int NoOfAttachments = 0;

            IQueryable<DocumentLinks> doclink = db.DocumentLinks
                    .Where(x => x.DocID == id);

            NoOfAttachments = doclink.Count();

            if (NoOfAttachments == 1)
            {
                //need to work out how to open a google doc direct from a controller
                ViewBag.fromarea = fromarea;
                ViewBag.fromdocument = fromdocument;
                ViewBag.id = id;
                return View(db.DocumentLinks.ToList()
                    .Where(c => c.DocID == id));
                //return JavaScript("window.open('https://drive.google.com/file/d/0B6ZAuXCcOKuZTkMxd25wempBRDQ/view?usp=sharing')");
                //return RedirectToAction("GoogleDoc");
            }
            else
            {
            ViewBag.fromarea = fromarea;
            ViewBag.fromdocument = fromdocument;
            ViewBag.id = id;
            return View(db.DocumentLinks.ToList()
                .Where(c => c.DocID == id));
            }

        }

        public ActionResult GoogleDoc(int? id)
        {
            //This gets the document data
            ViewData["Documentlinks"] = (from documentlink in db.DocumentLinks
                                         where documentlink.Id.Equals(id)
                                     select documentlink)
                                         .ToList();
            return View();
        }

        // GET: MasterData/DocumentLinks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentLinks documentLinks = db.DocumentLinks.Find(id);
            if (documentLinks == null)
            {
                return HttpNotFound();
            }
            return View(documentLinks);
        }

        // GET: MasterData/DocumentLinks/Create
        public ActionResult Create(int? id)
        {
            ViewBag.id = id;
            var model = new DocumentLinks();
            model.DocID = id.Value;
            model.DateCreated = DateTime.Now;
            return View(model);
        }

        // POST: MasterData/DocumentLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,Owner,FileLink,DocID,DateCreated")] DocumentLinks documentLinks)
        {
            if (ModelState.IsValid)
            {
                db.DocumentLinks.Add(documentLinks);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "DocumentLinks", new { id = documentLinks.DocID });
            }

            return View(documentLinks);
        }

        // GET: MasterData/DocumentLinks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentLinks documentLinks = db.DocumentLinks.Find(id);
            if (documentLinks == null)
            {
                return HttpNotFound();
            }
            return View(documentLinks);
        }

        // POST: MasterData/DocumentLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,Owner,FileLink,DocID,DateCreated")] DocumentLinks documentLinks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(documentLinks).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "DocumentLinks", new { id = documentLinks.DocID });

            }
            return View(documentLinks);
        }

        // GET: MasterData/DocumentLinks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DocumentLinks documentLinks = db.DocumentLinks.Find(id);
            if (documentLinks == null)
            {
                return HttpNotFound();
            }
            return View(documentLinks);
        }

        // POST: MasterData/DocumentLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DocumentLinks documentLinks = db.DocumentLinks.Find(id);
            var docidval = documentLinks.DocID;
            db.DocumentLinks.Remove(documentLinks);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return RedirectToAction("Index", "DocumentLinks", new { id = docidval });

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
