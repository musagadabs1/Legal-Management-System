using System;

namespace LMS.Entities
{
    public class Certification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CertificationType { get; set; }
        public Nullable<DateTime> DateAchieved { get; set; }
        public string Skilled { get; set; }
        public string StaffId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string ModeifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
