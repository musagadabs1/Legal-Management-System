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
    public class CertificationsController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Certifications
        public async Task<ActionResult> Index()
        {

            
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                var adminCertifications = db.Certifications.Include(c => c.Staff);
                return View(await adminCertifications.ToListAsync());
            }
            var certifications = db.Files.Include(f => f.Client).Include(f => f.Staff);
            return View(await certifications.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Certifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = await db.Certifications.FindAsync(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        // GET: Certifications/Create
        public ActionResult Create()
        {
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
            return View();
        }

        // POST: Certifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,CertificationType,DateAchieved,Skilled,StaffId,CreatedBy,CreatedOn,ModeifiedBy,ModifiedOn")] Certification certification)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                certification.CreatedBy = user;
                certification.CreatedOn = DateTime.Today;
                certification.StaffId = LegalGuideUtility.StaffId; //(string)ViewBag.StaffId;

                db.Certifications.Add(certification);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", certification.StaffId);
            return View(certification);
        }

        // GET: Certifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = await db.Certifications.FindAsync(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", certification.StaffId);
            return View(certification);
        }

        // POST: Certifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,CertificationType,DateAchieved,Skilled,StaffId,CreatedBy,CreatedOn,ModeifiedBy,ModifiedOn")] Certification certification)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                certification.ModeifiedBy = user;
                certification.ModifiedOn = DateTime.Today;
                certification.StaffId = LegalGuideUtility.StaffId; //(string)ViewBag.StaffId;

                db.Entry(certification).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", certification.StaffId);
            return View(certification);
        }

        // GET: Certifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = await db.Certifications.FindAsync(id);
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        // POST: Certifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Certification certification = await db.Certifications.FindAsync(id);
            db.Certifications.Remove(certification);
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
