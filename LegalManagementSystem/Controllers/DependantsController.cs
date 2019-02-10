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
    public class DependantsController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Dependants
        public async Task<ActionResult> Index()
        {
            var dependants = db.Dependants.Include(d => d.Staff);
            return View(await dependants.ToListAsync());
        }

        // GET: Dependants/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = await db.Dependants.FindAsync(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            return View(dependant);
        }

        // GET: Dependants/Create
        public ActionResult Create()
        {
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
            return View();
        }

        // POST: Dependants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,PolicyNumber,EffectiveDate,EndDate,Gender,Relationship,DateOfBirth,Description,Address,StaffId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Dependant dependant)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                dependant.CreatedBy = user;
                dependant.CreatedOn = DateTime.Today;
                dependant.StaffId = LegalGuideUtility.StaffId;
                db.Dependants.Add(dependant);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", dependant.StaffId);
            return View(dependant);
        }

        // GET: Dependants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = await db.Dependants.FindAsync(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", dependant.StaffId);
            return View(dependant);
        }

        // POST: Dependants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,PolicyNumber,EffectiveDate,EndDate,Gender,Relationship,DateOfBirth,Description,Address,StaffId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Dependant dependant)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                dependant.ModifiedBy = user;
                dependant.ModifiedOn = DateTime.Today;
                dependant.StaffId = LegalGuideUtility.StaffId;

                db.Entry(dependant).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", dependant.StaffId);
            return View(dependant);
        }

        // GET: Dependants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = await db.Dependants.FindAsync(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            return View(dependant);
        }

        // POST: Dependants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Dependant dependant = await db.Dependants.FindAsync(id);
            db.Dependants.Remove(dependant);
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
