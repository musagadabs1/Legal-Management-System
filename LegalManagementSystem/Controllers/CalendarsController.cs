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
    public class CalendarsController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Calendars
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.Calendars.ToListAsync());
            }
            return View(await db.Calendars.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Calendars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendar calendar = await db.Calendars.FindAsync(id);
            if (calendar == null)
            {
                return HttpNotFound();
            }
            return View(calendar);
        }

        // GET: Calendars/Create
        public ActionResult Create()
        {
            ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
            return View();
        }

        // POST: Calendars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,From,To,MatterNumber,StaffId,Locations,Priority,MeetingType,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Calendar calendar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = User.Identity.Name;
                    calendar.CreatedBy = user;
                    calendar.CreatedOn = DateTime.Today;

                    db.Calendars.Add(calendar);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.Error = "Can't Add Calendar, Error Occured. Please Contact IT Department.";
                }
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
                ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
                return View(calendar);
            }
            ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
            return View(calendar);
        }

        // GET: Calendars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
            Calendar calendar = await db.Calendars.FindAsync(id);
            if (calendar == null)
            {
                return HttpNotFound();
            }
            return View(calendar);
        }

        // POST: Calendars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,From,To,MatterNumber,StaffId,Locations,Priority,MeetingType,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Calendar calendar)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var user = User.Identity.Name;
                    calendar.ModifiedBy = user;
                    calendar.ModifiedOn = DateTime.Today;
                    db.Entry(calendar).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.Error = "Can't Add Calendar, Error Occured. Please Contact IT Department.";
                }

            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
                ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
                return View(calendar);
            }
            ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
            return View(calendar);
        }

        // GET: Calendars/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calendar calendar = await db.Calendars.FindAsync(id);
            if (calendar == null)
            {
                return HttpNotFound();
            }
            return View(calendar);
        }

        // POST: Calendars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Calendar calendar = await db.Calendars.FindAsync(id);
            db.Calendars.Remove(calendar);
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
