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
    public class CompaniesController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: Companies
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
            {
                return View(await db.Companies.ToListAsync());
            }
            return View(await db.Companies.Where(x => x.CreatedBy.Equals(user)).ToListAsync());
        }

        // GET: Companies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.LegalType = new List<SelectListItem>
            {
                new SelectListItem{Value="Criminal", Text="Criminal"},
                new SelectListItem{Value="Civil",Text="Civil"}
            };
            ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem{Value="Algeria", Text="Algeria"},
                new SelectListItem{Value="Benin",Text="Benin"},
                new SelectListItem{Value="Cameroon", Text="Cameroon"},
                new SelectListItem{Value="Djibouti", Text="Djibouti"},
                new SelectListItem{Value="Egypt", Text="Egypt"},
                new SelectListItem{Value="Ghana", Text="Ghana"},
                new SelectListItem{Value="Kenya", Text="Kenya"},
                new SelectListItem{Value="Liberia", Text="Liberia"},
                new SelectListItem{Value="Mali", Text="Mali"},
                new SelectListItem{Value="Nigeria", Text="Nigeria"},
                new SelectListItem{Value="Rwanda", Text="Rwanda"},
                new SelectListItem{Value="South Africa", Text="South Africa"},
                new SelectListItem{Value="Tanzania", Text="Tanzania"},
                new SelectListItem{Value="Uganda", Text="Uganda"},
                new SelectListItem{Value="Zimbabwe", Text="Zimbabwe"},

            };
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ShortName,CompanyLegalType,CompanyGroup,Nationality,Address,City,State,Country,Website,PhoneNumber,MobileNumber,EmailAddress,CompanyLawyer,Capital,Currency,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Company company)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var user = User.Identity.Name;
                    company.CreatedBy = user;
                    company.CreatedOn = DateTime.Today;

                    db.Companies.Add(company);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.Error = "Can't Add Company, Error Occured. Please Contact IT Department.";
                }
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.LegalType = new List<SelectListItem>
            {
                new SelectListItem{Value="Criminal", Text="Criminal"},
                new SelectListItem{Value="Civil",Text="Civil"}
            };
                ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem{Value="Algeria", Text="Algeria"},
                new SelectListItem{Value="Benin",Text="Benin"},
                new SelectListItem{Value="Cameroon", Text="Cameroon"},
                new SelectListItem{Value="Djibouti", Text="Djibouti"},
                new SelectListItem{Value="Egypt", Text="Egypt"},
                new SelectListItem{Value="Ghana", Text="Ghana"},
                new SelectListItem{Value="Kenya", Text="Kenya"},
                new SelectListItem{Value="Liberia", Text="Liberia"},
                new SelectListItem{Value="Mali", Text="Mali"},
                new SelectListItem{Value="Nigeria", Text="Nigeria"},
                new SelectListItem{Value="Rwanda", Text="Rwanda"},
                new SelectListItem{Value="South Africa", Text="South Africa"},
                new SelectListItem{Value="Tanzania", Text="Tanzania"},
                new SelectListItem{Value="Uganda", Text="Uganda"},
                new SelectListItem{Value="Zimbabwe", Text="Zimbabwe"},

            };
                return View(company);
            }
            ViewBag.LegalType = new List<SelectListItem>
            {
                new SelectListItem{Value="Criminal", Text="Criminal"},
                new SelectListItem{Value="Civil",Text="Civil"}
            };
            ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem{Value="Algeria", Text="Algeria"},
                new SelectListItem{Value="Benin",Text="Benin"},
                new SelectListItem{Value="Cameroon", Text="Cameroon"},
                new SelectListItem{Value="Djibouti", Text="Djibouti"},
                new SelectListItem{Value="Egypt", Text="Egypt"},
                new SelectListItem{Value="Ghana", Text="Ghana"},
                new SelectListItem{Value="Kenya", Text="Kenya"},
                new SelectListItem{Value="Liberia", Text="Liberia"},
                new SelectListItem{Value="Mali", Text="Mali"},
                new SelectListItem{Value="Nigeria", Text="Nigeria"},
                new SelectListItem{Value="Rwanda", Text="Rwanda"},
                new SelectListItem{Value="South Africa", Text="South Africa"},
                new SelectListItem{Value="Tanzania", Text="Tanzania"},
                new SelectListItem{Value="Uganda", Text="Uganda"},
                new SelectListItem{Value="Zimbabwe", Text="Zimbabwe"},

            };
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            ViewBag.LegalType = new List<SelectListItem>
            {
                new SelectListItem{Value="Criminal", Text="Criminal"},
                new SelectListItem{Value="Civil",Text="Civil"}
            };
            ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem{Value="Algeria", Text="Algeria"},
                new SelectListItem{Value="Benin",Text="Benin"},
                new SelectListItem{Value="Cameroon", Text="Cameroon"},
                new SelectListItem{Value="Djibouti", Text="Djibouti"},
                new SelectListItem{Value="Egypt", Text="Egypt"},
                new SelectListItem{Value="Ghana", Text="Ghana"},
                new SelectListItem{Value="Kenya", Text="Kenya"},
                new SelectListItem{Value="Liberia", Text="Liberia"},
                new SelectListItem{Value="Mali", Text="Mali"},
                new SelectListItem{Value="Nigeria", Text="Nigeria"},
                new SelectListItem{Value="Rwanda", Text="Rwanda"},
                new SelectListItem{Value="South Africa", Text="South Africa"},
                new SelectListItem{Value="Tanzania", Text="Tanzania"},
                new SelectListItem{Value="Uganda", Text="Uganda"},
                new SelectListItem{Value="Zimbabwe", Text="Zimbabwe"},

            };
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ShortName,CompanyLegalType,CompanyGroup,Nationality,Address,City,State,Country,Website,PhoneNumber,MobileNumber,EmailAddress,CompanyLawyer,Capital,Currency,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] Company company)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var user = User.Identity.Name;
                    company.ModifiedBy = user;
                    company.ModifiedOn = DateTime.Today;

                    db.Entry(company).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    ViewBag.Error = "Can't Add Company, Error Occured. Please Contact IT Department.";
                }
            }
            else
            {
                ViewBag.Error = "Fill in the required fields to continue";
                ViewBag.LegalType = new List<SelectListItem>
            {
                new SelectListItem{Value="Criminal", Text="Criminal"},
                new SelectListItem{Value="Civil",Text="Civil"}
            };
                ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem{Value="Algeria", Text="Algeria"},
                new SelectListItem{Value="Benin",Text="Benin"},
                new SelectListItem{Value="Cameroon", Text="Cameroon"},
                new SelectListItem{Value="Djibouti", Text="Djibouti"},
                new SelectListItem{Value="Egypt", Text="Egypt"},
                new SelectListItem{Value="Ghana", Text="Ghana"},
                new SelectListItem{Value="Kenya", Text="Kenya"},
                new SelectListItem{Value="Liberia", Text="Liberia"},
                new SelectListItem{Value="Mali", Text="Mali"},
                new SelectListItem{Value="Nigeria", Text="Nigeria"},
                new SelectListItem{Value="Rwanda", Text="Rwanda"},
                new SelectListItem{Value="South Africa", Text="South Africa"},
                new SelectListItem{Value="Tanzania", Text="Tanzania"},
                new SelectListItem{Value="Uganda", Text="Uganda"},
                new SelectListItem{Value="Zimbabwe", Text="Zimbabwe"},

            };
                return View(company);
            }
            ViewBag.LegalType = new List<SelectListItem>
            {
                new SelectListItem{Value="Criminal", Text="Criminal"},
                new SelectListItem{Value="Civil",Text="Civil"}
            };
            ViewBag.Countries = new List<SelectListItem>
            {
                new SelectListItem{Value="Algeria", Text="Algeria"},
                new SelectListItem{Value="Benin",Text="Benin"},
                new SelectListItem{Value="Cameroon", Text="Cameroon"},
                new SelectListItem{Value="Djibouti", Text="Djibouti"},
                new SelectListItem{Value="Egypt", Text="Egypt"},
                new SelectListItem{Value="Ghana", Text="Ghana"},
                new SelectListItem{Value="Kenya", Text="Kenya"},
                new SelectListItem{Value="Liberia", Text="Liberia"},
                new SelectListItem{Value="Mali", Text="Mali"},
                new SelectListItem{Value="Nigeria", Text="Nigeria"},
                new SelectListItem{Value="Rwanda", Text="Rwanda"},
                new SelectListItem{Value="South Africa", Text="South Africa"},
                new SelectListItem{Value="Tanzania", Text="Tanzania"},
                new SelectListItem{Value="Uganda", Text="Uganda"},
                new SelectListItem{Value="Zimbabwe", Text="Zimbabwe"},

            };
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = await db.Companies.FindAsync(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Company company = await db.Companies.FindAsync(id);
            db.Companies.Remove(company);
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
