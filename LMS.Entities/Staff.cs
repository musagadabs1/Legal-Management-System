using System;
using System.Collections.Generic;
namespace LMS.Entities
{
    public class Staff
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOE { get; set; }
        public Nullable<bool> Status { get; set; }
        public string Address { get; set; }
        public string MaritalStatus { get; set; }
        public string ImagePath { get; set; }
        public string OfficeNo { get; set; }
        public string MobileNo { get; set; }
        public string EmailAddress { get; set; }
        public string PersonalEmail { get; set; }
        public string Relationship { get; set; }
        public string KTelephone { get; set; }
        public string NKEmail { get; set; }
        public string NKAddress { get; set; }
        public string Bank { get; set; }
        public Nullable<int> AccountNumber { get; set; }
        public string NKFullName { get; set; }
        public string Password { get; set; }
        public string StaffId { get; set; }
        public int LineManagerId { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public Nullable<int> YearCallToBar { get; set; }
        public string Location { get; set; }
        public Nullable<int> AdvocateGroupId { get; set; }
        public string NationalIdentityNumber { get; set; }
        public string BloodGroup { get; set; }

        public virtual ICollection<Certification> Certifications { get; set; }
        public virtual ICollection<Dependant> Dependants { get; set; }
        public virtual ICollection<Education> Educations { get; set; }
        public virtual ICollection<Experience> Experiences { get; set; }
        public virtual ICollection<FileEvent> FileEvents { get; set; }
        public virtual LineManager LineManager { get; set; }
    }
}
