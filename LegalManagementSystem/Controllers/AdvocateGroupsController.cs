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
using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Repositories;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin, Attorney, Advocate")]
    public class AdvocateGroupsController : Controller
    {
        //private MyCaseNewEntities db = new MyCaseNewEntities();
        private IAdvocateGroup advocateGroupRepo;
        public AdvocateGroupsController()
        {
            advocateGroupRepo = new AdvocateGroupRepository();
        }
        // GET: AdvocateGroups
        public async Task<ActionResult> Index()
        {
            return View(await advocateGroupRepo.GetAdvocateGroupsAsync());
        }

        // GET: AdvocateGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvocateGroup advocateGroup = await advocateGroupRepo.GetAdvocateGroupAsync(id);
            if (advocateGroup == null)
            {
                return HttpNotFound();
            }
            return View(advocateGroup);
        }

        // GET: AdvocateGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvocateGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GroupName,Description,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] AdvocateGroup advocateGroup)
        {
            if (ModelState.IsValid)
            {
                advocateGroupRepo.AddAdvocateGroup(advocateGroup);
                await advocateGroupRepo.CompleteAsync();
                return RedirectToAction("Index");
            }

            return View(advocateGroup);
        }

        // GET: AdvocateGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvocateGroup advocateGroup = await advocateGroupRepo.GetAdvocateGroupAsync(id);
            if (advocateGroup == null)
            {
                return HttpNotFound();
            }
            return View(advocateGroup);
        }

        // POST: AdvocateGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GroupName,Description,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] AdvocateGroup advocateGroup)
        {
            if (ModelState.IsValid)
            {
                advocateGroupRepo.UpdateAdvocateGroup(advocateGroup);
                await advocateGroupRepo.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(advocateGroup);
        }

        // GET: AdvocateGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvocateGroup advocateGroup = await advocateGroupRepo.GetAdvocateGroupAsync(id);
            if (advocateGroup == null)
            {
                return HttpNotFound();
            }
            return View(advocateGroup);
        }

        // POST: AdvocateGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AdvocateGroup advocateGroup = await advocateGroupRepo.GetAdvocateGroupAsync(id);
            advocateGroupRepo.DeleteAdvocateGroup(advocateGroup);
            await advocateGroupRepo.CompleteAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                advocateGroupRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
