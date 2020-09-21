using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Attorney,Advocate")]
    public class DependantsController : Controller
    {
        //private MyCaseNewEntities db = new MyCaseNewEntities();
        private IDependant dependantRepo;
        public DependantsController()
        {
            dependantRepo = new DependantRepository();
        }
        // GET: Dependants
        public async Task<ActionResult> Index()
        {
            //var dependants = db.Dependants.Include(d => d.Staff);
            return View(await dependantRepo.GetDependantsWithStaffAsync());
        }

        // GET: Dependants/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = await dependantRepo.GetDependantAsync(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            return View(dependant);
        }

        // GET: Dependants/Create
        public ActionResult Create()
        {
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
            var gender = new List<SelectListItem> {
                new SelectListItem{Text="Male",Value ="M"},
                new SelectListItem{Text="Female",Value ="F"}
            };
            ViewBag.Gender = gender;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> SaveDependant(Dependant dependant)
        {
            //if (ModelState.IsValid)
            //{
            var user = User.Identity.Name;
            dependant.CreatedBy = user;
            dependant.CreatedOn = DateTime.Today;
            //var staffId = "stf01";

            dependant.StaffId = LegalGuideUtility.StaffId;
            dependantRepo.AddDependant(dependant);
            await dependantRepo.CompleteAsync();

            return RedirectToAction("Create", "Dependants");
            //}
            //var gender = new List<SelectListItem> {
            //    new SelectListItem{Text="Male",Value ="M"},
            //    new SelectListItem{Text="Female",Value ="F"}
            //};
            //ViewBag.Gender = gender;
            ////ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", dependant.StaffId);
            //return View(dependant);
        }

        // POST: Dependants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,LastName,PolicyNumber,EffectiveDate,EndDate,Gender,Relationship,DateOfBirth,Description,Address,StaffId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Dependant dependant)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                dependant.CreatedBy = user;
                dependant.CreatedOn = DateTime.Today;
                dependant.StaffId = LegalGuideUtility.StaffId;
                dependantRepo.AddDependant(dependant);
                await dependantRepo.CompleteAsync();
                return RedirectToAction("Create", "Dependants");
            }
            var gender = new List<SelectListItem> {
                new SelectListItem{Text="Male",Value ="M"},
                new SelectListItem{Text="Female",Value ="F"}
            };
            ViewBag.Gender = gender;
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", dependant.StaffId);
            return View(dependant);
        }

        // GET: Dependants/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = await dependantRepo.GetDependantAsync(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            var gender = new List<SelectListItem> {
                new SelectListItem{Text="Male",Value ="M"},
                new SelectListItem{Text="Female",Value ="F"}
            };
            ViewBag.Gender = gender;
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", dependant.StaffId);
            return View(dependant);
        }
        [HttpPost]
        public async Task<ActionResult> EditDependant(Dependant dependant)
        {
            //if (ModelState.IsValid)
            //{
                var user = User.Identity.Name;
                dependant.ModifiedBy = user;
                dependant.ModifiedOn = DateTime.Today;
                dependant.StaffId = LegalGuideUtility.StaffId;

                dependantRepo.UpdateDependant(dependant);
                await dependantRepo.CompleteAsync();

                return RedirectToAction("Index");
            //}
            //var gender = new List<SelectListItem> {
            //    new SelectListItem{Text="Male",Value ="M"},
            //    new SelectListItem{Text="Female",Value ="F"}
            //};
            //ViewBag.Gender = gender;
            ////ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", dependant.StaffId);
            //return View(dependant);
        }

        // POST: Dependants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,LastName,PolicyNumber,EffectiveDate,EndDate,Gender,Relationship,DateOfBirth,Description,Address,StaffId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Dependant dependant)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                dependant.ModifiedBy = user;
                dependant.ModifiedOn = DateTime.Today;
                dependant.StaffId = LegalGuideUtility.StaffId;

                dependantRepo.UpdateDependant(dependant);
                await dependantRepo.CompleteAsync();
                return RedirectToAction("Index");
            }
            var gender = new List<SelectListItem> {
                new SelectListItem{Text="Male",Value ="M"},
                new SelectListItem{Text="Female",Value ="F"}
            };
            ViewBag.Gender = gender;
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", dependant.StaffId);
            return View(dependant);
        }

        // GET: Dependants/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependant dependant = await dependantRepo.GetDependantAsync(id);
            if (dependant == null)
            {
                return HttpNotFound();
            }
            return View(dependant);
        }

        // POST: Dependants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Dependant dependant = await dependantRepo.GetDependantAsync(id);
            dependantRepo.DeleteDependant(dependant);
            await dependantRepo.CompleteAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dependantRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
