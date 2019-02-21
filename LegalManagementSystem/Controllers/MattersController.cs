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
    public class MattersController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Matters
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.Matters.ToListAsync());
            }
            return View(await db.Matters.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Matters/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matter matter = await db.Matters.FindAsync(id);
            if (matter == null)
            {
                return HttpNotFound();
            }
            return View(matter);
        }

        // GET: Matters/Create
        public ActionResult Create()
        {
            ViewBag.Client = new SelectList( db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
            return View();
        }

        // POST: Matters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Subject,Description,AreaOfPractice,ClientId,StaffId,Assignee,ArrivalDate,FiledOn,DueDate,MatterNumber,Priority,MatterStage,RequestedBy,MatterValue,EstimatedEffort,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Matter matter)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var user = User.Identity.Name;
                    matter.CreatedBy = user;
                    matter.CreatedOn = DateTime.Today;


                    db.Matters.Add(matter);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index","Home");
                }
                catch (Exception)
                {

                    ViewBag.Error = "Can't Add Matter, Error Occured. Please Contact IT Department.";
                }
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
                ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
                return View(matter);
            }
            ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
            return View(matter);
        }

        // GET: Matters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
            Matter matter = await db.Matters.FindAsync(id);
            if (matter == null)
            {
                return HttpNotFound();
            }
            return View(matter);
        }

        // POST: Matters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Subject,Description,AreaOfPractice,ClientId,StaffId,Assignee,ArrivalDate,FiledOn,DueDate,MatterNumber,Priority,MatterStage,RequestedBy,MatterValue,EstimatedEffort,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Matter matter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = User.Identity.Name;
                    matter.ModifiedBy = user;
                    matter.ModifiedOn = DateTime.Today;

                    db.Entry(matter).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.Error = "Can't Add Matter, Error Occured. Please Contact IT Department.";
                    //throw ex;
                }
                
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
                ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
                return View(matter);
            }
            ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
            return View(matter);
        }

        // GET: Matters/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matter matter = await db.Matters.FindAsync(id);
            if (matter == null)
            {
                return HttpNotFound();
            }
            return View(matter);
        }

        // POST: Matters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Matter matter = await db.Matters.FindAsync(id);
            db.Matters.Remove(matter);
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
