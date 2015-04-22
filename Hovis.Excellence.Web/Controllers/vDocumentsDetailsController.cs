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
using PagedList;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace Hovis.Excellence.Web.Controllers
{
    public class vDocumentsDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: vDocumentsDetails
        public ActionResult Index()
        {
            return View(db.v_Documents_Details.ToList());
        }

        // GET: SearchDetails
        //[Authorize(Roles = "Admin,VPDcanEdit")]
        public ActionResult IndexSearch(string currentFilter, string Searchfield, int? page)
        {

            var sfield = Convert.ToString(Searchfield);
            var cFilter = Convert.ToString(currentFilter);

            if (sfield != null)
            {
                page = 1;
            }
            else
            {
                sfield = cFilter;
            }

            ViewBag.CurrentFilter = sfield;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            if (sfield == null)
            {
                return View(db.v_Documents_Details
                .OrderBy(x => x.DocumentType).ThenBy(x => x.Title).ThenBy(x => x.Id).ToPagedList(pageNumber, pageSize));

            }
            else
            { 
            return View(db.v_Documents_Details
            .Where(x => x.DocumentType.Contains(sfield) || x.Title.Contains(sfield) || x.Owner.Contains(sfield) || x.Description.Contains(sfield))
            .OrderBy(x => x.DocumentType).ThenBy(x => x.Title).ThenBy(x => x.Id).ToPagedList(pageNumber, pageSize));
            }
        }


        public ActionResult Report(string typeid, string Searchfield)
        {
            LocalReport lr = new LocalReport();

            //For this to work with AZURE you must set the report build action to CONTENT and the 
            //Copy to output directory set to DO NOT COPY
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Reports/VersionReport.rdlc");
            string pdffilename = "HovisEnvironmentPolicyManuals.pdf";
            var sfield = Convert.ToString(Searchfield);
            if (typeid == null)
            {
                typeid = "pdf";
            }

            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return RedirectToAction("Index", "Home");
                //return View("Index");
            }
            List<v_Documents_Details> cm = new List<v_Documents_Details>();
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                if (sfield == null)
                { 
                    cm = dc.v_Documents_Details.ToList();
                }
                else
                {
                    cm = dc.v_Documents_Details
                           .Where(x => x.DocumentType.Contains(sfield) || x.Title.Contains(sfield) || x.Owner.Contains(sfield) || x.Description.Contains(sfield))
                           .OrderBy(x => x.DocumentType).ThenBy(x => x.Title).ThenBy(x => x.Id).ToList();
                }
            }
            ReportDataSource rd = new ReportDataSource("DocumentDetailsDataSet", cm);
            lr.DataSources.Add(rd);
            string reportType = typeid;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + typeid + "</OutputFormat>" +
            "  <PageWidth>11.7in</PageWidth>" +
            "  <PageHeight>8.27in</PageHeight>" +
            "  <MarginTop>0.2in</MarginTop>" +
            "  <MarginLeft>0.2in</MarginLeft>" +
            "  <MarginRight>0.2in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType, pdffilename);
        }



        // GET: vDocumentsDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            v_Documents_Details v_Documents_Details = db.v_Documents_Details.Find(id);
            if (v_Documents_Details == null)
            {
                return HttpNotFound();
            }
            return View(v_Documents_Details);
        }

        // GET: vDocumentsDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: vDocumentsDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,IssueNumber,IssueDate,Description,Owner,ReviewPeriodInMonths,ApplicationId,DocumentTypeId,DocumentCategoryId,DocumentTabsId,ApplicationName,DocumentType,DocumentCategory,DocumentTab,DateCreated")] v_Documents_Details v_Documents_Details)
        {
            if (ModelState.IsValid)
            {
                db.v_Documents_Details.Add(v_Documents_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(v_Documents_Details);
        }

        // GET: vDocumentsDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            v_Documents_Details v_Documents_Details = db.v_Documents_Details.Find(id);
            if (v_Documents_Details == null)
            {
                return HttpNotFound();
            }
            return View(v_Documents_Details);
        }

        // POST: vDocumentsDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,IssueNumber,IssueDate,Description,Owner,ReviewPeriodInMonths,ApplicationId,DocumentTypeId,DocumentCategoryId,DocumentTabsId,ApplicationName,DocumentType,DocumentCategory,DocumentTab,DateCreated")] v_Documents_Details v_Documents_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(v_Documents_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(v_Documents_Details);
        }

        // GET: vDocumentsDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            v_Documents_Details v_Documents_Details = db.v_Documents_Details.Find(id);
            if (v_Documents_Details == null)
            {
                return HttpNotFound();
            }
            return View(v_Documents_Details);
        }

        // POST: vDocumentsDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            v_Documents_Details v_Documents_Details = db.v_Documents_Details.Find(id);
            db.v_Documents_Details.Remove(v_Documents_Details);
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
