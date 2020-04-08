using System;

namespace LMS.Entities
{
    public class Experience
    {
        public int Id { get; set; }
        public string Employer { get; set; }
        public string Designation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public double? Salary { get; set; }
        public string StaffId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
