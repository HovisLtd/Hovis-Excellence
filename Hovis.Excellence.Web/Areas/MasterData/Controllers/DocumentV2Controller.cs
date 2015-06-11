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
using Hovis.Excellence.Web.Areas.MasterData.ViewModels;

namespace Hovis.Excellence.Web.Areas.MasterData.Controllers
{
    public class DocumentV2Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MasterData/DocumentV2
        public ActionResult Index()
        {
            var documents = db.Documents.Include(d => d.Applciation).Include(d => d.DocumentCategory).Include(d => d.DocumentTabs).Include(d => d.DocumentType);

            var viewModel = new DocumentListViewModel
            {
                Applications = db.Applications.OrderBy(x => x.Name).ToList(),
                Documents = documents.OrderBy(x => x.DocumentCategory.Name).ThenBy(x => x.Title).ToList(),
            };

            return View(viewModel);
            //var documents = db.Documents.Include(d => d.Applciation).Include(d => d.DocumentCategory).Include(d => d.DocumentTabs).Include(d => d.DocumentType);
            //return View(documents.ToList());
        }

        // GET: MasterData/DocumentV2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: MasterData/DocumentV2/Create
        public ActionResult Create()
        {
            var model = new Document();
            model.Owner = User.Identity.Name;
            model.IssueDate = DateTime.Now;


            var values = new[] { 0, 1, 3, 6, 9, 12, 18, 24 }
                    .Select(x => new SelectListItem
                    {
                        Value = x.ToString(),
                        Text = (x > 0) ? x.ToString() + " Months" : "Not Applicable" //0 = not applicable
                    });

            ViewBag.ReviewPeriods = new SelectList(values, "Value", "Text");
            ViewBag.ApplicationId = new SelectList(db.Applications, "Id", "Name");
            ViewBag.DocumentCategoryId = new SelectList(db.DocumentCategories, "Id", "Name");
            ViewBag.DocumentTabsId = new SelectList(db.DocumentTabs, "Id", "Name");
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "Name");
            return View(model);
        }

        // POST: MasterData/DocumentV2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,IssueNumber,IssueDate,Description,Owner,ReviewPeriodInMonths,ApplicationId,DocumentTypeId,DocumentCategoryId,DocumentTabsId,DateCreated")] Document document)
        {
            if (ModelState.IsValid)
            {
                if (document.Title == null)
                {
                    document.Title = "";
                }
                if (document.IssueNumber == null)
                {
                    document.IssueNumber = "";
                }
                if (document.Description == null)
                {
                    document.Description = "";
                }
                if (document.Owner == null)
                {
                    document.Owner = User.Identity.Name;
                }
                if (document.ReviewPeriodInMonths == 0)
                {
                    document.ReviewPeriodInMonths = 6;
                }
                if (document.DateCreated == null)
                {
                    document.DateCreated = DateTime.Now;
                }
                db.Documents.Add(document);
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Index", "DocumentLinks", new { id = document.Id, fromarea = "MasterData", fromdocument = "DocumentV2" });

            }

            var values = new[] { 0, 1, 3, 6, 9, 12, 18, 24 }
                            .Select(x => new SelectListItem
                            {
                                Value = x.ToString(),
                                Text = (x > 0) ? x.ToString() + " Months" : "Not Applicable" //0 = not applicable
                            });

            ViewBag.ReviewPeriods = new SelectList(values, "Value", "Text");
            ViewBag.ApplicationId = new SelectList(db.Applications, "Id", "Name", document.ApplicationId);
            ViewBag.DocumentCategoryId = new SelectList(db.DocumentCategories, "Id", "Name", document.DocumentCategoryId);
            ViewBag.DocumentTabsId = new SelectList(db.DocumentTabs, "Id", "Name", document.DocumentTabsId);
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "Name", document.DocumentTypeId);
            return View(document);
        }

        // GET: MasterData/DocumentV2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            var values = new[] { 0, 1, 3, 6, 9, 12, 18, 24 }
                .Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = (x > 0) ? x.ToString() + " Months" : "Not Applicable" //0 = not applicable
                });

            ViewBag.ReviewPeriods = new SelectList(values, "Value", "Text");
            ViewBag.ApplicationId = new SelectList(db.Applications, "Id", "Name", document.ApplicationId);
            ViewBag.DocumentCategoryId = new SelectList(db.DocumentCategories, "Id", "Name", document.DocumentCategoryId);
            ViewBag.DocumentTabsId = new SelectList(db.DocumentTabs, "Id", "Name", document.DocumentTabsId);
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "Name", document.DocumentTypeId);
            return View(document);
        }

        // POST: MasterData/DocumentV2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IssueNumber,IssueDate,Description,Owner,ReviewPeriodInMonths,ApplicationId,DocumentTypeId,DocumentCategoryId,DocumentTabsId,DateCreated")] Document document)
        {
            if (ModelState.IsValid)
            {

                if (document.Title == null)
                {
                    document.Title = "";
                }
                if (document.IssueNumber == null)
                {
                    document.IssueNumber = "";
                }
                if (document.Description == null)
                {
                    document.Description = "";
                }
                if (document.Owner == null)
                {
                    document.Owner = User.Identity.Name;
                }
                if (document.ReviewPeriodInMonths == 0)
                {
                    document.ReviewPeriodInMonths = 6;
                }
                if (document.DateCreated == null)
                {
                    document.DateCreated = DateTime.Now;
                }

                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "DocumentLinks", new { id = document.Id });
            }

            var values = new[] { 0, 1, 3, 6, 9, 12, 18, 24 }
                .Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = (x > 0) ? x.ToString() + " Months" : "Not Applicable" //0 = not applicable
                });

            ViewBag.ReviewPeriods = new SelectList(values, "Value", "Text");
            ViewBag.ApplicationId = new SelectList(db.Applications, "Id", "Name", document.ApplicationId);
            ViewBag.DocumentCategoryId = new SelectList(db.DocumentCategories, "Id", "Name", document.DocumentCategoryId);
            ViewBag.DocumentTabsId = new SelectList(db.DocumentTabs, "Id", "Name", document.DocumentTabsId);
            ViewBag.DocumentTypeId = new SelectList(db.DocumentTypes, "Id", "Name", document.DocumentTypeId);
            return View(document);
        }

        // GET: MasterData/DocumentV2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: MasterData/DocumentV2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
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
