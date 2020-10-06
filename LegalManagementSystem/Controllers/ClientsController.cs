using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "Admin,Advocate,Lawyer,Attorney,Staff")]
    public class ClientsController : Controller
    {
        //private MyCaseNewEntities db = new MyCaseNewEntities();

        IClient _clientRepo;
        public ClientsController()
        {
            _clientRepo = new ClientRepository();
        }
        // GET: Clients
        public async Task<ActionResult> Index()
        {
            var user = User.Identity.Name;
            try
            {
                //_clientRepo.ProxySetting();
                var adminData = await _clientRepo.GetClientsAsync();
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                {
                    
                    return View(await _clientRepo.GetClientsAsync());
                    //return Json(adminData, JsonRequestBehavior.AllowGet); //View(data );// ; ;
                }
                
                return View(await _clientRepo.GetClientsAsync(x => x.CreatedBy.Equals(user)));
                //var result = await _clientRepo.GetClientsAsync();
                //var data = adminData.Where(x => x.CreatedBy == user);
                //return Json(data, JsonRequestBehavior.AllowGet);
                //return View();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //GET: Clients
        [HttpPost]
        public async Task<JsonResult> GetClientRecords()
        {
            var user = User.Identity.Name;


            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            //Find Order Column
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            //Search
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();
            //Paging Size (10,20,50,100)    
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            

            try
            {
                var adminData = await _clientRepo.GetClientsAsync();
                if (HttpContext.User.IsInRole(LegalGuideUtility.ADMINISTRATOR))
                {
                    var dataToReturn = adminData.Select(s => new ClientViewModel 
                    {ClientId=s.ClientId, FirstName= s.FirstName,LastName= s.LastName,EmailAddress= s.EmailAddress,PhoneNumber= s.PhoneNumber }
                    );
                    //DTO record
                    List<ClientViewModel> clientData = dataToReturn.ToList();

                    //Sorting
                    //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                    //{
                    //    clientData = clientData.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                    //}

                    //Sorting
                    if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                    {
                        if (sortColumn == "FirstName")
                        {
                            clientData = sortColumnDir == "asc" ? clientData.OrderBy(o => o.FirstName).ToList() : clientData.OrderByDescending(o => o.FirstName).ToList();
                        }
                        else if (sortColumn == "LastName")
                        {
                            clientData = sortColumnDir == "asc" ? clientData.OrderBy(o => o.LastName).ToList() : clientData.OrderByDescending(o => o.LastName).ToList();
                        }
                        else if (sortColumn == "EmailAddress")
                        {
                            clientData = sortColumnDir == "asc" ? clientData.OrderBy(o => o.EmailAddress).ToList() : clientData.OrderByDescending(o => o.EmailAddress).ToList();
                        }
                        else
                        {
                            clientData = sortColumnDir == "asc" ? clientData.OrderBy(o => o.PhoneNumber).ToList() : clientData.OrderByDescending(o => o.PhoneNumber).ToList();
                        }

                    }


                    //Search
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        clientData = clientData.Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower())
                                                                || x.LastName.ToLower().Contains(searchValue.ToLower())
                                                                || x.EmailAddress.ToLower().Contains(searchValue.ToLower())
                                                                || x.PhoneNumber.ToLower().Contains(searchValue.ToLower())).ToList();
                    }
                    //total number of row count
                    recordsTotal = clientData.Count();
                    //Paging
                    var aData = clientData.Skip(skip).Take(pageSize).ToList();
                    
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
                var data = adminData.Where(x => x.CreatedBy == user).Select(s => new ClientViewModel {ClientId=s.ClientId, FirstName= s.FirstName, LastName= s.LastName,EmailAddress= s.EmailAddress,PhoneNumber= s.PhoneNumber });

                List<ClientViewModel> resultData = data.ToList();

                //Sorting
                //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                //{
                //    resultData = resultData.OrderBy(sortColumn + " " + sortColumnDir).ToList();
                //}

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    if (sortColumn == "FirstName")
                    {
                        resultData = sortColumnDir == "asc" ? resultData.OrderBy(o => o.FirstName).ToList() : resultData.OrderByDescending(o => o.FirstName).ToList();
                    }
                    else if (sortColumn == "LastName")
                    {
                        resultData = sortColumnDir == "asc" ? resultData.OrderBy(o => o.LastName).ToList() : resultData.OrderByDescending(o => o.LastName).ToList();
                    }
                    else if (sortColumn == "EmailAddress")
                    {
                        resultData = sortColumnDir == "asc" ? resultData.OrderBy(o => o.EmailAddress).ToList() : resultData.OrderByDescending(o => o.EmailAddress).ToList();
                    }
                    else
                    {
                        resultData = sortColumnDir == "asc" ? resultData.OrderBy(o => o.PhoneNumber).ToList() : resultData.OrderByDescending(o => o.PhoneNumber).ToList();
                    }

                }

                //Search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    resultData = resultData.Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower())
                                                            || x.LastName.ToLower().Contains(searchValue.ToLower())
                                                            || x.EmailAddress.ToLower().Contains(searchValue.ToLower())
                                                            || x.PhoneNumber.ToLower().Contains(searchValue.ToLower())).ToList();
                }

                //total number of row count
                recordsTotal = resultData.Count();
                //Paging
                var cData = resultData.Skip(skip).Take(pageSize).ToList();
                //if (adminDisplayResult.Count == 0)
                //{
                //    adminDisplayResult = clientData.ToList();
                //}

                return Json(new
                {
                    draw=draw,
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
        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = await db.Clients.FindAsync(id);
            Client client = await _clientRepo.GetClientAsync(id);//.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        [HttpPost]
        public async Task<JsonResult> SaveClient(Client client)
        {
            try
            {
                if (!IsClientRegistered(client.FirstName, client.LastName, client.EmailAddress))
                {
                    int nextId = GetCurrentId() + 1;
                    string clienName = client.FirstName;
                    string firstLetter = clienName.Substring(0, 1);
                    string clientId = firstLetter + nextId.ToString() + "-" + DateTime.Today.ToShortDateString();
                    var user = User.Identity.Name;
                    client.CreatedBy = user;
                    client.CreatedOn = DateTime.Today;
                    client.ClientNumber = clientId;

                    _clientRepo.AddClient(client);
                    //db.Clients.Add(client);
                    await _clientRepo.CompleteAsync();
                    return Json(0, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Client Already Exist.";
                    //return View(client);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error occured while registering this client. Check and try again. " + ex;
                //return View(client);
                //throw ex;
            }
            return Json(0, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> Create(Client client)
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

                        //db.Clients.Add(client);
                        _clientRepo.AddClient(client);
                        await _clientRepo.CompleteAsync();
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
                //var client = db.Clients.Where(c => c.FirstName.Equals(firstName) && c.LastName.Equals(lastName) && c.EmailAddress.Equals(email)).FirstOrDefault();
                var client = _clientRepo.GetClient(c => c.FirstName.Equals(firstName) && c.LastName.Equals(lastName) && c.EmailAddress.Equals(email));
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
                return _clientRepo.MaxClient();
                //return (db.Clients.Max(x => x.ClientId));
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
            var client = await _clientRepo.GetClientAsync(id);
            //Client client = await db.Clients.FindAsync(id);
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
        //[ValidateAntiForgeryToken]
        public JsonResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {

                var user = User.Identity.Name;
                client.ModifiedBy = user;
                client.ModifiedOn = DateTime.Today;

                _clientRepo.UpdateClient(client);
                //await db.CompleteAsync();
                return Json(0, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            return Json(0, JsonRequestBehavior.AllowGet);
            //return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await _clientRepo.GetClientAsync(Convert.ToInt32(id));//.FindAsync(id);
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
            Client client = await _clientRepo.GetClientAsync(id);
            _clientRepo.DeleteClient(client);
            await _clientRepo.CompleteAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _clientRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
