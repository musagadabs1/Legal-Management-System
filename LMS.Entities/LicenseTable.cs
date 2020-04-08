using System;
namespace LMS.Entities
{
    public class LicenseTable
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string SoftwareVersion { get; set; }
        public string ApprovedKey { get; set; }
        public DateTime ValidityFrom { get; set; }
        public DateTime ValidityTo { get; set; }
        public string ApprovedDocument { get; set; }
        public string ApprovedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsLicensed { get; set; }
        public bool? IsExpired { get; set; }
    }
}
