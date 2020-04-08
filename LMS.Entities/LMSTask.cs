using System;

namespace LMS.Entities
{
    public class LMSTask
    {
        public int Id { get; set; }
        public string TaskType { get; set; }
        public string Description { get; set; }
        public string MatterNumber { get; set; }
        public int NotifyDays { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string Priority { get; set; }
        public string Reporter { get; set; }
        public Nullable<int> AdvocateGroupId { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }

        public virtual Matter Matter { get; set; }
    }
}
