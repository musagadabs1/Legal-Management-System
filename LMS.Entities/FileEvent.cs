using System;

namespace LMS.Entities
{
    public class FileEvent
    {
        public int Id { get; set; }
        public string FileNumber { get; set; }
        public string StaffId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string EventName { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Completed { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
