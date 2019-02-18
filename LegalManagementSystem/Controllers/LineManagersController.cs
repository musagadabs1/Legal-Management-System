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
    public class LineManagersController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: LineManagers
        public async Task<ActionResult> Index()
        {
            return View(await db.LineManagers.ToListAsync());
        }

        // GET: LineManagers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineManager lineManager = await db.LineManagers.FindAsync(id);
            if (lineManager == null)
            {
                return HttpNotFound();
            }
            return View(lineManager);
        }

        // GET: LineManagers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LineManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "LineManagerId,Name,Department,Designation")] LineManager lineManager)
        {
            if (ModelState.IsValid)
            {
                db.LineManagers.Add(lineManager);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(lineManager);
        }

        // GET: LineManagers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineManager lineManager = await db.LineManagers.FindAsync(id);
            if (lineManager == null)
            {
                return HttpNotFound();
            }
            return View(lineManager);
        }

        // POST: LineManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "LineManagerId,Name,Department,Designation")] LineManager lineManager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lineManager).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(lineManager);
        }

        // GET: LineManagers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineManager lineManager = await db.LineManagers.FindAsync(id);
            if (lineManager == null)
            {
                return HttpNotFound();
            }
            return View(lineManager);
        }

        // POST: LineManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LineManager lineManager = await db.LineManagers.FindAsync(id);
            db.LineManagers.Remove(lineManager);
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
