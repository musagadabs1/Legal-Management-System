using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Entities
{
    public class CourtActivity
    {
        public int Id { get; set; }
        public string MatterNumber { get; set; }
        public System.DateTime DateHeared { get; set; }
        public string CourtName { get; set; }
        public string Location { get; set; }
        public string StaffId { get; set; }
        public string Status { get; set; }
        public string AdvocateArgument { get; set; }
        public string OpponentArgument { get; set; }
        public string AdvocateNote { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }
        public Nullable<DateTime> DateAdjourned { get; set; }
        public string DefenseCounselName { get; set; }

        public virtual Matter Matter { get; set; }
    }
}
