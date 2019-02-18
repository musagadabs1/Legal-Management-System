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
    
    public partial class Document
    {
        public int DocumentId { get; set; }
        public string FileNumber { get; set; }
        public string DocName { get; set; }
        public System.DateTime AssignedDate { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime DateModified { get; set; }
        public string DocPath { get; set; }
    
        public virtual File File { get; set; }
    }
}
