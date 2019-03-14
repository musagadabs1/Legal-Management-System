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

    public partial class CourtActivity
    {
        public int Id { get; set; }
        [Display(Name = "Case Number")]
        [Required]
        public string MatterNumber { get; set; }
        [Display(Name = "Hearing Date")]
        [DataType(DataType.Date)]
        public System.DateTime DateHeared { get; set; }
        [Display(Name = "Court Name")]
        [Required]
        public string CourtName { get; set; }
        public string Location { get; set; }
        public string StaffId { get; set; }
        public string Status { get; set; }
        [Display(Name = "Plantiff Argument")]
        [DataType(DataType.MultilineText)]
        public string AdvocateArgument { get; set; }
        [Display(Name = "Defendant Argument")]
        [DataType(DataType.MultilineText)]
        public string OpponentArgument { get; set; }
        [Display(Name = "Advocate Note")]
        [DataType(DataType.MultilineText)]
        public string AdvocateNote { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        [Display(Name = "Date Adjourned")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateAdjourned { get; set; }
        [Display(Name="Defendant Name")]
        public string DefenseCounselName { get; set; }
    }
}

/*
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

    public partial class CourtActivity
    {
        
        
    }
}

     
     
     */
