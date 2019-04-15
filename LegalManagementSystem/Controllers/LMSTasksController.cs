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
    [Authorize(Roles = "Admin,Attorney,Advocate")]
    public class LMSTasksController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: LMSTasks
        public async Task<ActionResult> Index()
        {
            
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.LMSTasks.ToListAsync());
            }
            return View(await db.LMSTasks.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: LMSTasks/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LMSTask lMSTask = await db.LMSTasks.FindAsync(id);
            if (lMSTask == null)
            {
                return HttpNotFound();
            }
            return View(lMSTask);
        }

        // GET: LMSTasks/Create
        public ActionResult Create()
        {
            ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
            ViewBag.Priority =new  List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            return View();
        }

        // POST: LMSTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,TaskType,Description,MatterNumber,NotifyDays,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Priority,Reporter,Assignee,DueDate")] LMSTask lMSTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = User.Identity.Name;
                    lMSTask.CreatedBy = user;
                    lMSTask.CreatedOn = DateTime.Today;

                    db.LMSTasks.Add(lMSTask);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.Error = "Can't Add Task, Error Occured. Please Contact IT Department.";
                }
                
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
                ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
                ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
                return View(lMSTask);
            }
            ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            return View(lMSTask);
        }

        // GET: LMSTasks/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LMSTask lMSTask = await db.LMSTasks.FindAsync(id);
            ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            if (lMSTask == null)
            {
                return HttpNotFound();
            }
            return View(lMSTask);
        }

        // POST: LMSTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,TaskType,Description,MatterNumber,NotifyDays,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,Priority,Reporter,Assignee,DueDate")] LMSTask lMSTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = User.Identity.Name;
                    lMSTask.ModifiedBy = user;
                    lMSTask.ModifiedOn = DateTime.Today;

                    db.Entry(lMSTask).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.Error = "Can't Add Task, Error Occured. Please Contact IT Department.";
                }
                
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
                ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
                ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
                return View(lMSTask);
            }
            ViewBag.Matter = new SelectList(db.GetAllMattersForDropDown(), "Id", "Subject");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown(), "StaffId", "StaffName");
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            return View(lMSTask);
        }

        // GET: LMSTasks/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LMSTask lMSTask = await db.LMSTasks.FindAsync(id);
            if (lMSTask == null)
            {
                return HttpNotFound();
            }
            return View(lMSTask);
        }

        // POST: LMSTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LMSTask lMSTask = await db.LMSTasks.FindAsync(id);
            db.LMSTasks.Remove(lMSTask);
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
