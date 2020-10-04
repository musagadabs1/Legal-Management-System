using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.ViewModels
{
    public class StaffVM
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOE { get; set; }
        public string Address { get; set; }
        public string MaritalStatus { get; set; }
        //public HttpPostedFileBase File { get; set; }
        public string OfficeNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string PersonalEmail { get; set; }
        public string Relationship { get; set; }
        public string KTelephone { get; set; }
        public string NKEmail { get; set; }
        public string NKAddress { get; set; }
        public string Bank { get; set; }
        public int AccountNumber { get; set; }
        public string NKFullName { get; set; }
        public string Password { get; set; }
        public string StaffId { get; set; }
        public int LineManagerId { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public int YearCallToBar { get; set; }
        public string Location { get; set; }
        public int AdvocateGroupId { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string BloodGroup { get; set; }
    }
}