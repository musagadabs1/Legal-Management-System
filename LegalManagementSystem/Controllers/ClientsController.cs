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
    [Authorize(Roles = "Admin,Advocate,Lawyer,Attorney,Staff")]
    public class ClientsController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Clients
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.Clients.ToListAsync());
            }
            return View(await db.Clients.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,EmailAddress,PhoneNumber,Address,Town,PostalCode,Website,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,ClientNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!IsClientRegistered(client.FirstName, client.LastName, client.EmailAddress))
                    {
                        int nextId = GetCurrentId() + 1;
                        string clienName = client.FirstName;
                        string firstLetter = clienName.Substring(0, 1);
                        string clientId = firstLetter + nextId.ToString() + "-" + DateTime.Today.ToShortDateString();
                        var user = User.Identity;
                        client.CreatedBy = user.Name;
                        client.CreatedOn = DateTime.Today;
                        client.ClientNumber = clientId;

                        db.Clients.Add(client);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error = "Client Already Exist.";
                        return View(client);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Error occured while registering this client. Check and try again. " + ex;
                    return View(client);
                    //throw ex;
                }
                //db.Clients.Add(client);
                // db.SaveChangesAsync();
                //return RedirectToAction("Index");
            }

            return View(client);
        }
        private bool IsClientRegistered(string firstName, string lastName, string email)
        {
            //bool registered = false;
            try
            {
                var client = db.Clients.Where(c => c.FirstName.Equals(firstName) && c.LastName.Equals(lastName) && c.EmailAddress.Equals(email)).FirstOrDefault();
                if (client != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private int GetCurrentId()
        {
            try
            {
                return (db.Clients.Max(x => x.ClientId));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // GET: Clients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,EmailAddress,PhoneNumber,Address,Town,PostalCode,Website,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn,ClientNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                //int nextId = GetCurrentId() + 1;
                //string clienName = client.FirstName;
                //string firstLetter = clienName.Substring(0, 1);
                //string clientId = firstLetter + nextId.ToString() + "-" + DateTime.Today.ToShortDateString();

                var user = User.Identity;
                client.ModifiedBy = user.Name;
                client.ModifiedOn = DateTime.Today;
                //client.ClientNumber = clientId;

                db.Entry(client).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            db.Clients.Remove(client);
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
