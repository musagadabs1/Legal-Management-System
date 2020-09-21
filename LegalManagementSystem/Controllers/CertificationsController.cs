using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Attorney,Advocate")]
    public class CertificationsController : Controller
    {
        //private MyCaseNewEntities db = new MyCaseNewEntities();
        private ICertification db;
        public CertificationsController()
        {
            db = new CertificationRepository();
        }

        // GET: Certifications
        public async Task<ActionResult> Index()
        {


            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                //var adminCertifications = db.GetCertificationsWithStaffAsync;
                return View(await db.GetCertificationsWithStaffAsync());
            }
            //var certifications = db.GetCertificationsWithStaff();
            return View(await db.GetCertificationsWithStaffAsync(x => x.CreatedBy.Equals(user)));
        }

        // GET: Certifications/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var certification = await db.GetCertificationAsync(Convert.ToInt32(id));
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        // GET: Certifications/Create
        public ActionResult Create()
        {
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
            ViewBag.Skilled = new List<SelectListItem>
            {
              new SelectListItem{Text="Advanced",Value="Advanced"},
               new SelectListItem{Text="Beginner",Value="Beginner"},
               new SelectListItem{Text="Intermediate",Value="Intermediate"},
               new SelectListItem{Text="Novice",Value="Novice"},
               new SelectListItem{Text="Expert",Value="Expert"}
            };
            return View();
        }

        // POST: Certifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Certification certification)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                certification.CreatedBy = user;
                certification.CreatedOn = DateTime.Today;
                certification.StaffId = LegalGuideUtility.StaffId; //(string)ViewBag.StaffId;

                db.AddCertification(certification);
                await db.CompleteAsync();
                return RedirectToAction("Create", "Dependants");
            }

            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", certification.StaffId);
            ViewBag.Skilled = new List<SelectListItem>
            {
              new SelectListItem{Text="Advanced",Value="Advanced"},
               new SelectListItem{Text="Beginner",Value="Beginner"},
               new SelectListItem{Text="Intermediate",Value="Intermediate"},
               new SelectListItem{Text="Novice",Value="Novice"},
               new SelectListItem{Text="Expert",Value="Expert"}
            };
            return View(certification);
        }

        // GET: Certifications/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = await db.GetCertificationAsync(Convert.ToInt32(id));
            if (certification == null)
            {
                return HttpNotFound();
            }
            ViewBag.Skilled = new List<SelectListItem>
            {
              new SelectListItem{Text="Advanced",Value="Advanced"},
               new SelectListItem{Text="Beginner",Value="Beginner"},
               new SelectListItem{Text="Intermediate",Value="Intermediate"},
               new SelectListItem{Text="Novice",Value="Novice"},
               new SelectListItem{Text="Expert",Value="Expert"}
            };
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", certification.StaffId);
            return View(certification);
        }

        // POST: Certifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Certification certification)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                certification.ModeifiedBy = user;
                certification.ModifiedOn = DateTime.Today;
                certification.StaffId = LegalGuideUtility.StaffId; //(string)ViewBag.StaffId;

                db.UpdateCertification(certification);
                await db.CompleteAsync();
                return RedirectToAction("Create", "Dependants");
            }
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", certification.StaffId);
            ViewBag.Skilled = new List<SelectListItem>
            {
              new SelectListItem{Text="Advanced",Value="Advanced"},
               new SelectListItem{Text="Beginner",Value="Beginner"},
               new SelectListItem{Text="Intermediate",Value="Intermediate"},
               new SelectListItem{Text="Novice",Value="Novice"},
               new SelectListItem{Text="Expert",Value="Expert"}
            };
            return View(certification);
        }

        // GET: Certifications/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Certification certification = await db.GetCertificationAsync(Convert.ToInt32(id));
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }

        // POST: Certifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Certification certification = await db.GetCertificationAsync(Convert.ToInt32(id));
            db.DeleteCertification(certification);
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
