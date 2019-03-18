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
    using System.ComponentModel.DataAnnotations;

    public partial class Staff
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Staff()
        {
            this.Certifications = new HashSet<Certification>();
            this.Dependants = new HashSet<Dependant>();
            this.Educations = new HashSet<Education>();
            this.Experiences = new HashSet<Experience>();
            this.FileEvents = new HashSet<FileEvent>();
        }
    
        public int Id { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedOn { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Birth")]
        public System.DateTime DOB { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Of Employment")]
        public System.DateTime DOE { get; set; }
        public Nullable<bool> Status { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Marital Status")]
        public string MaritalStatus { get; set; }
        public string ImagePath { get; set; }
        [Display(Name = "Office No")]
        public string OfficeNo { get; set; }
        [Required]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Personal Email")]
        public string PersonalEmail { get; set; }
        public string Relationship { get; set; }
        [Display(Name = "Next of Kin Telephone")]
        public string KTelephone { get; set; }
        [Display(Name = "Next of Kin Email")]
        public string NKEmail { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Next of Kin Address")]
        public string NKAddress { get; set; }
        public string Bank { get; set; }
        [Display(Name = "Account Number")]
        public Nullable<int> AccountNumber { get; set; }
        [Display(Name = "Next of Kin Full Name")]
        public string NKFullName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Staff Id")]
        public string StaffId { get; set; }
        [Required]
        [Display(Name = "Line Manager")]
        public int LineManagerId { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required]
        [Display(Name = "Year Call To Bar")]
        public Nullable<int> YearCallToBar { get; set; }
        public string Location { get; set; }
        [Display(Name = "Advocate Group")]
        public Nullable<int> AdvocateGroupId { get; set; }
        [Required]
        [Display(Name = "Personal Identity Number")]
        public string NationalIdentityNumber { get; set; }
        [Display(Name = "Blood Group")]
        public string BloodGroup { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Certification> Certifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Dependant> Dependants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Education> Educations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Experience> Experiences { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileEvent> FileEvents { get; set; }
        public virtual LineManager LineManager { get; set; }
    }
}
