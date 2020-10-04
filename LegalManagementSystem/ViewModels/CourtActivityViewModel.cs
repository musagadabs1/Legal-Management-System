using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.Models
{
    public class CourtActivityViewModel
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public string MatterNumber { get; set; }
        public string DateHeared { get; set; }
        public string CourtName { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string AdvocateArgument { get; set; }
        public string OpponentArgument { get; set; }
        public string DefenseCounselName { get; set; }
        public string AdvocateNote { get; set; }
        public string DateAdjourned { get; set; }
    }
}