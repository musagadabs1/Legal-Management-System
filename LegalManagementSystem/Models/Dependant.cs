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

    public partial class Dependant
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Policy Number")]
        public Nullable<int> PolicyNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Effective Date")]
        public System.DateTime EffectiveDate { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public Nullable<System.DateTime> EndDate { get; set; }
        public string Gender { get; set; }
        public string Relationship { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        public string StaffId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }

        public virtual Staff Staff { get; set; }
    }
}
