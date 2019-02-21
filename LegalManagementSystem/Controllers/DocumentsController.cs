using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LegalManagementSystem.Models;
using System.IO;

namespace LegalManagementSystem.Controllers
{
    public class DocumentsController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Documents
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                var adminDocuments = db.Documents.Include(d => d.File);
                return View(await adminDocuments.ToListAsync());
            }
            var documents = db.Documents.Include(d => d.File);
            return View(await documents.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName");
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DocumentId,DocName,AssignedDate,Tags,Description,CreatedBy,DateCreated,ModifiedBy,DateModified,DocPath,FileNumber")] Document document,HttpPostedFileBase fileBase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = string.Empty;
                    string filePath = string.Empty;

                    if (fileBase.ContentLength > 0 && fileBase != null)
                    {
                          filePath = fileBase.FileName;
                          fileName = Path.GetFileName(fileBase.FileName);
                    }
                    var folderPath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Docs";
                    var docPath = Path.Combine(folderPath, filePath);
                    var user = User.Identity;
                    document.CreatedBy = user.Name;
                    document.DateCreated = DateTime.Today;
                    document.DocPath = fileName;

                    db.Documents.Add(document);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Can't add Document. Fill in the required Fields. ";
                    ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", document.FileNumber);
                    return View(document);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Can't add Document. Something went wrong " + ex.Message;
                //throw;
            }

            ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", document.FileNumber);
            return View(document);
        }

        // GET: Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", document.FileNumber);
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DocumentId,DocName,AssignedDate,Tags,Description,CreatedBy,DateCreated,ModifiedBy,DateModified,DocPath,FileNumber")] Document document,HttpPostedFileBase fileBase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = string.Empty;
                    string filePath = string.Empty;

                    if (fileBase.ContentLength > 0 && fileBase != null)
                    {
                        filePath = fileBase.FileName;
                        fileName = Path.GetFileName(fileBase.FileName);
                    }
                    var folderPath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Docs";
                    var docPath = Path.Combine(folderPath, filePath);
                    var user = User.Identity;
                    document.ModifiedBy = user.Name;
                    document.DateModified = DateTime.Today;
                    document.DocPath = fileName;

                    db.Entry(document).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Can't add Document. Fill in the required Fields. ";
                    ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", document.FileNumber);
                    return View(document);
                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't add Document. Something went wrong " + ex.Message;
            }
            ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", document.FileNumber);
            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await db.Documents.FindAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Document document = await db.Documents.FindAsync(id);
                db.Documents.Remove(document);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
              ViewBag.Error = "Can't delete the Document. Check and try again. " + ex.Message;
              return View();
                //throw;
            }
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
