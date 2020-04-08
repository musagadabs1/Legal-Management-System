using System;
namespace LMS.Entities
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public int? AdvocateGroupId { get; set; }

        public virtual UserRole UserRole { get; set; }
    }
}
