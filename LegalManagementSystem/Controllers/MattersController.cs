using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.Repositories;
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
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR) || IsSelectedForTheCase(staffId))
                {
                    return View(await matterRepo.GetMattersIncludeClientAsync(x => x.MatterStage != "Closed" && x.MatterStage != "Dismissed" && x.MatterStage != "Judgement Delivered" && x.MatterStage != "Strike Out"));
                }
                //if (IsSelectedForTheCase(staffId))
                //{
                //    return View(await matterRepo.GetMattersIncludeClientAsync(x => x.MatterStage != "Closed" && x.MatterStage != "Dismissed" && x.MatterStage != "Judgement Delivered" && x.MatterStage != "Strike Out"));
                //}
                return View(await matterRepo.GetMattersIncludeClientAsync(x => x.MatterStage != "Closed" && x.MatterStage != "Dismissed" && x.MatterStage != "Judgement Delivered" && x.MatterStage != "Strike Out" && x.CreatedBy.Equals(user)));
            }
            catch (Exception ex)
            {

                LegalGuideUtility.ErrorMessage = "Error Occured." + Environment.NewLine + ex.Message + Environment.NewLine + "Please Contact IT Department. ";
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetMattersData()
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

                    var adminMatters = await matterRepo.GetMattersIncludeClientAsync(x => x.MatterStage != "Closed" && x.MatterStage != "Dismissed" && x.MatterStage != "Judgement Delivered" && x.MatterStage != "Strike Out");

                    var returnedMatters = adminMatters.Select(s => new MatterViewModel
                    {
                        MatterNumber = s.MatterNumber,
                        DueDate = s.DueDate.ToShortDateString(),
                        FiledOn = s.FiledOn.ToShortDateString(),
                        Subject = s.Subject,
                        ClientName = s.Client.FirstName + " " + s.Client.LastName,
                        MatterStage = s.MatterStage,
                        Description = s.Description,
                        PracticeArea = s.AreaOfPractice

                    });

                    List<MatterViewModel> matterDTO = returnedMatters.ToList();

                    //Sorting
                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                    {
                        if (sortColumnIndex==0)
                        {
                            matterDTO = sortColumnDir == "asc" ? matterDTO.OrderBy(o => o.Subject).ToList() : matterDTO.OrderByDescending(o => o.Subject).ToList();
                        }
                        else if (sortColumnIndex==1)
                        {
                            matterDTO = sortColumnDir == "asc" ? matterDTO.OrderBy(o => o.Description).ToList() : matterDTO.OrderByDescending(o => o.Description).ToList();
                        }
                        else if (sortColumnIndex==2)
                        {
                            matterDTO = sortColumnDir == "asc" ? matterDTO.OrderBy(o => o.PracticeArea).ToList() : matterDTO.OrderByDescending(o => o.PracticeArea).ToList();
                        }
                        else if (sortColumnIndex == 3)
                        {
                            matterDTO = sortColumnDir == "asc" ? matterDTO.OrderBy(o => o.DueDate).ToList() : matterDTO.OrderByDescending(o => o.DueDate).ToList();
                        }
                        else if (sortColumnIndex == 4)
                        {
                            matterDTO = sortColumnDir == "asc" ? matterDTO.OrderBy(o => o.FiledOn).ToList() : matterDTO.OrderByDescending(o => o.FiledOn).ToList();
                        }
                        else
                        {
                            matterDTO = sortColumnDir == "asc" ? matterDTO.OrderBy(o => o.MatterStage).ToList() : matterDTO.OrderByDescending(o => o.MatterStage).ToList();
                        }

                    }
                    //Searching
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        matterDTO = matterDTO.Where(x => x.Subject.ToLower().Contains(searchValue.ToLower())
                                                                || x.Description.ToLower().Contains(searchValue.ToLower())
                                                                || x.PracticeArea.ToLower().Contains(searchValue.ToLower())
                                                                || x.MatterStage.ToLower().Contains(searchValue.ToLower())).ToList();
                    }

                    //total number of row count
                    recordsTotal = matterDTO.Count();
                    //Paging
                    var aData = matterDTO.Skip(skip).Take(pageSize).ToList();

                    return Json(new
                    {
                        draw=draw,
                        iTotalRecords=recordsTotal,
                        iTotalDisplayRecords=recordsTotal,
                        aaData=aData
                    },JsonRequestBehavior.AllowGet);
                }

                // Other Users
                var matters= await matterRepo.GetMattersIncludeClientAsync(x => x.MatterStage != "Closed" && x.MatterStage != "Dismissed" && x.MatterStage != "Judgement Delivered" && x.MatterStage != "Strike Out" && x.CreatedBy==user);

                var rMatters = matters.Select(s => new MatterViewModel
                {
                    MatterNumber = s.MatterNumber,
                    DueDate = s.DueDate.ToShortDateString(),// ? s.DueDate.Value.ToString("dd-MM-yyyy"):DateTime.Now.ToShortDateString(),
                    FiledOn = s.FiledOn.ToShortDateString(),// ? s.DueDate.Value.ToString("dd-MM-yyyy") :DateTime.Now.ToShortDateString(),
                    Subject = s.Subject,
                    ClientName = s.Client.FirstName + " " + s.Client.LastName,
                    MatterStage = s.MatterStage,
                    Description = s.Description,
                    PracticeArea = s.AreaOfPractice

                });

                if (!string.IsNullOrEmpty(searchValue))
                {
                    rMatters = rMatters.Where(x => x.Subject.ToLower().Contains(searchValue.ToLower())
                                                            || x.Description.ToLower().Contains(searchValue.ToLower())
                                                            || x.PracticeArea.ToLower().Contains(searchValue.ToLower())
                                                            || x.MatterStage.ToLower().Contains(searchValue.ToLower())).ToList();
                }
                

                if (sortColumnIndex == 0)
                {
                    rMatters = sortDirection == "asc" ? rMatters.OrderBy(x => x.Subject) : rMatters.OrderByDescending(x => x.Subject);
                }
                else if (sortColumnIndex == 1)
                {
                    rMatters = sortDirection == "asc" ? rMatters.OrderBy(x => x.Description) : rMatters.OrderByDescending(x => x.Description);
                }
                else if (sortColumnIndex == 2)
                {
                    rMatters = sortDirection == "asc" ? rMatters.OrderBy(x => x.PracticeArea) : rMatters.OrderByDescending(x => x.PracticeArea);
                }
                else if (sortColumnIndex == 3)
                {
                    rMatters = sortDirection == "asc" ? rMatters.OrderBy(x => x.DueDate) : rMatters.OrderByDescending(x => x.DueDate);
                }
                else if (sortColumnIndex == 4)
                {
                    rMatters = sortDirection == "asc" ? rMatters.OrderBy(x => x.FiledOn) : rMatters.OrderByDescending(x => x.FiledOn);
                }
                else
                {
                    rMatters = sortDirection == "asc" ? rMatters.OrderBy(x => x.MatterStage) : rMatters.OrderByDescending(x => x.MatterStage);
                }
                //total number of row count
                recordsTotal = rMatters.Count();
                //Paging
                var rData = rMatters.Skip(skip).Take(pageSize).ToList();

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
        
        [HttpPost]
        public JsonResult AddAssignee(string data, string matterNumber)
        {
            //List<string> assigneeList = new List<string>();
            var assigneeStaffList = data.Split(',');
            var staffMatter = new StaffMatter();
            var subject = "Your are been assigned a this case";
            var body = "<p>You here by notify that you are assigned to the case. Please check your dashboard for details</p>";

            foreach (var staffId in assigneeStaffList)
            {
                staffMatter.MatterNumber = matterNumber;
                staffMatter.StaffId = staffId;
                try
                {
                    var email = staffRepo.GetEmailByStaffId(staffId);
                    staffMatterRepo.AddStaffMatter(staffMatter);
                    //db.StaffMatters.Add(staffMatter);
                    staffMatterRepo.Complete();
                    //LegalGuideUtility.SendEmail(email, subject, body);
                    //db.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
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
            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName");
            return View();
        }
        // GET: Matters/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                //return Json(0, JsonRequestBehavior.AllowGet);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var matter = await matterRepo.GetMatterAsync(id);
            if (matter == null)
            {
                //return Json(0, JsonRequestBehavior.AllowGet);
                return HttpNotFound();
            }

            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
            //return Json(0, JsonRequestBehavior.AllowGet);
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
            
            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
            //return View(matter);
            return Json(matter, JsonRequestBehavior.AllowGet);

        }

        // POST: Matters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Edit(Matter matter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = User.Identity.Name;
                    matter.ModifiedBy = user;
                    matter.ModifiedOn = DateTime.Today;
                    matterRepo.UpdateMatter(matter);
                    //await db.SaveChangesAsync();
                    await matterRepo.CompleteAsync();
                    return Json(matter, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index");
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
                ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
                return Json(matter, JsonRequestBehavior.AllowGet);
                //return View(matter);
            }
            ViewBag.ClientId = new SelectList(clientRepo.GetClients(), "ClientId", "FirstName", matter.ClientId);
            return Json(matter, JsonRequestBehavior.AllowGet);
            //return View(matter);
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
                clientRepo.Dispose();
                staffRepo.Dispose();
                managerRepo.Dispose();
                staffMatterRepo.Dispose();
            }
            base.Dispose(disposing);
            matterRepo.Dispose();
            clientRepo.Dispose();
            staffRepo.Dispose();
            managerRepo.Dispose();
            staffMatterRepo.Dispose();


        }
    }

}


