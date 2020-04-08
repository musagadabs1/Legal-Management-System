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
    public class TodoesController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Todoes
        public async Task<ActionResult> Index()
        {
            return View(await db.Todoes.ToListAsync());
        }

        // GET: Todoes/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = await db.Todoes.FindAsync(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }
        [HttpPost]
        public async Task<ActionResult> ChangeStatus(Guid id)
        {
            var todo = await db.Todoes.FirstOrDefaultAsync(x => x.Id==id);

            if (todo != null)
            {
                todo.Done = !todo.Done;
                //await db.Entry(todo).State
                await db.SaveChangesAsync();
            }
            return View();
        }
        [HttpGet]
        public ActionResult GetPending(string employeeId)
        {
            var dao = db.Todoes.Where(x => x.Created_By==employeeId && !x.Done);

            var _todo = (from item in dao.OrderByDescending(x => x.CreatedDate)
                         select new
                         {
                             item.Id,
                             item.Title,
                             item.Details,
                             item.Done,
                             item.CreatedDate
                         }).ToList();
            var todos = new Dictionary<string, dynamic>();
            foreach (var item in _todo.GroupBy(x => x.CreatedDate.Date))
            {
                todos.Add(item.Key.ToString("dd MMM yyyy"), item.ToList());
            }
            return View(todos);
        }
        [HttpPost]
        public async Task<ActionResult> SaveTodo(Todo model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = model.CreatedDate;
                db.Todoes.Add(model);
                await db.SaveChangesAsync();
                return View();
                //return Get(model.EmployeeId, model.CreatedDate);
            }
            else
            {
                return View(ModelState);
            }
        }

        // GET: Todoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Title,Details,Done,CreatedDate")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.Id = Guid.NewGuid();
                todo.Created_By = User.Identity.Name;
                db.Todoes.Add(todo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        // GET: Todoes/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = await db.Todoes.FindAsync(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Todoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Title,Details,Done,CreatedDate")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.Created_By = User.Identity.Name;
                db.Entry(todo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        // GET: Todoes/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = await db.Todoes.FindAsync(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Todoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Todo todo = await db.Todoes.FindAsync(id);
            db.Todoes.Remove(todo);
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
