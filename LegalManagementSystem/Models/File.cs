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
    
    public partial class File
    {
        public int Id { get; set; }
        public string FileNumber { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<System.DateTime> OpeningDate { get; set; }
        public string FilePath { get; set; }
        public string AdvocateId { get; set; }
        public Nullable<System.DateTime> ClosingDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }
}
