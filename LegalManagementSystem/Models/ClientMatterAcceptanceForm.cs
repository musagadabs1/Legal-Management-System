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

    public partial class ClientMatterAcceptanceForm
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="Client Name")]
        public int ClientId { get; set; }
        //[Required]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Client Industry")]
        //[DataType(DataType.MultilineText)]
        [Required]
        public string Industry { get; set; }
        [Display(Name ="Agreed Fee")]
        [Required]
        public double AgreedFee { get; set; }
        [Display(Name = "Payment Terms")]
        [Required]
        public string PaymentTerms { get; set; }
        [Display(Name = "Time Frame")]
        [Required]
        public int TimeFrame { get; set; }
        [Display(Name = "Matter Manager")]
        [Required]
        public string MatterManager { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    
        public virtual Client Client { get; set; }
    }
}