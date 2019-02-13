﻿using System;
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
    [Authorize(Roles ="Admin,Attorney,Advocate")]
    public class StaffsController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Staffs
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.Staffs.ToListAsync());
            }
            return View(await db.Staffs.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }
        public ActionResult Salary()
        {
            return View();
        }

        // GET: Staffs/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = await db.Staffs.FindAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            //var staffType = new List<SelectListItem>
            //{
            //    new SelectListItem {Value = "Staff", Text = "Staff" },
            //    new SelectListItem{Value = "Faculty", Text = "Faculty"},
            //    new SelectListItem{Value = "Other", Text = "Other"}
            //};
            //ViewBag.StaffType = staffType;

            var gender= new List<SelectListItem> {
                new SelectListItem{Text="Male",Value ="M"},
                new SelectListItem{Text="Female",Value ="F"}
            };
            ViewBag.Gender = gender;
           var Marital=  new List<SelectListItem> {
                new SelectListItem{Text="Divorced",Value="Divorced"},
                new SelectListItem{Text="Married",Value="Married"},
                new SelectListItem{Text="Single",Value="Single"},
                new SelectListItem{Text="Separated",Value="Separated"},
                new SelectListItem{Text="Maried With Children",Value="Maried With Children"},
                new SelectListItem{Text="Single With Children",Value="Single With Children"}

            };
            ViewBag.Marital = Marital;
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,MiddleName,Gender,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,LastName,DOB,DOE,Status,Address,MaritalStatus,ImagePath,OfficeNo,MobileNo,EmailAddress,PersonalEmail,Relationship,KTelephone,NKEmail,NKAddress,Bank,Branch,AccountNumber,NKFullName,Password,StaffId,LineManager,Department,Designation,YearCallToBar,Location")] Staff staff)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    var staffId = string.Empty;
                    var user = User.Identity.Name;
                    staff.CreatedBy = user;
                    staff.CreatedOn = DateTime.Today;
                    staffId = staff.StaffId.ToUpper();
                    LegalGuideUtility.StaffId = staffId; //ViewBag.StaffId=staff.StaffId;
                    staff.Status = "Active";
                    //staff.Branch = "Abuja";

                    db.Staffs.Add(staff);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Create", "Experiences");
                }
                catch (Exception ex)
                {

                    ViewBag.Error = "Can't Save Profile please check and try again." + ex.Message;
                }
            }
            else
            {
                ViewBag.Error = "Can't Save Profile, Some fields are missing";
            }
            ViewBag.Gender = new List<SelectListItem> {
                new SelectListItem{Text="Male",Value ="M"},
                new SelectListItem{Text="Female",Value ="F"}
            };
             //= gender;
            ViewBag.Marital = new List<SelectListItem> {
                new SelectListItem{Text="Divorced",Value="Divorced"},
                new SelectListItem{Text="Married",Value="Married"},
                new SelectListItem{Text="Single",Value="Single"},
                new SelectListItem{Text="Separated",Value="Separated"},
                new SelectListItem{Text="Maried With Children",Value="Maried With Children"},
                new SelectListItem{Text="Single With Children",Value="Single With Children"}

            };
             //= Marital;
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Gender = new List<SelectListItem> {
                new SelectListItem{Text="Male",Value="M"},
                new SelectListItem{Text="Female",Value="F"}
            };
            ViewBag.Marital = new List<SelectListItem> {
                new SelectListItem{Text="Divorced",Value="Divorced"},
                new SelectListItem{Text="Married",Value="Married"},
                new SelectListItem{Text="Single",Value="Single"},
                new SelectListItem{Text="Separated",Value="Separated"},
                new SelectListItem{Text="Maried With Children",Value="Maried With Children"},
                new SelectListItem{Text="Single With Children",Value="Single With Children"}

            };
            Staff staff = await db.Staffs.FindAsync(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,MiddleName,Gender,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,LastName,DOB,DOE,Status,Address,MaritalStatus,ImagePath,OfficeNo,MobileNo,EmailAddress,PersonalEmail,Relationship,KTelephone,NKEmail,NKAddress,Bank,Branch,AccountNumber,NKFullName,Password,StaffId,LineManager,Department,Designation,YearCallToBar,Location")] Staff staff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var staffId = string.Empty;

                    var user = User.Identity.Name;
                    staff.ModifiedBy = user;
                    staffId = staff.StaffId.ToUpper();
                    staff.ModifiedOn = DateTime.Today;
                    LegalGuideUtility.StaffId = staffId; //ViewBag.StaffId=staff.StaffId;
                    staff.Status = LegalGuideUtility.Status;
                    //staff.Branch = "Abuja";

                    db.Entry(staff).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Create", "Experiences");
                }
                else
                {
                    ViewBag.Error = "Can't Save Profile, Some fields are missing";
                    ViewBag.Gender = new List<SelectListItem> {
                        new SelectListItem{Text="Male",Value ="M"},
                        new SelectListItem{Text="Female",Value ="F"}
                    };
                     ViewBag.Marital = new List<SelectListItem> {
                            new SelectListItem{Text="Divorced",Value="Divorced"},
                            new SelectListItem{Text="Married",Value="Married"},
                            new SelectListItem{Text="Single",Value="Single"},
                            new SelectListItem{Text="Separated",Value="Separated"},
                            new SelectListItem{Text="Maried With Children",Value="Maried With Children"},
                            new SelectListItem{Text="Single With Children",Value="Single With Children"}

                     };
                     //= Marital;
                    return View(staff);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Can't Save Profile please check and try again." + ex.Message;
                //throw;
            }
            ViewBag.Gender = new List<SelectListItem> {
                new SelectListItem{Text="Male",Value ="M"},
                new SelectListItem{Text="Female",Value ="F"}
            };
            //= gender;
            ViewBag.Marital = new List<SelectListItem> {
                new SelectListItem{Text="Divorced",Value="Divorced"},
                new SelectListItem{Text="Married",Value="Married"},
                new SelectListItem{Text="Single",Value="Single"},
                new SelectListItem{Text="Separated",Value="Separated"},
                new SelectListItem{Text="Maried With Children",Value="Maried With Children"},
                new SelectListItem{Text="Single With Children",Value="Single With Children"}

            };
             //= Marital;
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = await db.Staffs.FindAsync(id);
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
            Staff staff = await db.Staffs.FindAsync(id);
            db.Staffs.Remove(staff);
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
