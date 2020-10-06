using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.Repositories;
using LegalManagementSystem.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Attorney,Advocate")]
    public class DocumentsController : Controller
    {
        private IDocument _documentRepo;
        private IMatter _matterRepo;
        private static string mattrNumer = string.Empty;
        public DocumentsController()
        {
            _documentRepo = new DocumentRepository();
            _matterRepo = new MatterRepository();
        }
        // GET: Documents
        public async Task<ActionResult> Index(string matterNumber)
        {
            try
            {
                mattrNumer = matterNumber;
                var user = User.Identity.Name;
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                {
                    return View(await _documentRepo.GetDocumentsAsync(x => x.MatterNumber.Equals(matterNumber)));
                }
                return View(await _documentRepo.GetDocumentsAsync(x => x.CreatedBy.Equals(user) && x.MatterNumber.Equals(matterNumber)));
            }
            catch (Exception ex)
            {
                LegalGuideUtility.ErrorMessage = "Oooop. Something had happened. " + ex.Message;
                return RedirectToAction("Error");
                //throw;
            }

        }
        [HttpPost]
        public async Task<JsonResult> GetDocuments()
        {
            var user = User.Identity.Name;


            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //Sort Column index
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);
            //Search
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            try
            {
                var adminData = await _documentRepo.GetDocumentsAsync(x => x.MatterNumber==mattrNumer);
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                {
                    var dataToReturn = adminData.Select(s => new DocumentViewModel
                    {DocumentId=s.DocumentId, DocName = s.DocName, AssignedDate = s.AssignedDate.ToString("MM/dd/yyyy"),Description=s.Description}
                    );
                    //DTO record
                    List<DocumentViewModel> docData = dataToReturn.ToList();

                    //Sorting
                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                    {
                        if (sortColumnIndex == 0)
                        {
                            docData = sortColumnDir == "asc" ? docData.OrderBy(o => o.DocName).ToList() : docData.OrderByDescending(o => o.DocName).ToList();
                        }
                        else if (sortColumnIndex == 1)
                        {
                            docData = sortColumnDir == "asc" ? docData.OrderBy(o => o.AssignedDate).ToList() : docData.OrderByDescending(o => o.AssignedDate).ToList();
                        }
                        else
                        {
                            docData = sortColumnDir == "asc" ? docData.OrderBy(o => o.Description).ToList() : docData.OrderByDescending(o => o.Description).ToList();
                        }

                    }
                    //Search
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        docData = docData.Where(x => x.DocName.ToLower().Contains(searchValue.ToLower())
                                                                || x.AssignedDate.ToLower().Contains(searchValue.ToLower())
                                                                || x.Description.ToLower().Contains(searchValue.ToLower())).ToList();
                    }
                    //total number of row count
                    recordsTotal = docData.Count();
                    //Paging
                    var aData = docData.Skip(skip).Take(pageSize).ToList();

                    return Json(new
                    {
                        draw,
                        //param.sEcho,
                        recordsFiltered = recordsTotal,
                        recordsTotal,
                        data = aData
                    }, JsonRequestBehavior.AllowGet);
                }

                //Not an Admin User
                var data = adminData.Where(x => x.CreatedBy.Equals(user) && x.MatterNumber==mattrNumer).Select(s => new DocumentViewModel { DocumentId = s.DocumentId, DocName = s.DocName, AssignedDate = s.AssignedDate.ToString("MM/dd/yyyy"), Description = s.Description});

                List<DocumentViewModel> resultData = data.ToList();

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    if (sortColumnIndex == 0)
                    {
                        resultData = sortColumnDir == "asc" ? resultData.OrderBy(o => o.DocName).ToList() : resultData.OrderByDescending(o => o.DocName).ToList();
                    }
                    else if (sortColumnIndex == 1)
                    {
                        resultData = sortColumnDir == "asc" ? resultData.OrderBy(o => o.AssignedDate).ToList() : resultData.OrderByDescending(o => o.AssignedDate).ToList();
                    }
                    else
                    {
                        resultData = sortColumnDir == "asc" ? resultData.OrderBy(o => o.Description).ToList() : resultData.OrderByDescending(o => o.Description).ToList();
                    }

                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    resultData = resultData.Where(x => x.DocName.ToLower().Contains(searchValue.ToLower())
                                                                || x.AssignedDate.ToLower().Contains(searchValue.ToLower())
                                                                || x.Description.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //total number of row count
                recordsTotal = resultData.Count();
                //Paging
                var cData = resultData.Skip(skip).Take(pageSize).ToList();

                return Json(new
                {
                    draw = draw,
                    //param.sEcho,
                    filteredRecords = recordsTotal,
                    totalRecords = recordsTotal,
                    aaData = cData
                }, JsonRequestBehavior.AllowGet);

                //return Json(resultData);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        // GET: Documents/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CaseDocument document = await _documentRepo.GetDocumentWithCaseAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }
        public JsonResult GetMatterForDropDown(string searchKey)
        {
            try
            {
                //var user = User.Identity.Name;
                //var email = LegalGuideUtility.GetStaffEmailByLoginName(user);
                //var staffId = LegalGuideUtility.GetStaffIdByEmail(email);
                var getData = _matterRepo.GetMatterForDropDowns(); //  db.GetAllMattersForDropDown().ToList();

                //if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                //{
                if (searchKey != null)
                {
                    getData = _matterRepo.GetMatterForDropDowns().Where(x => x.Subject.Contains(searchKey)).ToList();
                }
                var ModifiedData = getData.Select(x => new
                {
                    id = x.MatterNumber,
                    text = x.Subject
                });

                return Json(ModifiedData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public ActionResult GetDocumentsByMatterNumber(string id)
        {
            try
            {
                var getCourtActivity = _documentRepo.GetDocumentsByMatterNumber(id);
                if (getCourtActivity.Count()<=0)
                {
                return RedirectToAction("Create", "Documents", new { mattNumber = id });
                }
                else
                {
                    return RedirectToAction("index", "Documents", new { matterNumber= id });
                }
                //return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost]
        public async Task<JsonResult> SaveDocument()
        {
            try
            {
                //mattrNumer = id;
                string fileName = string.Empty;
                string filePath = string.Empty;

                Document model = JsonConvert.DeserializeObject<Document>(Request.Form["model"].ToString());

                HttpPostedFileBase fileBase = Request.Files[0];

                if (fileBase.ContentLength > 0 && fileBase != null)
                {
                    filePath = fileBase.FileName;
                    fileName = Path.GetFileName(fileBase.FileName);
                }

                var folderPath = AppDomain.CurrentDomain.BaseDirectory + "/Content/Documents";
                var docPath = Path.Combine(folderPath, filePath);

                var user = User.Identity;
                model.CreatedBy = user.Name;
                model.DateCreated = DateTime.Today;
                model.DocPath = docPath;
                model.MatterNumber = mattrNumer;

                _documentRepo.AddDocument(model);
                fileBase.SaveAs(docPath);
                //db.Documents.Add(document);
                await _documentRepo.CompleteAsync();
                //await db.SaveChangesAsync();
                return Json(0,JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't add Document. Something went wrong " + ex.Message;
                return Json(0,JsonRequestBehavior.AllowGet);
            }



        }
        public FileResult Download(int documentId)
        {
            try
            {
                var document = _documentRepo.GetDocument(documentId);
                if (document == null)
                    throw new Exception(String.Format("No report found with id {0}", documentId));

                //var folderPath = AppDomain.CurrentDomain.BaseDirectory + "~/Content/Documents/" + document.DocPath;
                var folderPath = "~/Content/Documents/" + document.DocPath;
                return File(folderPath, "application/force-download", Path.GetFileName(folderPath)+".pdf");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        // GET: Documents/Create
        public ActionResult Create(string mattNumber)
        {
            mattrNumer = mattNumber;
            return View();
        }

        // GET: Documents/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await _documentRepo.GetDocumentAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Document document, HttpPostedFileBase fileBase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string fileName = string.Empty;
                    string filePath = string.Empty;

                    if (fileBase.ContentLength > 0 && fileBase != null)
                    {
                        filePath = fileBase.FileName;
                        fileName = Path.GetFileName(fileBase.FileName);
                    }
                    var folderPath = AppDomain.CurrentDomain.BaseDirectory + "/App_Data/Docs";
                    var docPath = Path.Combine(folderPath, filePath);

                    var user = User.Identity;
                    document.ModifiedBy = user.Name;
                    document.DateModified = DateTime.Today;
                    document.DocPath = fileName;

                    _documentRepo.UpdateDocument(document);
                    //db.Entry(document).State = EntityState.Modified;
                    await _documentRepo.CompleteAsync();
                    //await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Can't add Document. Fill in the required Fields. ";
                    ViewBag.MatterNumber = new SelectList(_matterRepo.GetMatterForDropDowns(), "MatterNumber", "Subject", document.MatterNumber);
                    return View(document);

                }
            }
            catch (Exception ex)
            {

                ViewBag.Error = "Can't add Document. Something went wrong " + ex.Message;
            }

            return View(document);
        }

        // GET: Documents/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = await _documentRepo.GetDocumentAsync(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Document document = await _documentRepo.GetDocumentAsync(id);
            //db.Documents.Remove(document);
            _documentRepo.DeleteDocument(document);
            await _documentRepo.CompleteAsync();
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _documentRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
