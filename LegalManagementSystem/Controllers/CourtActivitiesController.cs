using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Attorney, Advocate")]
    public class CourtActivitiesController : Controller
    {
        //private MyCaseNewEntities db = new MyCaseNewEntities();
        private ICourtActivity db;
        private IStaff staffRepo;
        private IMatter matterRepo;
        public CourtActivitiesController()
        {
            db = new CourtActivityRepository();
            staffRepo = new StaffRepository();
            matterRepo = new MatterRepository();
        }

        private static string mattrNumer = string.Empty;
        // GET: CourtActivities
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.GetCourtActivitiesAsync(x => x.Status =="Adjourned"));
                //return View(await db.CourtActivities.Where(x => x.Status == "Adjourned").ToListAsync());
            }
            return View(await db.GetCourtActivitiesAsync(x => x.CreatedBy.Equals(user) && x.Status == "Adjourned"));

        }
        //[Httpget]
        public async Task<ActionResult> AllCourtActivitiesForMatterNumber(string number)
        {
            /*
             
            //Server side parameters
            var start = Convert.ToInt32(Request["start"]);
            var length = Convert.ToInt32(Request["length"]);
            var searchValue = Request["search[value]"];
            var sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            var sortDirection = Request["order[0][dir]"];


            var courtActivities = new List<CourtActivityViewModel>();
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {


                var getCourtActivitiesAdmin = db.CourtActivities.ToList();
                foreach (var item in getCourtActivitiesAdmin)
                {
                    courtActivities.Add(new CourtActivityViewModel
                    {
                        CourtName = item.CourtName,
                        DateAdjourned =(DateTime)item.DateAdjourned,
                        DateHeared = item.DateHeared,
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
                    courtActivities = courtActivities.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower()) || x.AdvocateNote.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower())|| x.OpponentArgument.ToLower().Contains(searchValue.ToLower()) ||x.Location.ToLower().Contains(searchValue.ToLower()) || x.DefenseCounselName.ToLower().Contains(searchValue.ToLower()) || x.DateHeared.ToShortDateString().Contains(searchValue) || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //Sort Operations
                courtActivities = courtActivities.OrderBy(sortColumnName + " " + sortDirection).ToList();

                // Paging operation
                courtActivities = courtActivities.Skip(start).Take(length).ToList();

                return Json(new { data = courtActivities }, JsonRequestBehavior.AllowGet);
            }
            var getLineManagers = db.CourtActivities.Where(x => x.CreatedBy.Equals(user)).ToList();
            foreach (var item in getLineManagers)
            {
                courtActivities.Add(new CourtActivityViewModel
                {
                    CourtName = item.CourtName,
                    DateAdjourned = (DateTime)item.DateAdjourned,
                    DateHeared = item.DateHeared,
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
                courtActivities = courtActivities.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower()) || x.AdvocateNote.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower()) || x.OpponentArgument.ToLower().Contains(searchValue.ToLower()) || x.Location.ToLower().Contains(searchValue.ToLower()) || x.DefenseCounselName.ToLower().Contains(searchValue.ToLower()) || x.DateHeared.ToShortDateString().Contains(searchValue) || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            //Sort Operations
            courtActivities = courtActivities.OrderBy(sortColumnName + " " + sortDirection).ToList();

            // Paging operation
            courtActivities = courtActivities.Skip(start).Take(length).ToList();
            return Json(new { data = courtActivities }, JsonRequestBehavior.AllowGet); 
             
             
            */
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.GetCourtActivitiesAsync(x => x.MatterNumber==number && x.Status != "Strike Out"));
            }
            return View(await db.GetCourtActivitiesAsync(x => x.CreatedBy.Equals(user) && x.MatterNumber==number && x.Status != "Strike Out"));

        }
        public ActionResult GetCourtActivitiesByMatterNumber(string id)
        {
            var getCourtActivities = db.GetCourtActivity(x => x.MatterNumber==id && x.Status != "Strike Out");
            if (getCourtActivities ==null)
            {
                return RedirectToAction("Create", "CourtActivities",new { mattNumber = id });
            }
            else
            {
                return RedirectToAction("AllCourtActivitiesForMatterNumber", "CourtActivities", new { number = id });
            }
            //return View();
        }
        [HttpPost]
        public ActionResult GetCourtActivities()
        {
            //Server side parameters
            var start = Convert.ToInt32(Request["start"]);
            var length = Convert.ToInt32(Request["length"]);
            var searchValue = Request["search[value]"];
            var sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            var sortDirection = Request["order[0][dir]"];


            var courtActivities = new List<CourtActivityViewModel>();
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {


                var getCourtActivitiesAdmin = db.GetCourtActivities();
                foreach (var item in getCourtActivitiesAdmin)
                {
                    courtActivities.Add(new CourtActivityViewModel
                    {
                        CourtName = item.CourtName,
                        DateAdjourned =(DateTime)item.DateAdjourned,
                        DateHeared = item.DateHeared,
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
                    courtActivities = courtActivities.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower()) || x.AdvocateNote.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower())|| x.OpponentArgument.ToLower().Contains(searchValue.ToLower()) ||x.Location.ToLower().Contains(searchValue.ToLower()) || x.DefenseCounselName.ToLower().Contains(searchValue.ToLower()) || x.DateHeared.ToShortDateString().Contains(searchValue) || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //Sort Operations
                courtActivities = courtActivities.OrderBy(sortColumnName + " " + sortDirection).ToList();

                // Paging operation
                courtActivities = courtActivities.Skip(start).Take(length).ToList();

                return Json(new { data = courtActivities }, JsonRequestBehavior.AllowGet);
            }
            var getLineManagers = db.GetCourtActivities(x => x.CreatedBy.Equals(user));
            foreach (var item in getLineManagers)
            {
                courtActivities.Add(new CourtActivityViewModel
                {
                    CourtName = item.CourtName,
                    DateAdjourned = (DateTime)item.DateAdjourned,
                    DateHeared = item.DateHeared,
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
                courtActivities = courtActivities.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower()) || x.AdvocateNote.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower()) || x.OpponentArgument.ToLower().Contains(searchValue.ToLower()) || x.Location.ToLower().Contains(searchValue.ToLower()) || x.DefenseCounselName.ToLower().Contains(searchValue.ToLower()) || x.DateHeared.ToShortDateString().Contains(searchValue) || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            //Sort Operations
            courtActivities = courtActivities.OrderBy(sortColumnName + " " + sortDirection).ToList();

            // Paging operation
            courtActivities = courtActivities.Skip(start).Take(length).ToList();
            return Json(new { data = courtActivities }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStaffPassword(string password)
        {
            var user = User.Identity.Name;
            var email = staffRepo.GetStaffEmailByLoginName(user);
            var profilePassword = string.Empty;
            var staffRec = staffRepo.GetStaffByEmail(email) ;
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
            var courtActivity = await db.GetCourtActivityAsync(Convert.ToInt32(id));
            if (courtActivity == null)
            {
                return HttpNotFound();
            }
            return View(courtActivity);
        }

        [HttpPost]
        public async Task<JsonResult> SaveCourtActivity(CourtActivity courtActivity)
        {
            var user = User.Identity.Name;
            var email = staffRepo.GetStaffEmailByLoginName(user);
            var staffId = staffRepo.GetStaffIdByEmail(email);
            courtActivity.CreatedBy = user;

            courtActivity.CreatedOn = DateTime.Today;
            courtActivity.MatterNumber = mattrNumer;
            courtActivity.StaffId = staffId;
            var status = courtActivity.Status;

            var matter = matterRepo.GetMatterByMatterNumber(mattrNumer);//.Ge db.Matters.FirstOrDefault(x => x.MatterNumber == mattrNumer);
            if (matter != null)
            {
                    matter.CourtStatus = "YES";
                    matter.MatterStage = status;
                
            }
            matterRepo.UpdateMatter(matter);
            matterRepo.Complete();

            db.AddCourtActivity(courtActivity);

            await db.CompleteAsync();
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        //private string MatterNumber { get; set; }
        // GET: CourtActivities/Create
        public ActionResult Create(string mattNumber)
        {
            //ViewBag.MatterNumber = id;
            ViewBag.Status = new List<SelectListItem>{
                new SelectListItem { Value="Adjourned",Text="Adjourned"},
                new SelectListItem { Value="Dismissed",Text="Dismissed"},
                new SelectListItem { Value="Judgement Delivered",Text="Judgement Delivered"},
                new SelectListItem { Value="Strike Out",Text="Strike Out"} 
            };
            mattrNumer = mattNumber;
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

                var email = staffRepo.GetStaffEmailByLoginName(user);
                var staffId = staffRepo.GetStaffIdByEmail(email);

                courtActivity.MatterNumber = mattrNumer;// ViewBag.MatterNumber;// LegalGuideUtility.MatterId;
                courtActivity.StaffId = staffId;

                

                var matter = matterRepo.GetMatterByMatterNumber( mattrNumer);
                if (matter !=null)
                {
                    matter.CourtStatus = "YES";
                }
                matterRepo.UpdateMatter(matter);
                matterRepo.Complete();

                db.AddCourtActivity(courtActivity);

                await db.CompleteAsync();
                return RedirectToAction("Index","Matters");
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
            var getData = matterRepo.GetMatterForDropDowns(); //db.GetAllMattersForDropDown().ToList();

            //if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            //{
            if (searchKey != null)
            {
                getData = matterRepo.GetMatterForDropDowns().Where(x => x.Subject.Contains(searchKey)).ToList();
                //getData = db.GetAllMattersForDropDown().Where(x => x.Subject.Contains(searchKey)).ToList();
            }
            var ModifiedData = getData.Select(x => new
            {
                id = x.MatterNumber,
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
                var staffIdInStaffMatters = staffRepo.StaffId(LegalGuideUtility.MatterId);
                //var staffIdInStaffMatters = db.StaffMatters.Where(x => x.MatterNumber.Equals(LegalGuideUtility.MatterId)).Distinct().FirstOrDefault().StaffId;
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
            var courtActivity = await db.GetCourtActivityAsync(Convert.ToInt32(id));
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
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CourtActivity courtActivity)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                var email = staffRepo.GetStaffEmailByLoginName(user);
                var staffId = staffRepo.GetStaffIdByEmail(email);
                //courtActivity.CreatedBy 
                courtActivity.ModifiedBy = user;
                courtActivity.ModifiedOn = DateTime.Today;
                courtActivity.StaffId = staffId;
                var matterNumber = courtActivity.MatterNumber;
                var status = courtActivity.Status;
                var matter = matterRepo.GetMatterByMatterNumber(matterNumber);//.Ge db.Matters.FirstOrDefault(x => x.MatterNumber == mattrNumer);
                if (matter != null)
                {
                    matter.CourtStatus = "YES";
                    matter.MatterStage = status;

                }
                matterRepo.UpdateMatter(matter);
                matterRepo.Complete();
                //db.Entry(courtActivity).State = EntityState.Modified;
                db.UpdateCourtActivity(courtActivity);
                await db.CompleteAsync();
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
            var courtActivity = await db.GetCourtActivityAsync(Convert.ToInt32(id));
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
            var courtActivity = await db.GetCourtActivityAsync(id);
            db.DeleteCourtActivity(courtActivity);
            await db.CompleteAsync();
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
