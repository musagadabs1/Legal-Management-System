using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Data.Interfaces
{
    public interface IStaff
    {
        Staff GetStaffByEmail(string email);
        void AddStaff(Staff staff);
        void DeleteStaff(Staff staff);
        Staff GetStaff(string id);
        IEnumerable<Staff> GetStaffs();
        Staff GetStaff(Expression<Func<Staff,bool>> expression);
        void UpdateStaff(Staff staff);
        string StaffId(string matterId);
        //IEnumerable<StaffForDropDown> GetAllStaffForDropDown();
        void Dispose();
        Task<int> CompleteAsync();
        int Complete();
        IEnumerable<Staff> GetStaffs(Expression<Func<Staff, bool>> expression);
        Task<IEnumerable<Staff>> GetStaffsAsync(Expression<Func<Staff, bool>> expression);
        Task<IEnumerable<Staff>> GetStaffsAsync();
        Task<Staff> GetStaffAsync(string id);
    }
}
