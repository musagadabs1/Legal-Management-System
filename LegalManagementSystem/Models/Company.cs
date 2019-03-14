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

    public partial class Company
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Company Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Company Short Name")]
        public string ShortName { get; set; }
        //[Required]
        [Display(Name = "Company Legal Type")]
        public string CompanyLegalType { get; set; }
        //[Required]
        [Display(Name = "Company Group")]
        public string CompanyGroup { get; set; }
        public string Nationality { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
        //[Required]
        [Display(Name = "Company Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Company Mobile Number")]
        public string MobileNumber { get; set; }
        [Required]
        [Display(Name = "Company Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        //[Required]
        [Display(Name = "Company Lawyer")]
        public string CompanyLawyer { get; set; }
        public Nullable<double> Capital { get; set; }
        public string Currency { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
