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
using System.Linq.Dynamic;
using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Repositories;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LineManagersController : Controller
    {
        //private MyCaseNewEntities db = new MyCaseNewEntities();
        private ILineManager lineManagerRepo;

        public LineManagersController()
        {
            lineManagerRepo = new LineManagerRepository();
        }
        // GET: LineManagers
        public async Task<ActionResult> Index()
        {
            
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                //GetManagersAsync
                //return View(await db.LineManagers.ToListAsync());
                return View(await lineManagerRepo.GetManagersAsync());
            }
            //return View(await db.LineManagers.ToListAsync());
            return View(await lineManagerRepo.GetManagersAsync());
        }
        public ActionResult GetLineManagers()
        {
            //Server side parameters
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            List<LineManagerViewModel> lineManagers = new List<LineManagerViewModel>();
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {


                //var getLineManagersAdmin = db.LineManagers.ToList<LineManager>();
                var getLineManagersAdmin = lineManagerRepo.GetManagers();
                foreach (var item in getLineManagersAdmin)
                {
                    lineManagers.Add(new LineManagerViewModel
                    {
                        Name = item.Name,
                        Department = item.Department,
                        Designation = item.Designation
                    });
                }
                //Search operations
                if (!string.IsNullOrEmpty(searchValue))
                {
                    lineManagers = lineManagers.Where(x => x.Department.ToLower().Contains(searchValue.ToLower()) || x.Designation.ToLower().Contains(searchValue.ToLower()) || x.Name.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //Sort Operations
                lineManagers = lineManagers.OrderBy(sortColumnName + " " + sortDirection).ToList();

                // Paging operation
                lineManagers = lineManagers.Skip(start).Take(length).ToList();

                return Json(new { data = lineManagers }, JsonRequestBehavior.AllowGet);
            }
            var getLineManagers = lineManagerRepo.GetManagers(x => x.CreatedBy.Equals(user));
            //var getLineManagers = db.LineManagers.Where(x => x.CreatedBy.Equals(user)).ToList();
            foreach (var item in getLineManagers)
            {
                lineManagers.Add(new LineManagerViewModel
                {
                    Name = item.Name,
                    Department = item.Department,
                    Designation = item.Designation
                });
            }
            //Searching Operations
            if (!string.IsNullOrEmpty(searchValue))
            {
                lineManagers = lineManagers.Where(x => x.Department.ToLower().Contains(searchValue.ToLower()) || x.Designation.ToLower().Contains(searchValue.ToLower()) || x.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            //Sort Operations
            lineManagers = lineManagers.OrderBy(sortColumnName + " " + sortDirection).ToList();

            // Paging operation
            lineManagers = lineManagers.Skip(start).Take(length).ToList();
            return Json(new { data = lineManagers }, JsonRequestBehavior.AllowGet);
        }

        // GET: LineManagers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LineManager lineManager = await lineManagerRepo.GetManagerAsync(id);
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
        public async Task<ActionResult> Create([Bind(Include = "LineManagerId,Name,Department,Designation,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] LineManager lineManager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = User.Identity;
                    lineManager.CreatedBy = user.Name;
                    lineManager.CreatedOn = DateTime.Today;

                    //db.LineManagers.Add(lineManager);
                    lineManagerRepo.AddManager(lineManager);
                    await lineManagerRepo.CompleteAsync();
                    //await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Can't add Document. Fill in the required Fields. ";
                    return View(lineManager);

                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't add Line Manager. Fill in the required Fields. " + ex.Message;
            }

            return View(lineManager);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveLineManager(LineManager lineManager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = User.Identity;
                    lineManager.CreatedBy = user.Name;
                    lineManager.CreatedOn = DateTime.Today;

                    lineManagerRepo.AddManager(lineManager);
                    //db.LineManagers.Add(lineManager);
                    //await db.SaveChangesAsync();
                    await lineManagerRepo.CompleteAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Can't add Document. Fill in the required Fields. ";
                    return View(lineManager);

                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't add Line Manager. Fill in the required Fields. " + ex.Message;
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
            LineManager lineManager = await lineManagerRepo.GetManagerAsync(id);
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
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(LineManager lineManager)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = User.Identity;
                    lineManager.ModifiedBy = user.Name;
                    lineManager.ModifiedOn = DateTime.Today;

                    //db.Entry(lineManager).State = EntityState.Modified;
                    lineManagerRepo.UpdateClient(lineManager);
                    //await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Can't add Document. Fill in the required Fields. ";
                    return View(lineManager);

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Can't add Line Manager. Fill in the required Fields. " + ex.Message;
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
            //LineManager lineManager = await db.LineManagers.FindAsync(id);
            LineManager lineManager = await lineManagerRepo.GetManagerAsync(Convert.ToInt32(id));
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
            LineManager lineManager = await lineManagerRepo.GetManagerAsync(Convert.ToInt32(id));
            lineManagerRepo.DeleteManager(lineManager);
            //db.LineManagers.Remove(lineManager);
            await lineManagerRepo.CompleteAsync();
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lineManagerRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
