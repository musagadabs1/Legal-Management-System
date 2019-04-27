﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using LegalManagementSystem.Models;
using System.Web.Security;

namespace LegalManagementSystem.Controllers
{
    [Authorize(Roles = "ITAdmin")]
    public class AccountController : Controller
    {
        MyCaseNewEntities _context = new MyCaseNewEntities();

        public AccountController()
        {
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string username = model["txtUsername"];
            string password = model["txtPassword"];

            try
            {
                var dataItem = _context.LoginUsers.Where(x => x.Username.Equals(username) && x.Password.Equals(password)).First();
                if (dataItem == null)
                {
                    ViewBag.ErrorMsg = "Login Failed. Check Username/Password and try again.";
                    return View();
                }
                if (dataItem != null)
                {
                    var userFullName = LegalGuideUtility.GetUserFullNameByLoginName(dataItem.Username);
                    ViewBag.FullName = userFullName;
                    LegalGuideUtility.UserFullName = userFullName;
                    FormsAuthentication.SetAuthCookie(dataItem.Username, false);
                    if (IsExpired(userFullName) ==false && IsLicensed(userFullName)==true)
                    {
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                             && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    else if(LicenseType(userFullName).Equals("Trial"))
                    {
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                                                     && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            LegalGuideUtility.LicenseMessage = "Trial Period ends at " + TrialPeriod(userFullName);
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            //ViewBag.ErrorMsg = "Login Failed. Check Username/Password and try again.";
                            LegalGuideUtility.LicenseMessage = "Trial Period ends in " + TrialPeriod(userFullName) + " day(s)";
                            return RedirectToLocal(returnUrl);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMsg = "Software is expired. Please contact IT Administrator.";
                        return View();
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user/pass");
                    return View();
                }
            }
            catch (Exception /*ex*/)
            {

                ViewBag.ErrorMsg = "Login failed. Username or Password not correct.";// + ex.Message;
                //return View();
            }
            return View(model);
        }

        private bool IsExpired(string clientName)
        {
            try
            {
                var isExpired= _context.LicenseTables.Where(x => x.IsExpired ==true && x.ClientName.Equals(clientName)).FirstOrDefault();

                if (isExpired != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool IsLicensed(string clientName)
        {
            try
            {
                var getLicenseStatus = _context.LicenseTables.Where(x => x.IsLicensed==true && x.ClientName.Equals(clientName)).FirstOrDefault();

                if (getLicenseStatus != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private string LicenseType(string clientName)
        {
            try
            {
                var getLicenseType = _context.LicenseTables.Where(x => x.IsLicensed==false && x.ClientName.Equals(clientName)).FirstOrDefault();

                if (getLicenseType != null)
                {
                    return getLicenseType.SoftwareVersion;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private int TrialPeriod(string clientName)
        {
            try
            {
                //DateTime startTrial;
                //DateTime endTrial;
                int period = 0;
                var getLicenseType = _context.LicenseTables.Where(x => x.IsLicensed==false && x.ClientName.Equals(clientName)).FirstOrDefault();

                if (getLicenseType != null)
                {
                    period = getLicenseType.ValidityTo.Day - getLicenseType.ValidityFrom.Day;
                    return period;
                }
                return period;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //
        // GET: /Account/Register
        //[AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.RoleNames = new SelectList(_context.UserRoles.Where(u => u.RoleType != "ITAdmin" && u.RoleType != "ShrishAdmin").ToList(), "Id", "RoleType");
            ViewBag.AdvocateGroups=new SelectList(_context.AdvocateGroups.ToList(), "Id", "GroupName");
            return View();
        }
        private bool IsRegisteredUser(string username,string password)
        {
            try
            {
                var getUser = _context.LoginUsers.Where(l => l.Username.Equals(username) && l.Password.Equals(password)).FirstOrDefault();
                if (getUser != null)
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
        //
        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(LoginUser model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (IsRegisteredUser(model.Username,model.Password))
                    {
                        ViewBag.ErrorMsg = "Username already exist.";
                        ViewBag.RoleNames = new SelectList(_context.UserRoles.Where(u => u.RoleType != "Admin" && u.RoleType != "ShrishAdmin").ToList(), "Id", "RoleType");
                        ViewBag.AdvocateGroups = new SelectList(_context.AdvocateGroups.ToList(), "Id", "GroupName");
                        return View(model);
                    }

                    model.CreatedBy = model.Username;
                    model.CreatedOn = DateTime.Today.Date;
                    //model.Password = LegalGuideUtility.Encrypt(model.Password);
                    _context.LoginUsers.Add(model);
                    _context.SaveChanges();

                    return RedirectToAction("Login", "Account");
                }
                catch (Exception ex)
                {

                    ViewBag.ErrorMsg = "Something happened. please check and try again. " + ex.Message;
                    ViewBag.RoleNames = new SelectList(_context.UserRoles.Where(u => u.RoleType != "ITAdmin" && u.RoleType != "ShrishAdmin").ToList(), "Id", "RoleType");
                    ViewBag.AdvocateGroups = new SelectList(_context.AdvocateGroups.ToList(), "Id", "GroupName");
                    return View(model);
                }
            }
            else
            {
                //return RedirectToAction("Error");
                ViewBag.ErrorMsg = "Please fill in the required fields . ";
                //ViewBag.RoleNames = new SelectList(_context.UserRoles.Where(u => u.RoleType != "Admin").ToList(), "Id", "RoleType");
                //return View(model);

            }
            ViewBag.RoleNames = new SelectList(_context.UserRoles.Where(u => u.RoleType != "ITAdmin" && u.RoleType != "ShrishAdmin").ToList(), "Id", "RoleType");
            ViewBag.AdvocateGroups = new SelectList(_context.AdvocateGroups.ToList(), "Id", "GroupName");
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult Error()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
            //return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Matters");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}