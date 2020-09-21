using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.Models
{
    public class CaseDocument
    {
        public int Id { get; set; }
        public string MatterNumber { get; set; }
        public string DocName { get; set; }
        public System.DateTime AssignedDate { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string DocPath { get; set; }
        public string Matter { get; set; }
    }
}