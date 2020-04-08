using System;
using System.Collections.Generic;

namespace LMS.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public string RoleType { get; set; }
        public string RoleDescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public virtual ICollection<LoginUser> LoginUsers { get; set; }
    }
}
