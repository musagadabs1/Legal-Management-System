using System;

namespace LMS.Entities
{
    public class Document
    {
        public int DocumentId { get; set; }
        public string MatterNumber { get; set; }
        public string DocName { get; set; }
        public DateTime AssignedDate { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string DocPath { get; set; }
    }
}
