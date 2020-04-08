using System;

namespace LMS.Entities
{
    public class Dependant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> PolicyNumber { get; set; }
        public DateTime EffectiveDate { get; set; }
        public Nullable<DateTime> EndDate { get; set; }
        public string Gender { get; set; }
        public string Relationship { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string StaffId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedOn { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
