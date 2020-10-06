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
        private ICourtActivity courtRepo;
        private IStaff staffRepo;
        private IMatter matterRepo;
        private IJudgement judgementRepo;
        public CourtActivitiesController()
        {
            courtRepo = new CourtActivityRepository();
            staffRepo = new StaffRepository();
            matterRepo = new MatterRepository();
            judgementRepo = new JudgementRepository();
        }

        private static string mattrNumer = string.Empty;
        // GET: CourtActivities
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            var courts = await courtRepo.GetCourtActivitiesAsync(x => x.Status == "Adjourned");
            var returnedCourts = courts.Select(s => new CourtActivityVM
            {
                AdvocateArgument = s.AdvocateArgument,
                AdvocateNote = s.AdvocateNote,
                CourtName = s.CourtName,
                DateAdjourned = s.DateAdjourned.HasValue ? s.DateAdjourned.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToShortDateString(),
                DateHeared = s.DateHeared.ToShortDateString(),
                DefenseCounselName = s.DefenseCounselName,
                Location = s.Location,
                OpponentArgument = s.OpponentArgument,
                Status = s.Status,
                Id = s.Id,
                CreatedBy=s.CreatedBy
            });
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                
                return View(returnedCourts);
                //return View(await db.CourtActivities.Where(x => x.Status == "Adjourned").ToListAsync());
            }
            //await courtRepo.GetCourtActivitiesAsync(x => x.CreatedBy.Equals(user) && x.Status == "Adjourned")
            return View(returnedCourts.Where(x => x.CreatedBy.Equals(user)));

        }
        [HttpPost]
        public async Task<ActionResult> AllCourtActivitiesForMatterNumber(string number)
        {
            var user = User.Identity.Name;
            var courts = await courtRepo.GetCourtActivitiesAsync(x => x.Status == "Adjourned");
            var returnedCourts = courts.Select(s => new CourtActivityViewModel
            {
                AdvocateArgument = s.AdvocateArgument,
                AdvocateNote = s.AdvocateNote,
                CourtName = s.CourtName,
                DateAdjourned = s.DateAdjourned.HasValue ? s.DateAdjourned.Value.ToString("dd/MM/yyyy") : DateTime.Now.ToShortDateString(),
                DateHeared = s.DateHeared.ToShortDateString(),
                DefenseCounselName = s.DefenseCounselName,
                Location = s.Location,
                OpponentArgument = s.OpponentArgument,
                Status = s.Status,
                Id = s.Id,
                CreatedBy = s.CreatedBy
            });
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {

                return View(returnedCourts);
            }
            return View(returnedCourts.Where(x => x.CreatedBy.Equals(user)));

        }
        //[HttpPost]
        public ActionResult GetCourtActivitiesByMatterNumber(string id)
        {
            try
            {
                var getCourtActivity = courtRepo.GetCourtActivity(x => x.MatterNumber == id && x.Status != LegalGuideUtility.StrikeOut && x.Status != LegalGuideUtility.JudgementDelivered && x.Status != LegalGuideUtility.Dismissed);
                if (getCourtActivity ==null || getCourtActivity.MatterNumber==id)
                {
                return RedirectToAction("Create", "CourtActivities", new { mattNumber = id });
                }
                return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            //else
            //{
                //return RedirectToAction("AllCourtActivitiesForMatterNumber", "CourtActivities", new { number = id });
            //}
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


                var getCourtActivitiesAdmin = courtRepo.GetCourtActivities();
                foreach (var item in getCourtActivitiesAdmin)
                {
                    courtActivities.Add(new CourtActivityViewModel
                    {
                        CourtName = item.CourtName,
                        DateAdjourned = item.DateAdjourned.HasValue ? item.DateAdjourned.Value.ToString("dd-MM-yyyy") : DateTime.Now.ToShortDateString(),
                        DateHeared = item.DateHeared.ToShortDateString(),
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
                    courtActivities = courtActivities.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower()) || x.AdvocateNote.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower())|| x.OpponentArgument.ToLower().Contains(searchValue.ToLower()) ||x.Location.ToLower().Contains(searchValue.ToLower()) || x.DefenseCounselName.ToLower().Contains(searchValue.ToLower()) || x.DateHeared.Contains(searchValue) || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //Sort Operations
                courtActivities = courtActivities.OrderBy(sortColumnName + " " + sortDirection).ToList();

                // Paging operation
                courtActivities = courtActivities.Skip(start).Take(length).ToList();

                return Json(new { data = courtActivities }, JsonRequestBehavior.AllowGet);
            }
            var getLineManagers = courtRepo.GetCourtActivities(x => x.CreatedBy.Equals(user));
            foreach (var item in getLineManagers)
            {
                courtActivities.Add(new CourtActivityViewModel
                {
                    CourtName = item.CourtName,
                    DateAdjourned = item.DateAdjourned.HasValue ? item.DateAdjourned.Value.ToString("dd/MM/yyyy"):DateTime.Now.ToShortDateString(),
                    DateHeared = item.DateHeared.ToString("dd/MM/yyyy"),
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
                courtActivities = courtActivities.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower()) || x.AdvocateNote.ToLower().Contains(searchValue.ToLower()) || x.Status.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower()) || x.OpponentArgument.ToLower().Contains(searchValue.ToLower()) || x.Location.ToLower().Contains(searchValue.ToLower()) || x.DefenseCounselName.ToLower().Contains(searchValue.ToLower()) || x.DateHeared.Contains(searchValue) || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())).ToList();
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
                profilePassword = staffRec.StaffId;
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
            var courtActivity = await courtRepo.GetCourtActivityAsync(Convert.ToInt32(id));
            if (courtActivity == null)
            {
                return HttpNotFound();
            }
            return View(courtActivity);
        }

        [HttpPost]
        public async Task<JsonResult> GetCourtsData()
        {
            //System User
            var user = User.Identity.Name;

            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //Sort Column index
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];
            //Search
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            try
            {

                var email = staffRepo.GetStaffEmailByLoginName(user);
                var staffId = staffRepo.GetStaffIdByEmail(email);

                if (string.IsNullOrEmpty(staffId))
                {
                    LegalGuideUtility.ErrorMessage = "Staff not Registered. Please Contact IT Department";
                    return Json(0, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Error");
                }


                //Admin Section
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR) || IsSelectedForTheCase(staffId))
                {

                    var adminCourt = await courtRepo.GetCourtActivitiesAsync(x => x.Status == "Adjourned");

                    var returnedCourts = adminCourt.Select(s => new CourtActivityViewModel
                    {
                        MatterNumber = s.MatterNumber,
                        OpponentArgument=s.OpponentArgument,
                        AdvocateArgument=s.AdvocateArgument,
                        CourtName=s.CourtName,
                        DefenseCounselName=s.DefenseCounselName,
                        AdvocateNote=s.AdvocateNote,
                        DateAdjourned= s.DateAdjourned.HasValue ? s.DateAdjourned.Value.ToString("dd-MM-yyyy") : DateTime.Now.ToShortDateString(),
                        DateHeared = s.DateHeared.ToString("dd-MM-yyyy"),
                        Location = s.Location,
                        Status = s.Status

                    });

                    List<CourtActivityViewModel> courtDTO = returnedCourts.ToList();

                    //Sorting
                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                    {
                        if (sortColumnIndex == 0)
                        {
                            courtDTO = sortColumnDir == "asc" ? courtDTO.OrderBy(o => o.CourtName).ToList() : courtDTO.OrderByDescending(o => o.CourtName).ToList();
                        }
                        else if (sortColumnIndex == 1)
                        {
                            courtDTO = sortColumnDir == "asc" ? courtDTO.OrderBy(o => o.AdvocateArgument).ToList() : courtDTO.OrderByDescending(o => o.AdvocateArgument).ToList();
                        }
                        else if (sortColumnIndex == 2)
                        {
                            courtDTO = sortColumnDir == "asc" ? courtDTO.OrderBy(o => o.OpponentArgument).ToList() : courtDTO.OrderByDescending(o => o.OpponentArgument).ToList();
                        }
                        else if (sortColumnIndex == 3)
                        {
                            courtDTO = sortColumnDir == "asc" ? courtDTO.OrderBy(o => o.DateHeared).ToList() : courtDTO.OrderByDescending(o => o.DateHeared).ToList();
                        }
                        else if (sortColumnIndex == 4)
                        {
                            courtDTO = sortColumnDir == "asc" ? courtDTO.OrderBy(o => o.DateAdjourned).ToList() : courtDTO.OrderByDescending(o => o.DateAdjourned).ToList();
                        }
                        else
                        {
                            courtDTO = sortColumnDir == "asc" ? courtDTO.OrderBy(o => o.Location).ToList() : courtDTO.OrderByDescending(o => o.Location).ToList();
                        }

                    }
                    //Searching
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        courtDTO = courtDTO.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower())
                                                                || x.Location.ToLower().Contains(searchValue.ToLower())
                                                                || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())
                                                                || x.OpponentArgument.ToLower().Contains(searchValue.ToLower())).ToList();
                    }

                    //total number of row count
                    recordsTotal = courtDTO.Count();
                    //Paging
                    var aData = courtDTO.Skip(skip).Take(pageSize).ToList();

                    return Json(new
                    {
                        draw = draw,
                        iTotalRecords = recordsTotal,
                        iTotalDisplayRecords = recordsTotal,
                        aaData = aData
                    }, JsonRequestBehavior.AllowGet);
                }

                // Other Users 
                var courts = await courtRepo.GetCourtActivitiesAsync(x => x.Status == "Adjourned" && x.CreatedBy == user);

                var rCourts = courts.Select(s => new CourtActivityViewModel
                {
                    MatterNumber = s.MatterNumber,
                    OpponentArgument = s.OpponentArgument,
                    AdvocateArgument = s.AdvocateArgument,
                    CourtName = s.CourtName,
                    DefenseCounselName = s.DefenseCounselName,
                    AdvocateNote = s.AdvocateNote,
                    DateAdjourned = s.DateAdjourned.HasValue ? s.DateAdjourned.Value.ToString("dd-MM-yyyy") : DateTime.Now.ToShortDateString(),
                    DateHeared = s.DateHeared.ToString("dd-MM-yyyy"),
                    Location = s.Location,
                    Status = s.Status

                });

                //Searching
                if (!string.IsNullOrEmpty(searchValue))
                {
                    rCourts = rCourts.Where(x => x.CourtName.ToLower().Contains(searchValue.ToLower())
                                                            || x.Location.ToLower().Contains(searchValue.ToLower())
                                                            || x.AdvocateArgument.ToLower().Contains(searchValue.ToLower())
                                                            || x.OpponentArgument.ToLower().Contains(searchValue.ToLower())).ToList();
                }


                //Sorting
                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    if (sortColumnIndex == 0)
                    {
                        rCourts = sortColumnDir == "asc" ? rCourts.OrderBy(o => o.CourtName).ToList() : rCourts.OrderByDescending(o => o.CourtName).ToList();
                    }
                    else if (sortColumnIndex == 1)
                    {
                        rCourts = sortColumnDir == "asc" ? rCourts.OrderBy(o => o.AdvocateArgument).ToList() : rCourts.OrderByDescending(o => o.AdvocateArgument).ToList();
                    }
                    else if (sortColumnIndex == 2)
                    {
                        rCourts = sortColumnDir == "asc" ? rCourts.OrderBy(o => o.OpponentArgument).ToList() : rCourts.OrderByDescending(o => o.OpponentArgument).ToList();
                    }
                    else if (sortColumnIndex == 3)
                    {
                        rCourts = sortColumnDir == "asc" ? rCourts.OrderBy(o => o.DateHeared).ToList() : rCourts.OrderByDescending(o => o.DateHeared).ToList();
                    }
                    else if (sortColumnIndex == 4)
                    {
                        rCourts = sortColumnDir == "asc" ? rCourts.OrderBy(o => o.DateAdjourned).ToList() : rCourts.OrderByDescending(o => o.DateAdjourned).ToList();
                    }
                    else
                    {
                        rCourts = sortColumnDir == "asc" ? rCourts.OrderBy(o => o.Location).ToList() : rCourts.OrderByDescending(o => o.Location).ToList();
                    }

                }
                //total number of row count
                recordsTotal = rCourts.Count();
                //Paging
                var rData = rCourts.Skip(skip).Take(pageSize).ToList();

                return Json(new
                {
                    draw = draw,
                    iTotalRecords = recordsTotal,
                    iTotalDisplayRecords = recordsTotal,
                    aaData = rData
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SaveCourtActivity(CourtActivity courtActivity)
        {
            var user = User.Identity.Name;
            var email = staffRepo.GetStaffEmailByLoginName(user);
            var staffId = staffRepo.GetStaffIdByEmail(email);
            courtActivity.CreatedBy = user;

            courtActivity.AdvocateArgument = LegalGuideUtility.RemoveHTMLTags(courtActivity.AdvocateArgument);

            courtActivity.AdvocateNote = LegalGuideUtility.RemoveHTMLTags(courtActivity.AdvocateNote);
            courtActivity.Judgement = LegalGuideUtility.RemoveHTMLTags(courtActivity.Judgement);

            courtActivity.OpponentArgument = LegalGuideUtility.RemoveHTMLTags(courtActivity.OpponentArgument);


            courtActivity.CreatedOn = DateTime.Today;
            courtActivity.MatterNumber = mattrNumer;
            courtActivity.StaffId = staffId;
            var status = courtActivity.Status;


            //var judgement = new Judgement();
            var matter = matterRepo.GetMatterByMatterNumber(mattrNumer);//.Ge db.Matters.FirstOrDefault(x => x.MatterNumber == mattrNumer);
            if (matter != null)
            {
                    matter.CourtStatus = "YES";
                    matter.MatterStage = status;


                //Save to Judgement table if judgement is delivered
                if (status == "Judgement Delivered")
                {
                    var judgement=new Judgement
                    {
                        CourtName = courtActivity.CourtName,
                        DateDelivered = courtActivity.DateHeared,

                        JudgementPassed = courtActivity.Judgement,

                        CaseTitle = matter.Subject
                    }; //courtActivity.Matter.Subject;
                    //Commit
                    judgementRepo.AddJudgement(judgement);
                    judgementRepo.Complete();
                }

            }
            //Update the case 
            matterRepo.UpdateMatter(matter);
            matterRepo.Complete();
            
            //Add the new court activity
            courtRepo.AddCourtActivity(courtActivity);

            await courtRepo.CompleteAsync();
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        //private string MatterNumber { get; set; }
        // GET: CourtActivities/Create
        public ActionResult Create(string mattNumber)
        {
            mattrNumer = mattNumber;
            return View();
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
            var courtActivity = await courtRepo.GetCourtActivityAsync(Convert.ToInt32(id));
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
                courtRepo.UpdateCourtActivity(courtActivity);
                await courtRepo.CompleteAsync();
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
            var courtActivity = await courtRepo.GetCourtActivityAsync(Convert.ToInt32(id));
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
            var courtActivity = await courtRepo.GetCourtActivityAsync(id);
            courtRepo.DeleteCourtActivity(courtActivity);
            await courtRepo.CompleteAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                courtRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
