using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.Repositories;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Attorney, Advocate")]
    public class MattersController : Controller
    {
        //private MyCaseNewEntities db = new MyCaseNewEntities();
        private IMatter matterRepo;
        private IClient clientRepo;
        private IStaff staffRepo;
        private ILineManager managerRepo;
        private IStaffMatter staffMatterRepo;
        public MattersController()
        {
            matterRepo = new MatterRepository();
            clientRepo = new ClientRepository();
            staffRepo = new StaffRepository();
            managerRepo = new LineManagerRepository();
            staffMatterRepo = new StaffMatterRepository();
        }
        // GET: Matters
        public async Task<ActionResult> Index()
        {
            //var matters = db.Matters.Include(m => m.Client);
            //return View(await matters.ToListAsync());

            try
            {
                var user = User.Identity.Name;
                var email = staffRepo.GetStaffEmailByLoginName(user);
                var staffId = staffRepo.GetStaffIdByEmail(email);
                if (string.IsNullOrEmpty(staffId))
                {
                    LegalGuideUtility.ErrorMessage = "Staff not Registered. Please Contact IT Department";
                    return RedirectToAction("Error");
                }
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                {
                    return View(await matterRepo.GetMattersIncludeClientAsync(x => x.MatterStage != "Closed" && x.MatterStage != "Dismissed" && x.MatterStage != "Judgement Delivered" && x.MatterStage != "Strike Out"));
                }
                if (IsSelectedForTheCase(staffId))
                {
                    return View(await matterRepo.GetMattersIncludeClientAsync(x => x.MatterStage != "Closed" && x.MatterStage != "Dismissed" && x.MatterStage != "Judgement Delivered" && x.MatterStage != "Strike Out"));
                }
                return View(await matterRepo.GetMattersIncludeClientAsync(x => x.MatterStage != "Closed" && x.MatterStage != "Dismissed" && x.MatterStage != "Judgement Delivered" && x.MatterStage != "Strike Out" && x.CreatedBy.Equals(user)));
            }
            catch (Exception)
            {

                LegalGuideUtility.ErrorMessage = "Error Occured. Please Contact IT Department";
                return RedirectToAction("Error");
            }
        }
        public JsonResult GetClientForDropDown(string searchKey)
        {
            //var getData = context.GetAllAdvocateGroups().ToList();
            var getData = clientRepo.GetAllClientNameForDropDown();
            //var getData = db.GetAllClientForDropDown().ToList();
            //var data = null;

            if (searchKey != null)
            {
                getData = getData.Where(x => x.ClientName.Contains(searchKey)); // db.GetAllClientForDropDown().Where(x => x.ClientName.Contains(searchKey)).ToList();
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
            var getData = staffRepo.GetAllStaffForDropDown(); //db.GetAllStaffForDropDown().ToList();
            //var data = null;

            if (searchKey != null)
            {
                getData = getData.Where(x => x.StaffName.Contains(searchKey)); //db.GetAllStaffForDropDown().Where(x => x.StaffName.Contains(searchKey)).ToList();
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
            var getData = managerRepo.GetAllManagersForDropDown();
            //var getData = db.sp_GetLineManagers().ToList();
            //var data = null;

            if (searchKey != null)
            {
                getData = getData.Where(x => x.ManagerName.Contains(searchKey)); //db.sp_GetLineManagers().Where(x => x.ManagerName.Contains(searchKey)).ToList();
            }
            var ModifiedData = getData.Select(x => new
            {
                id = x.ManagerId,
                text = x.ManagerName
            });

            return Json(ModifiedData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMatterForEvents()
        {
            try
            {
                //var adminFileEvents
                matterRepo.ConFigProxy();
                //db.Configuration.ProxyCreationEnabled = false;
                var user = User.Identity.Name;
                var email = staffRepo.GetStaffEmailByLoginName(user);
                var staffId = staffRepo.GetStaffIdByEmail(email);

                //var staffIdInStaffMatters = db.StaffMatters.Where(x => x.MatterNumber.Equals(LegalGuideUtility.MatterId)).Distinct();

                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                {
                    var eventForAdmin = matterRepo.GetMatters();
                    //var eventForAdmin = db.Matters.ToList();
                    List<EventForView> events = new List<EventForView>();
                    foreach (var item in eventForAdmin)
                    {
                        events.Add(new EventForView
                        {
                            Title = item.Subject,
                            Start = (DateTime)item.FiledOn,
                            Description = item.Description

                        });
                    }
                    return Json(events, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<EventForView> events = new List<EventForView>();
                    var eventsForUser = matterRepo.GetMatters(x => x.CreatedBy.Equals(user) || IsSelectedForTheCase(staffId)); //db.Matters.Where(x => x.CreatedBy.Equals(user) || IsSelectedForTheCase(staffId)).ToList();
                    //var eventsForUser = db.Matters.Where(x => x.CreatedBy.Equals(user) || IsSelectedForTheCase(staffId)).ToList();
                    foreach (var item in eventsForUser)
                    {
                        events.Add(new EventForView
                        {
                            Title = item.Subject,
                            Start = (DateTime)item.FiledOn,
                            //End = (DateTime)item.DueDate,
                            Description = item.Description

                        });
                    }
                    return Json(events, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private bool IsSelectedForTheCase(string staffId)
        {
            try
            {

                var staffIdInStaffMatters = staffMatterRepo.StaffIdInStaffMatters(LegalGuideUtility.MatterId);
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
        public ActionResult GetMatters()
        {
            //Server side parameters
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            List<MatterViewModel> matters = new List<MatterViewModel>();
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                var getMattersAdmin = matterRepo.GetMatters();// db.Matters.ToList<Matter>();
                foreach (var item in getMattersAdmin)
                {
                    matters.Add(new MatterViewModel
                    {
                        MatterStage = item.MatterStage,
                        FiledOn = (DateTime)item.FiledOn,
                        DueDate = (DateTime)item.DueDate,
                        MatterNumber = item.MatterNumber,
                        AreaofPractice = item.AreaOfPractice,
                        ArrivalDate = (DateTime)item.ArrivalDate,
                        Priority = item.Priority,
                        Description = item.Description,
                        Subject = item.Subject
                    });
                }
                //Search operations
                if (!string.IsNullOrEmpty(searchValue))
                {
                    matters = matters.Where(x => x.MatterNumber.ToLower().Contains(searchValue.ToLower()) || x.MatterStage.ToLower().Contains(searchValue.ToLower()) || x.Priority.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower()) || x.AreaofPractice.ToLower().Contains(searchValue.ToLower()) || x.Description.ToLower().Contains(searchValue.ToLower()) || x.Subject.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //Sort Operations
                //matters = matters.OrderBy(sortColumnName + " " + sortDirection).ToList<MatterViewModel>();

                // Paging operation
                matters = matters.Skip(start).Take(length).ToList();

                return Json(new { data = matters }, JsonRequestBehavior.AllowGet);
            }
            var getMatters = matterRepo.GetMatters(x => x.CreatedBy.Equals(user));// db.Matters.Where(x => x.CreatedBy.Equals(user)).ToList<Matter>();
            foreach (var item in getMatters)
            {
                matters.Add(new MatterViewModel
                {
                    MatterStage = item.MatterStage,
                    FiledOn = (DateTime)item.FiledOn,
                    DueDate = (DateTime)item.DueDate,
                    MatterNumber = item.MatterNumber,
                    AreaofPractice = item.AreaOfPractice,
                    ArrivalDate = (DateTime)item.ArrivalDate,
                    Priority = item.Priority,
                    Description = item.Description,
                    Subject = item.Subject
                });
            }
            //Searching Operations
            if (!string.IsNullOrEmpty(searchValue))
            {
                matters = matters.Where(x => x.MatterNumber.ToLower().Contains(searchValue.ToLower()) || x.MatterStage.ToLower().Contains(searchValue.ToLower()) || x.Priority.ToLower().Contains(searchValue.ToLower()) || x.MatterNumber.ToLower().Contains(searchValue.ToLower()) || x.AreaofPractice.ToLower().Contains(searchValue.ToLower()) || x.Description.ToLower().Contains(searchValue.ToLower()) || x.Subject.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            //Sort Operations
            //matters = matters.OrderBy(sortColumnName + " " + sortDirection).ToList<MatterViewModel>();

            // Paging operation
            matters = matters.Skip(start).Take(length).ToList();
            return Json(new { data = matters }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddAssignee(string data, string matterNumber)
        {
            //List<string> assigneeList = new List<string>();
            var assigneeStaffList = data.Split(',');
            var staffMatter = new StaffMatter();
            foreach (var staffId in assigneeStaffList)
            {
                staffMatter.MatterNumber = matterNumber;
                staffMatter.StaffId = staffId;
                staffMatterRepo.AddStaffMatter(staffMatter);
                //db.StaffMatters.Add(staffMatter);
                staffMatterRepo.Complete();
                //db.SaveChanges();
            }
            return Json(0, JsonRequestBehavior.AllowGet);
        }
        // GET: Matters/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Matter matter = await matterRepo.GetMatterAsync(id);// db.Matters.FindAsync(id);
            if (matter == null)
            {
                return HttpNotFound();
            }
            return View(matter);
        }

        [HttpPost]
        public async Task<JsonResult> SaveCase(Matter matter)
        {
            try
            {
                var nextId = matterRepo.GetCurrentId() + 1;

                var matterId = "CASE-" + nextId + "-" + DateTime.Today.ToShortDateString();

                var user = User.Identity.Name;
                //var matter = new Matter { 
                matter.CreatedBy = user;
                matter.CreatedOn = DateTime.Today;
                matter.CourtStatus = "NO";
                matter.CaseNumber = matterId;

                //matter.CaseNumber = matterId; //"01_05-03-2019";
                LegalGuideUtility.MatterId = matter.MatterNumber;

                //db.Matters.Add(matter);
                matterRepo.AddMatter(matter);
                await matterRepo.CompleteAsync();
                //await db.SaveChangesAsync();

                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                ViewBag.Error = "Can't Add Matter, Error Occured. Please Contact IT Department.";
            }

            return Json(0, JsonRequestBehavior.AllowGet);

        }
        // GET: Matters/Create
        public ActionResult Create()
        {
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.Stage = new List<SelectListItem>{
                new SelectListItem { Value="Discovery",Text="Discovery"},
                new SelectListItem { Value="Trial",Text="Trial"},
                new SelectListItem { Value="Apeal",Text="Apeal"},
                new SelectListItem { Value="Motions",Text="Motions"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Pleading",Text="Pleading"}
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
            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName");
            //ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName");
            return View();
        }

        // POST: Matters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Subject,Description,AreaOfPractice,ClientId,LineManagerId,ArrivalDate,FiledOn,DueDate,MatterNumber,Priority,MatterStage,RequestedBy,MatterValue,EstimatedEffort,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,CaseNumber")] Matter matter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nextId = matterRepo.GetCurrentId() + 1;
                    var matterId = "CASE-" + nextId + "-" + DateTime.Today.ToShortDateString();
                    var user = User.Identity.Name;
                    matter.CreatedBy = user;
                    matter.CreatedOn = DateTime.Today;
                    matter.CaseNumber = matterId; //"01_05-03-2019";
                    LegalGuideUtility.MatterId = matter.MatterNumber;
                    //matter.ass
                    matter.CourtStatus = "NO";

                    //db.Matters.Add(matter);
                    matterRepo.AddMatter(matter);
                    //await db.SaveChangesAsync();
                    await matterRepo.CompleteAsync();

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.Error = "Can't Add Matter, Error Occured. Please Contact IT Department.";
                }
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
                ViewBag.Stage = new List<SelectListItem>{
                new SelectListItem { Value="Discovery",Text="Discovery"},
                new SelectListItem { Value="Trial",Text="Trial"},
                new SelectListItem { Value="Apeal",Text="Apeal"},
                new SelectListItem { Value="Motions",Text="Motions"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Pleading",Text="Pleading"}
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
                ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
                //ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FirstName", matter.ClientId);
                return View(matter);
            }
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.Stage = new List<SelectListItem>{
                new SelectListItem { Value="Discovery",Text="Discovery"},
                new SelectListItem { Value="Trial",Text="Trial"},
                new SelectListItem { Value="Apeal",Text="Apeal"},
                new SelectListItem { Value="Motions",Text="Motions"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Pleading",Text="Pleading"}
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
            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
            return View(matter);

        }
        // GET: Matters/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var matter = await matterRepo.GetMatterAsync(id);
            if (matter == null)
            {
                return HttpNotFound();
            }
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.Stage = new List<SelectListItem>{
                new SelectListItem { Value="Discovery",Text="Discovery"},
                new SelectListItem { Value="Trial",Text="Trial"},
                new SelectListItem { Value="Apeal",Text="Apeal"},
                new SelectListItem { Value="Motions",Text="Motions"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Pleading",Text="Pleading"}
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
            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
            return View(matter);
        }


        [HttpPost]
        public async Task<JsonResult> EditCase(string id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            var matter = await matterRepo.GetMatterAsync(id);
            if (matter == null)
            {
                //return HttpNotFound();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.Stage = new List<SelectListItem>{
                new SelectListItem { Value="Discovery",Text="Discovery"},
                new SelectListItem { Value="Trial",Text="Trial"},
                new SelectListItem { Value="Apeal",Text="Apeal"},
                new SelectListItem { Value="Motions",Text="Motions"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Pleading",Text="Pleading"}
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
            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
            //return View(matter);
            return Json(matter, JsonRequestBehavior.AllowGet);

        }

        // POST: Matters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Matter matter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = User.Identity.Name;
                    matter.ModifiedBy = user;
                    matter.ModifiedOn = DateTime.Today;

                    //db.Entry(matter).State = EntityState.Modified;
                    matterRepo.UpdateMatter(matter);
                    //await db.SaveChangesAsync();
                    await matterRepo.CompleteAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.Error = "Can't Add Matter, Error Occured. Please Contact IT Department.";
                    //throw ex;
                }
                //db.Entry(matter).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
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
                ViewBag.Stage = new List<SelectListItem>{
                new SelectListItem { Value="Discovery",Text="Discovery"},
                new SelectListItem { Value="Trial",Text="Trial"},
                new SelectListItem { Value="Apeal",Text="Apeal"},
                new SelectListItem { Value="Motions",Text="Motions"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Pleading",Text="Pleading"}
            };
                ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
                return View(matter);
            }
            ViewBag.Priority = new List<SelectListItem>{
                new SelectListItem { Value="Critical",Text="Critical"},
                new SelectListItem { Value="High",Text="High"},
                new SelectListItem { Value="Medium",Text="Medium"},
                new SelectListItem { Value="Low",Text="Low"}
            };
            ViewBag.Stage = new List<SelectListItem>{
                new SelectListItem { Value="Discovery",Text="Discovery"},
                new SelectListItem { Value="Trial",Text="Trial"},
                new SelectListItem { Value="Apeal",Text="Apeal"},
                new SelectListItem { Value="Motions",Text="Motions"},
                new SelectListItem { Value="Closed",Text="Closed"},
                new SelectListItem { Value="Pleading",Text="Pleading"}
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
            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
            return View(matter);
        }
        // GET: Matters/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var matter = await matterRepo.GetMatterAsync(id);
            if (matter == null)
            {
                return HttpNotFound();
            }
            return View(matter);
        }
        // POST: Matters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var matter = await matterRepo.GetMatterAsync(id);
            //db.Matters.Remove(matter);
            matterRepo.DeleteMatter(matter);
            //await db.SaveChangesAsync();
            await matterRepo.CompleteAsync();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                matterRepo.Dispose();
            }
            base.Dispose(disposing);
            matterRepo.Dispose();

            
        }
    }

}


