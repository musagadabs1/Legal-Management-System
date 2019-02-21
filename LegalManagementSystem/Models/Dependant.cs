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
        [Display(Name ="First Name *")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }
        //[Required]
        [Display(Name = "Policy Number *")]
        public int PolicyNumber { get; set; }
        //[Required]
        [Display(Name = "Effective Date *")]
        [DataType(DataType.Date)]
        public System.DateTime EffectiveDate { get; set; }
        [Display(Name = "End Date *")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string Gender { get; set; }
        public string Relationship { get; set; }
        [Display(Name = "Date of Birth*")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        public string Description { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        public string StaffId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    
        public virtual Staff Staff { get; set; }
    }
}
