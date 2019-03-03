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
    [Authorize(Roles = "Admin, Attorney, Advocate")]
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
        public JsonResult GetClientForDropDown(string searchKey)
        {
            //var getData = context.GetAllAdvocateGroups().ToList();
            var getData = db.GetAllClientForDropDown().ToList();
            //var data = null;

            if (searchKey != null)
            {
                getData = db.GetAllClientForDropDown().Where(x => x.ClientName.Contains(searchKey)).ToList();
            }
            var ModifiedData = getData.Select(x => new
            {
                id = x.ClientId,
                text = x.ClientName
            });

            return Json(ModifiedData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStaffForDropDown(string searchKey)
        {
            //var getData = context.GetAllAdvocateGroups().ToList();
            var getData = db.GetAllStaffForDropDown().ToList();
            //var data = null;

            if (searchKey != null)
            {
                getData = db.GetAllStaffForDropDown().Where(x => x.StaffName.Contains(searchKey)).ToList();
            }
            var ModifiedData = getData.Select(x => new
            {
                id = x.StaffId,
                text = x.StaffName
            });

            return Json(ModifiedData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLineManagerForDropDown(string searchKey)
        {
            //var getData = context.GetAllAdvocateGroups().ToList();
            var getData = db.sp_GetLineManagers().ToList();
            //var data = null;

            if (searchKey != null)
            {
                getData = db.sp_GetLineManagers().Where(x => x.ManagerName.Contains(searchKey)).ToList();
            }
            var ModifiedData = getData.Select(x => new
            {
                id = x.LineManagerId,
                text = x.ManagerName
            });

            return Json(ModifiedData, JsonRequestBehavior.AllowGet);
        }
        // GET: Matters/Create
        public ActionResult Create()
        {
            //ViewBag.Client = new SelectList( db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
            //ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
            //ViewBag.LineManager = new SelectList(db.sp_GetLineManagers().ToList(), "LineManagerId", "ManagerName");
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.PracticeArea = new List<SelectListItem>{
                new SelectListItem { Value="None",Text="None"},
                new SelectListItem { Value="Acquisition",Text="Acquisition"},
                new SelectListItem { Value="Administrative",Text="Administrative"},
                new SelectListItem { Value="Audit",Text="Audit"},
                new SelectListItem { Value="Civil",Text="Civil"},
                new SelectListItem { Value="Commercial",Text="Commercial"},
                new SelectListItem { Value="Consultation",Text="Consultation"},
                new SelectListItem { Value="Corporate",Text="Corporate"},
                new SelectListItem { Value="Criminal",Text="Criminal"},
                new SelectListItem { Value="Dispute",Text="Dispute"},
                new SelectListItem { Value="Due Deligence",Text="Due Deligence"},
                new SelectListItem { Value="Labour",Text="Labour"},
                new SelectListItem { Value="Real Estate",Text="Real Estate"},
                new SelectListItem { Value="Sharia",Text="Sharia"},
                new SelectListItem { Value="Agreement",Text="Agreement"}
            };
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
                ViewBag.LineManager = new SelectList(db.sp_GetLineManagers().ToList(), "LineManagerId", "ManagerName");
                ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
                ViewBag.PracticeArea = new List<SelectListItem>{
                new SelectListItem { Value="None",Text="None"},
                new SelectListItem { Value="Acquisition",Text="Acquisition"},
                new SelectListItem { Value="Administrative",Text="Administrative"},
                new SelectListItem { Value="Audit",Text="Audit"},
                new SelectListItem { Value="Civil",Text="Civil"},
                new SelectListItem { Value="Commercial",Text="Commercial"},
                new SelectListItem { Value="Consultation",Text="Consultation"},
                new SelectListItem { Value="Corporate",Text="Corporate"},
                new SelectListItem { Value="Criminal",Text="Criminal"},
                new SelectListItem { Value="Dispute",Text="Dispute"},
                new SelectListItem { Value="Due Deligence",Text="Due Deligence"},
                new SelectListItem { Value="Labour",Text="Labour"},
                new SelectListItem { Value="Real Estate",Text="Real Estate"},
                new SelectListItem { Value="Sharia",Text="Sharia"},
                new SelectListItem { Value="Agreement",Text="Agreement"}
            };
                return View(matter);
            }
            ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
            ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
            ViewBag.LineManager = new SelectList(db.sp_GetLineManagers().ToList(), "LineManagerId", "ManagerName");
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.PracticeArea = new List<SelectListItem>{
                new SelectListItem { Value="None",Text="None"},
                new SelectListItem { Value="Acquisition",Text="Acquisition"},
                new SelectListItem { Value="Administrative",Text="Administrative"},
                new SelectListItem { Value="Audit",Text="Audit"},
                new SelectListItem { Value="Civil",Text="Civil"},
                new SelectListItem { Value="Commercial",Text="Commercial"},
                new SelectListItem { Value="Consultation",Text="Consultation"},
                new SelectListItem { Value="Corporate",Text="Corporate"},
                new SelectListItem { Value="Criminal",Text="Criminal"},
                new SelectListItem { Value="Dispute",Text="Dispute"},
                new SelectListItem { Value="Due Deligence",Text="Due Deligence"},
                new SelectListItem { Value="Labour",Text="Labour"},
                new SelectListItem { Value="Real Estate",Text="Real Estate"},
                new SelectListItem { Value="Sharia",Text="Sharia"},
                new SelectListItem { Value="Agreement",Text="Agreement"}
            };
            return View(matter);
        }

        // GET: Matters/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
            //ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
            //ViewBag.LineManager = new SelectList(db.sp_GetLineManagers().ToList(), "LineManagerId", "ManagerName");
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.PracticeArea = new List<SelectListItem>{
                new SelectListItem { Value="None",Text="None"},
                new SelectListItem { Value="Acquisition",Text="Acquisition"},
                new SelectListItem { Value="Administrative",Text="Administrative"},
                new SelectListItem { Value="Audit",Text="Audit"},
                new SelectListItem { Value="Civil",Text="Civil"},
                new SelectListItem { Value="Commercial",Text="Commercial"},
                new SelectListItem { Value="Consultation",Text="Consultation"},
                new SelectListItem { Value="Corporate",Text="Corporate"},
                new SelectListItem { Value="Criminal",Text="Criminal"},
                new SelectListItem { Value="Dispute",Text="Dispute"},
                new SelectListItem { Value="Due Deligence",Text="Due Deligence"},
                new SelectListItem { Value="Labour",Text="Labour"},
                new SelectListItem { Value="Real Estate",Text="Real Estate"},
                new SelectListItem { Value="Sharia",Text="Sharia"},
                new SelectListItem { Value="Agreement",Text="Agreement"}
            };
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
                //ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
                //ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
                //ViewBag.LineManager = new SelectList(db.sp_GetLineManagers().ToList(), "LineManagerId", "ManagerName");
                ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
                ViewBag.PracticeArea = new List<SelectListItem>{
                new SelectListItem { Value="None",Text="None"},
                new SelectListItem { Value="Acquisition",Text="Acquisition"},
                new SelectListItem { Value="Administrative",Text="Administrative"},
                new SelectListItem { Value="Audit",Text="Audit"},
                new SelectListItem { Value="Civil",Text="Civil"},
                new SelectListItem { Value="Commercial",Text="Commercial"},
                new SelectListItem { Value="Consultation",Text="Consultation"},
                new SelectListItem { Value="Corporate",Text="Corporate"},
                new SelectListItem { Value="Criminal",Text="Criminal"},
                new SelectListItem { Value="Dispute",Text="Dispute"},
                new SelectListItem { Value="Due Deligence",Text="Due Deligence"},
                new SelectListItem { Value="Labour",Text="Labour"},
                new SelectListItem { Value="Real Estate",Text="Real Estate"},
                new SelectListItem { Value="Sharia",Text="Sharia"},
                new SelectListItem { Value="Agreement",Text="Agreement"}
            };
                return View(matter);
            }
            //ViewBag.Client = new SelectList(db.GetAllClientForDropDown().ToList(), "ClientId", "ClientName");
            //ViewBag.Staff = new SelectList(db.GetAllStaffForDropDown().ToList(), "StaffId", "StaffName");
            //ViewBag.LineManager = new SelectList(db.sp_GetLineManagers().ToList(), "LineManagerId", "ManagerName");
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.PracticeArea = new List<SelectListItem>{
                new SelectListItem { Value="None",Text="None"},
                new SelectListItem { Value="Acquisition",Text="Acquisition"},
                new SelectListItem { Value="Administrative",Text="Administrative"},
                new SelectListItem { Value="Audit",Text="Audit"},
                new SelectListItem { Value="Civil",Text="Civil"},
                new SelectListItem { Value="Commercial",Text="Commercial"},
                new SelectListItem { Value="Consultation",Text="Consultation"},
                new SelectListItem { Value="Corporate",Text="Corporate"},
                new SelectListItem { Value="Criminal",Text="Criminal"},
                new SelectListItem { Value="Dispute",Text="Dispute"},
                new SelectListItem { Value="Due Deligence",Text="Due Deligence"},
                new SelectListItem { Value="Labour",Text="Labour"},
                new SelectListItem { Value="Real Estate",Text="Real Estate"},
                new SelectListItem { Value="Sharia",Text="Sharia"},
                new SelectListItem { Value="Agreement",Text="Agreement"}
            };
            return View(matter);
        }
        [HttpPost]
        public JsonResult AddAssignee()
        {
            return Json("", JsonRequestBehavior.AllowGet);
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
