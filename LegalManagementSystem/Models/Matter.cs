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
    
    public partial class Matter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Matter()
        {
            this.CourtActivities = new HashSet<CourtActivity>();
        }
    
        public string Subject { get; set; }
        public string Description { get; set; }
        public string AreaOfPractice { get; set; }
        public int ClientId { get; set; }
        public Nullable<int> LineManagerId { get; set; }
        public Nullable<System.DateTime> ArrivalDate { get; set; }
        public System.DateTime FiledOn { get; set; }
        public System.DateTime DueDate { get; set; }
        public string MatterNumber { get; set; }
        public string Priority { get; set; }
        public string MatterStage { get; set; }
        public string RequestedBy { get; set; }
        public string MatterValue { get; set; }
        public string EstimatedEffort { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public string CaseNumber { get; set; }
        public string CourtStatus { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourtActivity> CourtActivities { get; set; }
    }
}
