﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LegalManagementSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class MyCaseNewEntities : DbContext
    {
        public MyCaseNewEntities()
            : base("name=MyCaseNewEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdvocateGroup> AdvocateGroups { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<CalendarStaff> CalendarStaffs { get; set; }
        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<ClientMatterAcceptanceForm> ClientMatterAcceptanceForms { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CourtActivity> CourtActivities { get; set; }
        public virtual DbSet<Dependant> Dependants { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<FileEvent> FileEvents { get; set; }
        public virtual DbSet<LicenseTable> LicenseTables { get; set; }
        public virtual DbSet<LineManager> LineManagers { get; set; }
        public virtual DbSet<LMSTask> LMSTasks { get; set; }
        public virtual DbSet<LoginUser> LoginUsers { get; set; }
        public virtual DbSet<Matter> Matters { get; set; }
        public virtual DbSet<StaffGroup> StaffGroups { get; set; }
        public virtual DbSet<StaffMatter> StaffMatters { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Todo> Todoes { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
    
        public virtual ObjectResult<GetAllAdvocateGroups_Result> GetAllAdvocateGroups()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllAdvocateGroups_Result>("GetAllAdvocateGroups");
        }
    
        public virtual ObjectResult<GetAllCalendarForDropdown_Result> GetAllCalendarForDropdown()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCalendarForDropdown_Result>("GetAllCalendarForDropdown");
        }
    
        public virtual int GetAllCases()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAllCases");
        }
    
        public virtual int GetAllCasesBetweenDates(Nullable<System.DateTime> fromDate, Nullable<System.DateTime> toDate)
        {
            var fromDateParameter = fromDate.HasValue ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(System.DateTime));
    
            var toDateParameter = toDate.HasValue ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAllCasesBetweenDates", fromDateParameter, toDateParameter);
        }
    
        public virtual int GetAllCasesByCaseNumber(string caseNumber)
        {
            var caseNumberParameter = caseNumber != null ?
                new ObjectParameter("caseNumber", caseNumber) :
                new ObjectParameter("caseNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAllCasesByCaseNumber", caseNumberParameter);
        }
    
        public virtual int GetAllCasesByClientName(string clientfirstName, string clientMiddleName, string clientLastName)
        {
            var clientfirstNameParameter = clientfirstName != null ?
                new ObjectParameter("clientfirstName", clientfirstName) :
                new ObjectParameter("clientfirstName", typeof(string));
    
            var clientMiddleNameParameter = clientMiddleName != null ?
                new ObjectParameter("clientMiddleName", clientMiddleName) :
                new ObjectParameter("clientMiddleName", typeof(string));
    
            var clientLastNameParameter = clientLastName != null ?
                new ObjectParameter("clientLastName", clientLastName) :
                new ObjectParameter("clientLastName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAllCasesByClientName", clientfirstNameParameter, clientMiddleNameParameter, clientLastNameParameter);
        }
    
        public virtual int GetAllCasesByPracticeArea(string pArea)
        {
            var pAreaParameter = pArea != null ?
                new ObjectParameter("pArea", pArea) :
                new ObjectParameter("pArea", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetAllCasesByPracticeArea", pAreaParameter);
        }
    
        public virtual ObjectResult<GetAllClientByClientNumber_Result> GetAllClientByClientNumber(string clientNumber)
        {
            var clientNumberParameter = clientNumber != null ?
                new ObjectParameter("clientNumber", clientNumber) :
                new ObjectParameter("clientNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllClientByClientNumber_Result>("GetAllClientByClientNumber", clientNumberParameter);
        }
    
        public virtual ObjectResult<GetAllClientByClientTown_Result> GetAllClientByClientTown(string clientTown)
        {
            var clientTownParameter = clientTown != null ?
                new ObjectParameter("clientTown", clientTown) :
                new ObjectParameter("clientTown", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllClientByClientTown_Result>("GetAllClientByClientTown", clientTownParameter);
        }
    
        public virtual ObjectResult<GetAllClientForDropDown_Result> GetAllClientForDropDown()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllClientForDropDown_Result>("GetAllClientForDropDown");
        }
    
        public virtual ObjectResult<GetAllClientNames_Result> GetAllClientNames()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllClientNames_Result>("GetAllClientNames");
        }
    
        public virtual ObjectResult<GetAllClients_Result> GetAllClients()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllClients_Result>("GetAllClients");
        }
    
        public virtual ObjectResult<GetAllCompaniesForDropDown_Result> GetAllCompaniesForDropDown()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCompaniesForDropDown_Result>("GetAllCompaniesForDropDown");
        }
    
        public virtual ObjectResult<GetAllCourtActivities_Result> GetAllCourtActivities()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCourtActivities_Result>("GetAllCourtActivities");
        }
    
        public virtual ObjectResult<GetAllCourtActivitiesByCaseNumber_Result> GetAllCourtActivitiesByCaseNumber(string caseNumber)
        {
            var caseNumberParameter = caseNumber != null ?
                new ObjectParameter("caseNumber", caseNumber) :
                new ObjectParameter("caseNumber", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCourtActivitiesByCaseNumber_Result>("GetAllCourtActivitiesByCaseNumber", caseNumberParameter);
        }
    
        public virtual ObjectResult<GetAllCourtActivitiesByCourtName_Result> GetAllCourtActivitiesByCourtName(string courtName)
        {
            var courtNameParameter = courtName != null ?
                new ObjectParameter("courtName", courtName) :
                new ObjectParameter("courtName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCourtActivitiesByCourtName_Result>("GetAllCourtActivitiesByCourtName", courtNameParameter);
        }
    
        public virtual ObjectResult<GetAllCourtActivitiesByHearingDate_Result> GetAllCourtActivitiesByHearingDate(Nullable<System.DateTime> hearingDate)
        {
            var hearingDateParameter = hearingDate.HasValue ?
                new ObjectParameter("hearingDate", hearingDate) :
                new ObjectParameter("hearingDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCourtActivitiesByHearingDate_Result>("GetAllCourtActivitiesByHearingDate", hearingDateParameter);
        }
    
        public virtual ObjectResult<GetAllCourtActivitiesByStatus_Result> GetAllCourtActivitiesByStatus(string status)
        {
            var statusParameter = status != null ?
                new ObjectParameter("status", status) :
                new ObjectParameter("status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCourtActivitiesByStatus_Result>("GetAllCourtActivitiesByStatus", statusParameter);
        }
    
        public virtual ObjectResult<GetAllCourtNames_Result> GetAllCourtNames()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllCourtNames_Result>("GetAllCourtNames");
        }
    
        public virtual ObjectResult<GetAllLocationsForDropdown_Result> GetAllLocationsForDropdown()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllLocationsForDropdown_Result>("GetAllLocationsForDropdown");
        }
    
        public virtual ObjectResult<GetAllMattersForDropDown_Result> GetAllMattersForDropDown()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllMattersForDropDown_Result>("GetAllMattersForDropDown");
        }
    
        public virtual ObjectResult<GetAllStaff_Result> GetAllStaff()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllStaff_Result>("GetAllStaff");
        }
    
        public virtual ObjectResult<GetAllStaffForDropDown_Result> GetAllStaffForDropDown()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllStaffForDropDown_Result>("GetAllStaffForDropDown");
        }
    
        public virtual ObjectResult<GetAllTasksForDropDown_Result> GetAllTasksForDropDown()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllTasksForDropDown_Result>("GetAllTasksForDropDown");
        }
    
        public virtual int GetCasesByFiledDate(Nullable<System.DateTime> filedDate)
        {
            var filedDateParameter = filedDate.HasValue ?
                new ObjectParameter("filedDate", filedDate) :
                new ObjectParameter("filedDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetCasesByFiledDate", filedDateParameter);
        }
    
        public virtual ObjectResult<GetClientByFirstNameOrLastName_Result> GetClientByFirstNameOrLastName(string firstName, string lastName)
        {
            var firstNameParameter = firstName != null ?
                new ObjectParameter("firstName", firstName) :
                new ObjectParameter("firstName", typeof(string));
    
            var lastNameParameter = lastName != null ?
                new ObjectParameter("lastName", lastName) :
                new ObjectParameter("lastName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetClientByFirstNameOrLastName_Result>("GetClientByFirstNameOrLastName", firstNameParameter, lastNameParameter);
        }
    
        public virtual ObjectResult<GEtListOfDocuments_Result> GEtListOfDocuments()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GEtListOfDocuments_Result>("GEtListOfDocuments");
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_GetLineManagers_Result> sp_GetLineManagers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetLineManagers_Result>("sp_GetLineManagers");
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
