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
    [Authorize(Roles = "Admin,Attorney,Advocate")]
    public class DocumentsController : Controller
    {

        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Documents
        public async Task<ActionResult> Index()
        {
            try
            {
                var user = User.Identity.Name;
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                {
                    return View(await db.Documents.ToListAsync());
                }
                return View(await db.Documents.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
            }
            catch (Exception ex)
            {
                LegalGuideUtility.ErrorMessage = "Oooop. Something had happened. " + ex.Message;
                return RedirectToAction("Error");
                //throw;
            }

        }
        public ActionResult Error()
        {
            return View();
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
        public JsonResult GetMatterForDropDown(string searchKey)
        {
            //var user = User.Identity.Name;
            //var email = LegalGuideUtility.GetStaffEmailByLoginName(user);
            //var staffId = LegalGuideUtility.GetStaffIdByEmail(email);
            var getData = db.GetAllMattersForDropDown().ToList();

            //if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            //{
            if (searchKey != null)
            {
                getData = db.GetAllMattersForDropDown().Where(x => x.Subject.Contains(searchKey)).ToList();
            }
            var ModifiedData = getData.Select(x => new
            {
                id = x.Id,
                text = x.Subject
            });

            return Json(ModifiedData, JsonRequestBehavior.AllowGet);
            //}
            //return null;
            //var data = null;


        }


        // GET: Documents/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Documents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DocumentId,MatterNumber,DocName,AssignedDate,Tags,Description,CreatedBy,DateCreated,ModifiedBy,DateModified,DocPath")] Document document, HttpPostedFileBase fileBase)
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
                    //ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", document.FileNumber);
                    //ViewBag.MatterNumber = new SelectList(db.Matters, "MatterNumber", "Subject");
                    return View(document);

                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't add Document. Something went wrong " + ex.Message;
            }


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
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DocumentId,MatterNumber,DocName,AssignedDate,Tags,Description,CreatedBy,DateCreated,ModifiedBy,DateModified,DocPath")] Document document,HttpPostedFileBase fileBase)
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
                    ViewBag.MatterNumber = new SelectList(db.Matters, "MatterNumber", "Subject", document.MatterNumber);
                    return View(document);

                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't add Document. Something went wrong " + ex.Message;
            }

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
            Document document = await db.Documents.FindAsync(id);
            db.Documents.Remove(document);
            await db.SaveChangesAsync();
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
