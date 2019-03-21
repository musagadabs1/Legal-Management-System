using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.Models
{
    public class CourtActivityViewModel
    {
        public string MatterNumber { get; set; }
        public DateTime DateHeared { get; set; }
        public string CourtName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string AdvocateArgument { get; set; }
        public string OpponentArgument { get; set; }
        public string DefenseCounselName { get; set; }
        public string AdvocateNote { get; set; }
        public DateTime DateAdjourned { get; set; }
    }
}