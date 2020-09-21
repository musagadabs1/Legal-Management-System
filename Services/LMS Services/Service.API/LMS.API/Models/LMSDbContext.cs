using LMS.Entities;
using System.Data.Entity;

namespace LMS.API.Models
{
    public class LMSDbContext:DbContext
    {
        public LMSDbContext() : base("name=MyCaseNewEntities")
        {

        }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<CourtActivity> CourtActivities { get; set; }
        public DbSet<AdvocateGroup> AdvocateGroups { get; set; }
        public DbSet<Certification> Certifications { get; set; }
        public DbSet<Dependant> Dependants { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<LineManager> LineManagers { get; set; }
        public DbSet<StaffMatter> StaffMatters { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<LoginUser> LoginUsers { get; set; }
        public DbSet<LicenseTable> LicenseTables { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<FileEvent> FileEvents { get; set; }
    }
}