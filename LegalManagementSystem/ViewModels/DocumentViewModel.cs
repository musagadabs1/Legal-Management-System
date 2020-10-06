using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.ViewModels
{
    public class DocumentViewModel
    {
        public int DocumentId { get; set; }
        public string DocName { get; set; }
        public string AssignedDate { get; set; }
        public string Description { get; set; }
    }
}