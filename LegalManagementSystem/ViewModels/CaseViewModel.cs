using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.Models
{
    public class CaseViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string AreaOfPractice { get; set; }
        public int ClientId { get; set; }
        public int LineManagerId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime FiledOn { get; set; }
        public DateTime DueDate { get; set; }
        public string MatterNumber { get; set; }
        public string Priority { get; set; }
        public string MatterStage { get; set; }
        public string RequestedBy { get; set; }
        public string MatterValue { get; set; }
        public string EstimatedEffort { get; set; }
    }
}