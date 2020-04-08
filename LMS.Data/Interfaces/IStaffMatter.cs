using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Data.Interfaces
{
    public interface IStaffMatter
    {
        void AddStaffMatter(StaffMatter staffMatter);
        void DeleteStaffMatter(StaffMatter staffMatter);
        StaffMatter GetStaffMatter(int id);
        IEnumerable<StaffMatter> GetStaffMatters();
        IEnumerable<StaffMatter> GetStaffMatters(Expression<Func<StaffMatter, bool>> expression);
        StaffMatter GetStaffMatter(Expression<Func<StaffMatter,bool>> expression);
        void UpdateStaffMatter(StaffMatter staff);
        string StaffIdInStaffMatters(string matterNumber);
        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
    }
}
