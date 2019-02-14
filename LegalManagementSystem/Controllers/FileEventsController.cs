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
    public class FileEventsController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: FileEvents
        public async Task<ActionResult> Index()
        {
            var fileEvents = db.FileEvents.Include(f => f.File).Include(f => f.Staff);
            return View(await fileEvents.ToListAsync());
        }

        // GET: FileEvents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileEvent fileEvent = await db.FileEvents.FindAsync(id);
            if (fileEvent == null)
            {
                return HttpNotFound();
            }
            return View(fileEvent);
        }

        // GET: FileEvents/Create
        public ActionResult Create()
        {
            ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName");
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
            return View();
        }

        // POST: FileEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FileNumber,StaffId,StartDate,EndDate,Location,EventName,Address,Description,Completed,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] FileEvent fileEvent)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                fileEvent.CreatedBy = user;
                fileEvent.CreatedOn = DateTime.Today;


                db.FileEvents.Add(fileEvent);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", fileEvent.FileNumber);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", fileEvent.StaffId);
            return View(fileEvent);
        }

        // GET: FileEvents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileEvent fileEvent = await db.FileEvents.FindAsync(id);
            if (fileEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", fileEvent.FileNumber);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", fileEvent.StaffId);
            return View(fileEvent);
        }

        // POST: FileEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FileNumber,StaffId,StartDate,EndDate,Location,EventName,Address,Description,Completed,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] FileEvent fileEvent)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                fileEvent.ModifiedBy = user;
                fileEvent.ModifiedOn = DateTime.Today;

                db.Entry(fileEvent).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FileNumber = new SelectList(db.Files, "FileNumber", "FileName", fileEvent.FileNumber);
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", fileEvent.StaffId);
            return View(fileEvent);
        }

        // GET: FileEvents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FileEvent fileEvent = await db.FileEvents.FindAsync(id);
            if (fileEvent == null)
            {
                return HttpNotFound();
            }
            return View(fileEvent);
        }

        // POST: FileEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FileEvent fileEvent = await db.FileEvents.FindAsync(id);
            db.FileEvents.Remove(fileEvent);
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
