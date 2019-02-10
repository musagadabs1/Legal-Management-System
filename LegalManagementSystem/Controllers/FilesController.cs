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
    [Authorize(Roles ="Admin,Advocate,Attorney")]
    public class FilesController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Files
        public async Task<ActionResult> Index()
        {
            
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                var adminFiles = db.Files.Include(f => f.Client).Include(f => f.Staff);
                return View(await adminFiles.ToListAsync());
            }
            var files = db.Files.Include(f => f.Client).Include(f => f.Staff);
            return View(await files.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Files/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = await db.Files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName");
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "Surname");
            ViewBag.Type = new List<SelectListItem>
            {
              new SelectListItem{Text="Family",Value="Family"},
               new SelectListItem{Text="Criminal",Value="Criminal"},
               new SelectListItem{Text="Dispute",Value="Dispute"}
            };
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FileNumber,FileName,FileType,ClientId,OpeningDate,FilePath,AdvocateId,ClosingDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Models.File file, HttpPostedFileBase fileBase)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;
            if (ModelState.IsValid)
            {
                try
                {

                    if (fileBase.ContentLength > 0 && fileBase != null)
                    {
                        filePath = fileBase.FileName;
                        fileName = Path.GetFileName(fileBase.FileName);
                    }
                    var folderPath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Docs";
                    var docPath = Path.Combine(folderPath, filePath);

                    var user = User.Identity.Name;
                    file.CreatedBy = user;
                    file.CreatedOn = DateTime.Today;
                    file.FilePath = fileName;

                    db.Files.Add(file);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error Occoured while uploading a file. Check and try again. " + ex.Message;
                    return View(file);
                }

            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", file.ClientId);
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "Surname", file.AdvocateId);
            ViewBag.Type = new List<SelectListItem>
            {
              new SelectListItem{Text="Family",Value="Family"},
               new SelectListItem{Text="Criminal",Value="Criminal"},
               new SelectListItem{Text="Dispute",Value="Dispute"}
            };
            return View(file);
        }

        // GET: Files/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = await db.Files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", file.ClientId);
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "Surname", file.AdvocateId);
            ViewBag.Type = new List<SelectListItem>
            {
              new SelectListItem{Text="Family",Value="Family"},
               new SelectListItem{Text="Criminal",Value="Criminal"},
               new SelectListItem{Text="Dispute",Value="Dispute"}
            };
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FileNumber,FileName,FileType,ClientId,OpeningDate,FilePath,AdvocateId,ClosingDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Models.File file, HttpPostedFileBase fileBase)
        {
            string fileName = string.Empty;
            string filePath = string.Empty;
            if (ModelState.IsValid)
            {
                if (fileBase.ContentLength > 0 && fileBase != null)
                {
                    filePath = fileBase.FileName;
                    fileName = Path.GetFileName(fileBase.FileName);
                }
                var folderPath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Docs";
                var docPath = Path.Combine(folderPath, filePath);
                var user = User.Identity.Name;
                file.ModifiedBy = user;
                file.ModifiedOn = DateTime.Today;
                file.FilePath = fileName;

                db.Entry(file).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", file.ClientId);
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "Surname", file.AdvocateId);
            ViewBag.Type = new List<SelectListItem>
            {
              new SelectListItem{Text="Family",Value="Family"},
               new SelectListItem{Text="Criminal",Value="Criminal"},
               new SelectListItem{Text="Dispute",Value="Dispute"}
            };
            return View(file);
        }

        // GET: Files/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Models.File file = await db.Files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Models.File file = await db.Files.FindAsync(id);
            db.Files.Remove(file);
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
