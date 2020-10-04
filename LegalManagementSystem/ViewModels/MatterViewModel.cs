using System;
using System.Globalization;

namespace LegalManagementSystem.Models
{
    public class MatterViewModel
    {
        public string MatterNumber { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string PracticeArea { get; set; }
        public string DueDate { get; set; }
        public string FiledOn { get; set; }
        public string MatterStage { get; set; }
        public string ClientName { get; set; }
    }
}