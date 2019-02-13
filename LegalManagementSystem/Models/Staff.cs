//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LegalManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            this.Certifications = new HashSet<Certification>();
            this.Dependants = new HashSet<Dependant>();
            this.Educations = new HashSet<Education>();
            this.Experiences = new HashSet<Experience>();
            this.Files = new HashSet<File>();
            this.Libraries = new HashSet<Library>();
        }
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        public string LastName { get; set; }
        public System.DateTime DOB { get; set; }
        public System.DateTime DOE { get; set; }
        public string Status { get; set; }
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
        public string LineManager { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public Nullable<int> YearCallToBar { get; set; }
        public string Location { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Certification> Certifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dependant> Dependants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Education> Educations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Experience> Experiences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<File> Files { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Library> Libraries { get; set; }
    }
}
