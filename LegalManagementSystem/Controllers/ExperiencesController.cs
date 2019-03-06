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
    [Authorize(Roles = "Admin,Attorney,Advocate")]
    public class ExperiencesController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Experiences
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                var adminExperiences = db.Experiences.Include(e => e.Staff);
                return View(await adminExperiences.ToListAsync());
            }
            var experiences = db.Experiences.Include(e => e.Staff);
            return View(await experiences.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Experiences/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = await db.Experiences.FindAsync(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // GET: Experiences/Create
        public ActionResult Create()
        {
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName");
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Employer,Designation,StartDate,EndDate,Description,Salary,StaffId,CreatedOn,CreatedBy,ModifiedBy,ModifiedOn")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                experience.CreatedBy = user;
                experience.CreatedOn = DateTime.Today;
                experience.StaffId = LegalGuideUtility.StaffId;

                db.Experiences.Add(experience);
                await db.SaveChangesAsync();
                return RedirectToAction("Create","Educations");
            }

            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", experience.StaffId);
            return View(experience);
        }

        // GET: Experiences/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = await db.Experiences.FindAsync(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", experience.StaffId);
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Employer,Designation,StartDate,EndDate,Description,Salary,StaffId,CreatedOn,CreatedBy,ModifiedBy,ModifiedOn")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                var user = User.Identity.Name;
                experience.ModifiedBy = user;
                experience.ModifiedOn = DateTime.Today;
                experience.StaffId = LegalGuideUtility.StaffId;

                db.Entry(experience).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Create", "Educations");
            }
            //ViewBag.StaffId = new SelectList(db.Staffs, "StaffId", "FirstName", experience.StaffId);
            return View(experience);
        }

        // GET: Experiences/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Experience experience = await db.Experiences.FindAsync(id);
            if (experience == null)
            {
                return HttpNotFound();
            }
            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Experience experience = await db.Experiences.FindAsync(id);
            db.Experiences.Remove(experience);
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
