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

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles ="Admin,Attorney,Advocate")]
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
            File file = await db.Files.FindAsync(id);
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
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "FirstName");
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
        public async Task<ActionResult> Create([Bind(Include = "Id,FileNumber,FileName,FileType,ClientId,OpeningDate,FilePath,AdvocateId,ClosingDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] File file)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                file.CreatedBy = user;
                file.CreatedOn = DateTime.Today;

                db.Files.Add(file);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", file.ClientId);
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "FirstName", file.AdvocateId);
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
            File file = await db.Files.FindAsync(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", file.ClientId);
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "FirstName", file.AdvocateId);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,FileNumber,FileName,FileType,ClientId,OpeningDate,FilePath,AdvocateId,ClosingDate,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] File file)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                file.CreatedBy = user;
                file.CreatedOn = DateTime.Today;

                db.Entry(file).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", file.ClientId);
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "FirstName", file.AdvocateId);
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
            File file = await db.Files.FindAsync(id);
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
            File file = await db.Files.FindAsync(id);
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
