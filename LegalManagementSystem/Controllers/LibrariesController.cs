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
    public class LibrariesController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Libraries
        public async Task<ActionResult> Index()
        {
            
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                var adminLibraries = db.Libraries.Include(l => l.Staff);
                return View(await adminLibraries.ToListAsync());
            }
            var libraries = db.Libraries.Include(l => l.Staff);
            return View(await libraries.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Libraries/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library library = await db.Libraries.FindAsync(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        // GET: Libraries/Create
        public ActionResult Create()
        {
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "Surname");
            return View();
        }

        // POST: Libraries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ISBN,Name,Type,UploadDate,FilePath,AdvocateId,CreatedBy,ModifiedBy,CreatedOn,ModifiedOn")] Library library)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                library.CreatedBy = user;
                library.CreatedOn = DateTime.Today;

                db.Libraries.Add(library);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "Surname", library.AdvocateId);
            return View(library);
        }

        // GET: Libraries/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library library = await db.Libraries.FindAsync(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "Surname", library.AdvocateId);
            return View(library);
        }

        // POST: Libraries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ISBN,Name,Type,UploadDate,FilePath,AdvocateId,CreatedBy,ModifiedBy,CreatedOn,ModifiedOn")] Library library)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                library.ModifiedBy = user;
                library.ModifiedOn = DateTime.Today;

                db.Entry(library).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AdvocateId = new SelectList(db.Staffs, "StaffId", "Surname", library.AdvocateId);
            return View(library);
        }

        // GET: Libraries/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Library library = await db.Libraries.FindAsync(id);
            if (library == null)
            {
                return HttpNotFound();
            }
            return View(library);
        }

        // POST: Libraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Library library = await db.Libraries.FindAsync(id);
            db.Libraries.Remove(library);
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
