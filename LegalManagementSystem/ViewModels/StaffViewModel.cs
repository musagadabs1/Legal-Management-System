using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.ViewModels
{
    public class StaffViewModel
    {
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string DOB { get; set; }
        public string DOE { get; set; }
    }
}