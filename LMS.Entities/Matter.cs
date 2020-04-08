using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Entities
{
    public class Matter
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string AreaOfPractice { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> LineManagerId { get; set; }
        public Nullable<System.DateTime> ArrivalDate { get; set; }
        public Nullable<System.DateTime> FiledOn { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string MatterNumber { get; set; }
        public string Priority { get; set; }
        public string MatterStage { get; set; }
        public string RequestedBy { get; set; }
        public string MatterValue { get; set; }
        public string EstimatedEffort { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string CaseNumber { get; set; }
        public string CourtStatus { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<CourtActivity> CourtActivities { get; set; }
        public virtual ICollection<LMSTask> LMSTasks { get; set; }
    }
}
