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
    public class CourtActivitiesController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: CourtActivities
        public async Task<ActionResult> Index()
        {
           
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.CourtActivities.ToListAsync());
            }
            return View(await db.CourtActivities.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
            //return View(await certifications.ToListAsync());
        }

        // GET: CourtActivities/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourtActivity courtActivity = await db.CourtActivities.FindAsync(id);
            if (courtActivity == null)
            {
                return HttpNotFound();
            }
            return View(courtActivity);
        }

        // GET: CourtActivities/Create
        public ActionResult Create()
        {
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjoined",Text="Adjoined"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"}
            };
            //,,
            return View();
        }

        // POST: CourtActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MatterNumber,DateHeared,CourtName,Location,StaffId,Status,AdvocateArgument,OpponentArgument,AdvocateNote,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] CourtActivity courtActivity)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                courtActivity.CreatedBy = user;
                courtActivity.CreatedOn = DateTime.Today;

                var email = LegalGuideUtility.GetStaffEmailByLoginName(user);
                var staffId = LegalGuideUtility.GetStaffIdByEmail(email);

                courtActivity.StaffId = staffId;

                db.CourtActivities.Add(courtActivity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjoined",Text="Adjoined"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"}
            };
            return View(courtActivity);
        }

        // GET: CourtActivities/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourtActivity courtActivity = await db.CourtActivities.FindAsync(id);
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjoined",Text="Adjoined"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"}
            };
            if (courtActivity == null)
            {
                return HttpNotFound();
            }
            return View(courtActivity);
        }

        // POST: CourtActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,MatterNumber,DateHeared,CourtName,Location,StaffId,Status,AdvocateArgument,OpponentArgument,AdvocateNote,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] CourtActivity courtActivity)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                courtActivity.ModifiedBy = user;
                courtActivity.ModifiedOn = DateTime.Today;

                var email = LegalGuideUtility.GetStaffEmailByLoginName(user);
                var staffId = LegalGuideUtility.GetStaffIdByEmail(email);

                courtActivity.StaffId = staffId;

                db.Entry(courtActivity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjoined",Text="Adjoined"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"}
            };
            return View(courtActivity);
        }

        // GET: CourtActivities/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourtActivity courtActivity = await db.CourtActivities.FindAsync(id);
            if (courtActivity == null)
            {
                return HttpNotFound();
            }
            return View(courtActivity);
        }

        // POST: CourtActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CourtActivity courtActivity = await db.CourtActivities.FindAsync(id);
            db.CourtActivities.Remove(courtActivity);
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
