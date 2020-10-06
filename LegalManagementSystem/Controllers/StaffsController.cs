using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.Repositories;
using LegalManagementSystem.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Attorney,Advocate")]
    public class StaffsController : Controller
    {
        //private MyCaseNewEntities db = new MyCaseNewEntities();
        private IStaff staffRepo;
        private IMatter matterRepo;
        private ILineManager lineManagerRepo;
        private IAdvocateGroup advocateGroupRepo;
        public StaffsController()
        {
            staffRepo = new StaffRepository();
            matterRepo = new MatterRepository();
            lineManagerRepo = new LineManagerRepository();
            advocateGroupRepo = new AdvocateGroupRepository();
        }
        // GET: Staffs
        public async Task<ActionResult> Index()
        {
            
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await staffRepo.GetStaffsAsync());
                //return View(await db.Staffs.ToListAsync());
            }
            return View(await staffRepo.GetStaffsAsync(x => x.CreatedBy == user));
            //return View(await db.Staffs.Where(x => x.CreatedBy.Equals(user)).ToListAsync());

        }

        [HttpPost]
        public async Task<JsonResult> GetStaffRecords()
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
                var staffRecords = await staffRepo.GetStaffsAsync();

                //Admin Section
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                {

                    var returnedStaffs = staffRecords.Select(s => new StaffViewModel
                    {
                        StaffId = s.StaffId,
                        FirstName = s.FirstName, 
                        LastName = s.LastName, 
                         DOB= s.DOB.ToShortDateString(),
                        DOE = s.DOE.ToShortDateString(), 
                        Address = s.Address,
                        EmailAddress = s.EmailAddress,
                        MobileNo = s.MobileNo

                    });

                    List<StaffViewModel> staffDTO = returnedStaffs.ToList();

                    //Sorting
                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                    {
                        if (sortColumnIndex == 0)
                        {
                            staffDTO = sortColumnDir == "asc" ? staffDTO.OrderBy(o => o.FirstName).ToList() : staffDTO.OrderByDescending(o => o.FirstName).ToList();
                        }
                        else if (sortColumnIndex == 1)
                        {
                            staffDTO = sortColumnDir == "asc" ? staffDTO.OrderBy(o => o.LastName).ToList() : staffDTO.OrderByDescending(o => o.LastName).ToList();
                        }
                        else if (sortColumnIndex == 2)
                        {
                            staffDTO = sortColumnDir == "asc" ? staffDTO.OrderBy(o => o.Address).ToList() : staffDTO.OrderByDescending(o => o.Address).ToList();
                        }
                        else if (sortColumnIndex == 3)
                        {
                            staffDTO = sortColumnDir == "asc" ? staffDTO.OrderBy(o => o.DOB).ToList() : staffDTO.OrderByDescending(o => o.DOB).ToList();
                        }
                        else if (sortColumnIndex == 4)
                        {
                            staffDTO = sortColumnDir == "asc" ? staffDTO.OrderBy(o => o.DOE).ToList() : staffDTO.OrderByDescending(o => o.DOE).ToList();
                        }
                        else if (sortColumnIndex == 4)
                        {
                            staffDTO = sortColumnDir == "asc" ? staffDTO.OrderBy(o => o.MobileNo).ToList() : staffDTO.OrderByDescending(o => o.MobileNo).ToList();
                        }
                        else
                        {
                            staffDTO = sortColumnDir == "asc" ? staffDTO.OrderBy(o => o.EmailAddress).ToList() : staffDTO.OrderByDescending(o => o.EmailAddress).ToList();
                        }

                    }
                    //Searching
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        staffDTO = staffDTO.Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower())
                                                                || x.LastName.ToLower().Contains(searchValue.ToLower()) 
                                                                || x.Address.ToLower().Contains(searchValue.ToLower()) 
                                                                || x.MobileNo.ToLower().Contains(searchValue.ToLower()) 
                                                                || x.EmailAddress.ToLower().Contains(searchValue.ToLower())).ToList();
                    }

                    //total number of row count
                    recordsTotal = staffDTO.Count();
                    //Paging
                    var aData = staffDTO.Skip(skip).Take(pageSize).ToList();

                    return Json(new
                    {
                        draw = draw,
                        iTotalRecords = recordsTotal,
                        iTotalDisplayRecords = recordsTotal,
                        aaData = aData
                    }, JsonRequestBehavior.AllowGet);
                }

                // Other Users
                var staffs = staffRecords.Where(x => x.CreatedBy == user);

                var rStaffs = staffs.Select(s => new StaffViewModel
                {
                    StaffId = s.StaffId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    DOB = s.DOB.ToShortDateString(),
                    DOE = s.DOE.ToShortDateString(),
                    Address = s.Address,
                    EmailAddress = s.EmailAddress,
                    MobileNo = s.MobileNo

                });

                if (!string.IsNullOrEmpty(searchValue))
                {
                    rStaffs = rStaffs.Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower())
                                                                || x.LastName.ToLower().Contains(searchValue.ToLower())
                                                                || x.Address.ToLower().Contains(searchValue.ToLower())
                                                                || x.MobileNo.ToLower().Contains(searchValue.ToLower())
                                                                || x.EmailAddress.ToLower().Contains(searchValue.ToLower())).ToList();
                }
                    if (sortColumnIndex == 0)
                    {
                    rStaffs = sortColumnDir == "asc" ? rStaffs.OrderBy(o => o.FirstName).ToList() : rStaffs.OrderByDescending(o => o.FirstName).ToList();
                    }
                    else if (sortColumnIndex == 1)
                    {
                    rStaffs = sortColumnDir == "asc" ? rStaffs.OrderBy(o => o.LastName).ToList() : rStaffs.OrderByDescending(o => o.LastName).ToList();
                    }
                    else if (sortColumnIndex == 2)
                    {
                    rStaffs = sortColumnDir == "asc" ? rStaffs.OrderBy(o => o.Address).ToList() : rStaffs.OrderByDescending(o => o.Address).ToList();
                    }
                    else if (sortColumnIndex == 3)
                    {
                    rStaffs = sortColumnDir == "asc" ? rStaffs.OrderBy(o => o.DOB).ToList() : rStaffs.OrderByDescending(o => o.DOB).ToList();
                    }
                    else if (sortColumnIndex == 4)
                    {
                    rStaffs = sortColumnDir == "asc" ? rStaffs.OrderBy(o => o.DOE).ToList() : rStaffs.OrderByDescending(o => o.DOE).ToList();
                    }
                    else if (sortColumnIndex == 4)
                    {
                    rStaffs = sortColumnDir == "asc" ? rStaffs.OrderBy(o => o.MobileNo).ToList() : rStaffs.OrderByDescending(o => o.MobileNo).ToList();
                    }
                    else
                    {
                    rStaffs = sortColumnDir == "asc" ? rStaffs.OrderBy(o => o.EmailAddress).ToList() : rStaffs.OrderByDescending(o => o.EmailAddress).ToList();
                    }
                //total number of row count
                recordsTotal = rStaffs.Count();
                //Paging
                var rData = rStaffs.Skip(skip).Take(pageSize).ToList();

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

        public JsonResult GetAdvocateGroupForDropDown(string searchKey)
        {
            var getData = matterRepo.AdvocateGroup();
            //var getData = db.GetAllAdvocateGroups().ToList();

            if (searchKey != null)
            {
                getData = getData.Where(x => x.GroupName.Contains(searchKey)).ToList();
                //getData = db.GetAllAdvocateGroups().Where(x => x.GroupName.Contains(searchKey)).ToList();
            }
            var ModifiedData = getData.Select(x => new
            {
                id = x.Id,
                text = x.GroupName
            });

            return Json(ModifiedData, JsonRequestBehavior.AllowGet);
        }

        // GET: Staffs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var gender = string.Empty;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = await staffRepo.GetStaffAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            var lineManger = lineManagerRepo.GetLineManager(staff.LineManagerId);
            var advocateGroup = advocateGroupRepo.GetAdvocateGroup(staff.AdvocateGroupId);

           gender= (staff.Gender == "M") ? "Male" : "Female";
           ViewBag.AdvocateGroup = advocateGroup !=null ? advocateGroup.GroupName :string.Empty;

            ViewBag.LineManager = lineManger !=null ? lineManger.Name:string.Empty;
            ViewBag.Gender = gender;

            return View(staff);
        }
        //Save Staff Record
        [HttpPost]
        public JsonResult SaveStaffRecord()//StaffVM model
        {
            try
            {
                string fileName = string.Empty;
                string filePath = string.Empty;

                //HttpPostedFileBase file = model.File;
                var file = System.Web.HttpContext.Current.Request.Files["file"];

                //if (file.ContentLength <= 0 && file == null)
                //{
                //    ViewBag.Error = " please select image to continue.";
                //    return Json(file, JsonRequestBehavior.AllowGet);
                //}

                filePath = file.FileName;
                fileName = Path.GetFileName(file.FileName);
                //"~/Content/Images/staff_images/
                var folderPath = "~/Content/Images/staff_images/";
                var fullFilePath = Path.Combine(folderPath, filePath);
                file.SaveAs(fullFilePath);

                var staffId = string.Empty;
                var user = User.Identity.Name;
                //Get model from the view
                var model = JsonConvert.DeserializeObject<Staff>(Request.Form["model"].ToString());

                staffId = model.StaffId.ToUpper();
                LegalGuideUtility.StaffId = staffId;
                model.CreatedBy = user;
                model.CreatedOn = DateTime.Today;
                model.ImagePath = fileName;
                model.Status = true;
                model.AdvocateGroupId = 0;
                staffRepo.AddStaff(model);
                staffRepo.Complete(); 
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't Save Profile please check and try again." + ex.Message;
                var error = ex.InnerException.Message;
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Staffs/Create
        public ActionResult Create()
        {
            return View();
        }

        // GET: Staffs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var staff = await staffRepo.GetStaffAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        [HttpPost]
        public JsonResult EditStaffDetail()
        {
            try
            {
                string fileName = string.Empty;
                string filePath = string.Empty;

                //HttpPostedFileBase file = model.File;
                var file = System.Web.HttpContext.Current.Request.Files["file"];

                //Get model from the view
                var model = JsonConvert.DeserializeObject<Staff>(Request.Form["model"].ToString());

                if (file != null)
                {
                    filePath = file.FileName;
                    fileName = Path.GetFileName(file.FileName);

                    var folderPath = "~/Content/Images/staff_images/";
                    var fullFilePath = Path.Combine(folderPath, filePath);

                    file.SaveAs(Server.MapPath(fullFilePath));
                    model.ImagePath = fileName;
                }

                

                var staffId = string.Empty;
                var user = User.Identity.Name;
                

                staffId = model.StaffId.ToUpper();
                LegalGuideUtility.StaffId = staffId;
                model.ModifiedBy = user;
                model.ModifiedOn = DateTime.Today;
                
                model.Status = true;
                model.AdvocateGroupId = 0;
                //Update Details
                staffRepo.UpdateStaff(model);
                staffRepo.Complete();
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't Update Profile please check and try again." + ex.Message;
                //var error = ex.InnerException.Message;
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Staffs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = await staffRepo.GetStaffAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Staff staff = await staffRepo.GetStaffAsync(id);
            //db.Staffs.Remove(staff);
            staffRepo.DeleteStaff(staff);
            //await db.SaveChangesAsync();
            await staffRepo.CompleteAsync();
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                staffRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}




