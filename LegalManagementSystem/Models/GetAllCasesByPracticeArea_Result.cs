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
    
    public partial class GetAllCasesByPracticeArea_Result
    {
        public int SNo { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Client_Name { get; set; }
        public string Practice_Area { get; set; }
        public Nullable<System.DateTime> Arrival_Date { get; set; }
        public Nullable<System.DateTime> Date_Filed { get; set; }
        public string Case_Number { get; set; }
        public Nullable<System.DateTime> Due_Date { get; set; }
        public string Priority { get; set; }
        public string Case_Stage { get; set; }
        public string Case_Value { get; set; }
    }
}
