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
using System.Linq.Dynamic;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Attorney, Advocate")]
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

        }
        public ActionResult GetCourtActivities()
        {
            //Server side parameters
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            List<CourtActivityViewModel> courtActivities = new List<CourtActivityViewModel>();
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {


                var getCourtActivitiesAdmin = db.CourtActivities.ToList<CourtActivity>();
                foreach (var item in getCourtActivitiesAdmin)
                {
                    courtActivities.Add(new CourtActivityViewModel
                    {
                        CourtName = item.CourtName,
                        DateAdjourned =(DateTime)item.DateAdjourned,
                        DateHeared =(DateTime) item.DateHeared,
                        MatterNumber=item.MatterNumber,
                        Location=item.Location,
                        Status=item.Status,
                        AdvocateArgument=item.AdvocateArgument,
                        OpponentArgument=item.OpponentArgument,
                        DefenseCounselName=item.DefenseCounselName,
                        AdvocateNote=item.AdvocateNote
                    });
                }
                //Search operations
                if (!string.IsNullOrEmpty(searchValue))
                {
                    courtActivities = courtActivities.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower()) || x.AdvocateNote.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower())|| x.OpponentArgument.ToLower().Contains(searchValue.ToLower()) ||x.Location.ToLower().Contains(searchValue.ToLower()) || x.DefenseCounselName.ToLower().Contains(searchValue.ToLower()) || x.DateHeared.ToShortDateString().Contains(searchValue) || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())).ToList<CourtActivityViewModel>();
                }

                //Sort Operations
                courtActivities = courtActivities.OrderBy(sortColumnName + " " + sortDirection).ToList<CourtActivityViewModel>();

                // Paging operation
                courtActivities = courtActivities.Skip(start).Take(length).ToList<CourtActivityViewModel>();

                return Json(new { data = courtActivities }, JsonRequestBehavior.AllowGet);
            }
            var getLineManagers = db.CourtActivities.Where(x => x.CreatedBy.Equals(user)).ToList<CourtActivity>();
            foreach (var item in getLineManagers)
            {
                courtActivities.Add(new CourtActivityViewModel
                {
                    CourtName = item.CourtName,
                    DateAdjourned = (DateTime)item.DateAdjourned,
                    DateHeared = (DateTime)item.DateHeared,
                    MatterNumber = item.MatterNumber,
                    Location = item.Location,
                    Status = item.Status,
                    AdvocateArgument = item.AdvocateArgument,
                    OpponentArgument = item.OpponentArgument,
                    DefenseCounselName = item.DefenseCounselName,
                    AdvocateNote = item.AdvocateNote
                });
            }
            //Searching Operations
            if (!string.IsNullOrEmpty(searchValue))
            {
                courtActivities = courtActivities.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower()) || x.AdvocateNote.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower()) || x.OpponentArgument.ToLower().Contains(searchValue.ToLower()) || x.Location.ToLower().Contains(searchValue.ToLower()) || x.DefenseCounselName.ToLower().Contains(searchValue.ToLower()) || x.DateHeared.ToShortDateString().Contains(searchValue) || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())).ToList<CourtActivityViewModel>();
            }

            //Sort Operations
            courtActivities = courtActivities.OrderBy(sortColumnName + " " + sortDirection).ToList<CourtActivityViewModel>();

            // Paging operation
            courtActivities = courtActivities.Skip(start).Take(length).ToList<CourtActivityViewModel>();
            return Json(new { data = courtActivities }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStaffPassword(string password)
        {
            var user = User.Identity.Name;
            var email = LegalGuideUtility.GetStaffEmailByLoginName(user);
            string profilePassword = string.Empty;
            var staffRec = db.Staffs.Where(x => x.EmailAddress.Equals(email)).FirstOrDefault();
            if (staffRec !=null)
            {
                profilePassword = staffRec.Password;
            }
            if (profilePassword.Equals(password))
            {
                return RedirectToAction("Create");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        public ActionResult Error()
        {
            return View();
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
            //ViewBag.MatterNumber = id;
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjourned",Text="Adjourned"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"},
                new SelectListItem { Value="Judgement Delivered",Text="Judgement Delivered"},
                new SelectListItem { Value="Strike Out",Text="Strike Out"}
                
                
            };
            return View();
        }

        // POST: CourtActivities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,MatterNumber,DateHeared,CourtName,Location,StaffId,Status,AdvocateArgument,OpponentArgument,AdvocateNote,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,DateAdjourned,DefenseCounselName")] CourtActivity courtActivity)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                courtActivity.CreatedBy = user;
                courtActivity.CreatedOn = DateTime.Today;

                var email = LegalGuideUtility.GetStaffEmailByLoginName(user);
                var staffId = LegalGuideUtility.GetStaffIdByEmail(email);

                //courtActivity.MatterNumber = ViewBag.MatterNumber;
                courtActivity.StaffId = staffId;

                db.CourtActivities.Add(courtActivity);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
        }
        ViewBag.Status = new List<SelectListItem>{
            new SelectListItem { Value="Adjourned",Text="Adjourned"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"},
                new SelectListItem { Value="Judgement Delivered",Text="Judgement Delivered"},
                new SelectListItem { Value="Strike Out",Text="Strike Out"}
            };


            return View(courtActivity);
        }
        public JsonResult GetMatterNumber(string matterNumber)
        {
            LegalGuideUtility.MatterId = matterNumber;
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMatterForDropDown(string searchKey)
        {
            //var user = User.Identity.Name;
            //var email = LegalGuideUtility.GetStaffEmailByLoginName(user);
            //var staffId = LegalGuideUtility.GetStaffIdByEmail(email);
            var getData = db.GetAllMattersForDropDown().ToList();

            //if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            //{
            if (searchKey != null)
            {
                getData = db.GetAllMattersForDropDown().Where(x => x.Subject.Contains(searchKey)).ToList();
            }
            var ModifiedData = getData.Select(x => new
            {
                id = x.Id,
                text = x.Subject
            });

            return Json(ModifiedData, JsonRequestBehavior.AllowGet);
        //}
            //return null;
            //var data = null;

            
        }
        private bool IsSelectedForTheCase(string staffId)
        {
            try
            {
                var staffIdInStaffMatters = db.StaffMatters.Where(x => x.MatterNumber.Equals(LegalGuideUtility.MatterId)).Distinct().FirstOrDefault().StaffId;
                if (staffId == staffIdInStaffMatters)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
                new SelectListItem { Value="Adjourned",Text="Adjourned"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"},
                new SelectListItem { Value="Judgement Delivered",Text="Judgement Delivered"},
                new SelectListItem { Value="Strike Out",Text="Strike Out"}
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,MatterNumber,DateHeared,CourtName,Location,StaffId,Status,AdvocateArgument,OpponentArgument,AdvocateNote,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,DateAdjourned,DefenseCounselName")] CourtActivity courtActivity)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                var email = LegalGuideUtility.GetStaffEmailByLoginName(user);
                var staffId = LegalGuideUtility.GetStaffIdByEmail(email);
                //courtActivity.CreatedBy 
                courtActivity.ModifiedBy = user;
                courtActivity.ModifiedOn = DateTime.Today;
                courtActivity.StaffId = staffId;

                db.Entry(courtActivity).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjourned",Text="Adjourned"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"},
                new SelectListItem { Value="Judgement Delivered",Text="Judgement Delivered"},
                new SelectListItem { Value="Strike Out",Text="Strike Out"}
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
