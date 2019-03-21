using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.ViewModels
{
    public class MatterViewModel
    {
        public string Subject { get; set; }
        public string Description { get; set; }
        public string AreaofPractice { get; set; }
        public string MatterNumber { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime FiledOn { get; set; }
        public DateTime DueDate { get; set; }
        public string Priority { get; set; }
        public string MatterStage { get; set; }
    }
}