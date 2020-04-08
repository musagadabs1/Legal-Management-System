using System;

namespace LMS.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public string School { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? EndDate { get; set; }
        public string Major { get; set; }
        public DateTime? DateAwarded { get; set; }
        public bool? Graduated { get; set; }
        public string Qualification { get; set; }
        public string Description { get; set; }
        public string Grade { get; set; }
        public string StaffId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
