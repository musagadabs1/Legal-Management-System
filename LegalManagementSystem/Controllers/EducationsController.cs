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
    [Authorize(Roles = "Admin,Attorney,Advocate")]
    public class EducationsController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Educations
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                var adminCertifications = db.Educations.Include(c => c.Staff);
                return View(await adminCertifications.ToListAsync());
            }
            var educations = db.Educations.Include(e => e.Staff);
            return View(await educations.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Educations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = await db.Educations.FindAsync(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // GET: Educations/Create
        public ActionResult Create()
        {
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
            ViewBag.Qualification = new List<SelectListItem>
            {
              new SelectListItem{Text="PhD",Value="PhD"},
               new SelectListItem{Text="Msc",Value="Msc"},
               new SelectListItem{Text="Bsc",Value="Bsc"},
               new SelectListItem{Text="PGD",Value="PGD"},
               new SelectListItem{Text="NCE",Value="NCE"},
               new SelectListItem{Text="ND",Value="ND"},
               new SelectListItem{Text="HND",Value="HND"}
            };
            ViewBag.Grade = new List<SelectListItem>
            {
              new SelectListItem{Text="First Class",Value="First Class"},
               new SelectListItem{Text="Second Class Upper",Value="Second Class Upper"},
               new SelectListItem{Text="Second Class Lower",Value="Second Class Lower"},
               new SelectListItem{Text="Third Class",Value="Third Class"},
               new SelectListItem{Text="Pass",Value="Pass"},
               new SelectListItem{Text="Not Available",Value="Not Available"}
            };
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,School,DateStart,EndDate,Major,DateAwarded,Graduated,Qualification,Description,Grade,StaffId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Education education)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                education.CreatedBy = user;
                education.CreatedOn = DateTime.Today;
                education.StaffId = LegalGuideUtility.StaffId;

                db.Educations.Add(education);
                await db.SaveChangesAsync();
                return RedirectToAction("Create", "Certifications");
            }
            ViewBag.Qualification = new List<SelectListItem>
            {
              new SelectListItem{Text="PhD",Value="PhD"},
               new SelectListItem{Text="Msc",Value="Msc"},
               new SelectListItem{Text="Bsc",Value="Bsc"},
               new SelectListItem{Text="PGD",Value="PGD"},
               new SelectListItem{Text="NCE",Value="NCE"},
               new SelectListItem{Text="ND",Value="ND"},
               new SelectListItem{Text="HND",Value="HND"}
            };
            ViewBag.Grade = new List<SelectListItem>
            {
              new SelectListItem{Text="First Class",Value="First Class"},
               new SelectListItem{Text="Second Class Upper",Value="Second Class Upper"},
               new SelectListItem{Text="Second Class Lower",Value="Second Class Lower"},
               new SelectListItem{Text="Third Class",Value="Third Class"},
               new SelectListItem{Text="Pass",Value="Pass"},
               new SelectListItem{Text="Not Available",Value="Not Available"}
            };

            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", education.StaffId);
            return View(education);
        }

        // GET: Educations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = await db.Educations.FindAsync(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", education.StaffId);
            ViewBag.Qualification = new List<SelectListItem>
            {
              new SelectListItem{Text="PhD",Value="PhD"},
               new SelectListItem{Text="Msc",Value="Msc"},
               new SelectListItem{Text="Bsc",Value="Bsc"},
               new SelectListItem{Text="PGD",Value="PGD"},
               new SelectListItem{Text="NCE",Value="NCE"},
               new SelectListItem{Text="ND",Value="ND"},
               new SelectListItem{Text="HND",Value="HND"}
            };
            ViewBag.Grade = new List<SelectListItem>
            {
              new SelectListItem{Text="First Class",Value="First Class"},
               new SelectListItem{Text="Second Class Upper",Value="Second Class Upper"},
               new SelectListItem{Text="Second Class Lower",Value="Second Class Lower"},
               new SelectListItem{Text="Third Class",Value="Third Class"},
               new SelectListItem{Text="Pass",Value="Pass"},
               new SelectListItem{Text="Not Available",Value="Not Available"}
            };
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,School,DateStart,EndDate,Major,DateAwarded,Graduated,Qualification,Description,Grade,StaffId,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Education education)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                education.ModifiedBy = user;
                education.ModifiedOn = DateTime.Today;
                education.StaffId = LegalGuideUtility.StaffId;

                db.Entry(education).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Create", "Educations");
            }
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", education.StaffId);
            ViewBag.Qualification = new List<SelectListItem>
            {
              new SelectListItem{Text="PhD",Value="PhD"},
               new SelectListItem{Text="Msc",Value="Msc"},
               new SelectListItem{Text="Bsc",Value="Bsc"},
               new SelectListItem{Text="PGD",Value="PGD"},
               new SelectListItem{Text="NCE",Value="NCE"},
               new SelectListItem{Text="ND",Value="ND"},
               new SelectListItem{Text="HND",Value="HND"}
            };
            ViewBag.Grade = new List<SelectListItem>
            {
              new SelectListItem{Text="First Class",Value="First Class"},
               new SelectListItem{Text="Second Class Upper",Value="Second Class Upper"},
               new SelectListItem{Text="Second Class Lower",Value="Second Class Lower"},
               new SelectListItem{Text="Third Class",Value="Third Class"},
               new SelectListItem{Text="Pass",Value="Pass"},
               new SelectListItem{Text="Not Available",Value="Not Available"}
            };
            return View(education);
        }

        // GET: Educations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = await db.Educations.FindAsync(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Education education = await db.Educations.FindAsync(id);
            db.Educations.Remove(education);
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
