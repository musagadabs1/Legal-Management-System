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
using System.IO;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles ="ShrishAdmin")]
    public class LicenseTablesController : Controller
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

        // GET: LicenseTables
        public async Task<ActionResult> Index()
        {
            return View(await db.LicenseTables.ToListAsync());
        }

        // GET: LicenseTables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LicenseTable licenseTable = await db.LicenseTables.FindAsync(id);
            if (licenseTable == null)
            {
                return HttpNotFound();
            }
            return View(licenseTable);
        }

        // GET: LicenseTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LicenseTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ClientName,SoftwareVersion,ApprovedKey,ValidityFrom,ValidityTo,ApprovedDocument,ApprovedBy,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] LicenseTable licenseTable,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    string filePath = string.Empty;

                    if (file.ContentLength > 0 && file != null)
                    {
                        filePath = file.FileName;
                        fileName = Path.GetFileName(file.FileName);
                    }
                    else
                    {
                        ViewBag.Error = " please select approved document to continue.";
                        return View(licenseTable);
                    }
                    var folderPath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/LicenseDocs";
                    var fullFilePath = Path.Combine(folderPath, filePath);
                    file.SaveAs(fullFilePath);

                    licenseTable.ApprovedDocument = fileName;
                    licenseTable.IsExpired = false;
                    licenseTable.IsLicensed = true;
                    licenseTable.ApprovedKey = LegalGuideUtility.Encrypt(licenseTable.ApprovedKey);
                    licenseTable.SoftwareVersion = licenseTable.SoftwareVersion.ToString();
                    licenseTable.CreatedBy = User.Identity.Name;
                    licenseTable.CreatedOn = DateTime.Today;
                    db.LicenseTables.Add(licenseTable);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ViewBag.Error = "Can't Save License info please check and try again." + ex.Message;
                }
                
            }
            else
            {
                ViewBag.Error = "Can't Save License info, Some fields are missing";
                return View(licenseTable);

            }

            return View(licenseTable);
        }

        // GET: LicenseTables/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LicenseTable licenseTable = await db.LicenseTables.FindAsync(id);
            if (licenseTable == null)
            {
                return HttpNotFound();
            }
            return View(licenseTable);
        }

        // POST: LicenseTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ClientName,SoftwareVersion,ApprovedKey,ValidityFrom,ValidityTo,ApprovedDocument,ApprovedBy,CreatedBy,CreatedOn,ModifiedBy,ModifiedOn")] LicenseTable licenseTable, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    string filePath = string.Empty;

                    if (file.ContentLength > 0 && file != null)
                    {
                        filePath = file.FileName;
                        fileName = Path.GetFileName(file.FileName);
                    }
                    else
                    {
                        ViewBag.Error = " please select approved document to continue.";
                        return View(licenseTable);
                    }
                    var folderPath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/LicenseDocs";
                    var fullFilePath = Path.Combine(folderPath, filePath);

                    licenseTable.ApprovedDocument = fileName;
                    licenseTable.ModifiedBy = User.Identity.Name;
                    licenseTable.ModifiedOn = DateTime.Today;
                    licenseTable.IsExpired = false;
                    licenseTable.IsLicensed = true;
                    licenseTable.ApprovedKey = LegalGuideUtility.Encrypt(licenseTable.ApprovedKey);
                    licenseTable.SoftwareVersion = licenseTable.SoftwareVersion.ToString();
                    db.Entry(licenseTable).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    ViewBag.Error = "Can't Save License info please check and try again." + ex.Message;
                }
                
            }
            else
            {
                ViewBag.Error = "Can't Save License info, Some fields are missing";
                return View(licenseTable);

            }
            return View(licenseTable);
        }

        // GET: LicenseTables/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LicenseTable licenseTable = await db.LicenseTables.FindAsync(id);
            if (licenseTable == null)
            {
                return HttpNotFound();
            }
            return View(licenseTable);
        }

        // POST: LicenseTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            LicenseTable licenseTable = await db.LicenseTables.FindAsync(id);
            db.LicenseTables.Remove(licenseTable);
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
