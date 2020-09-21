using LegalManagementSystem.Models;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface IStaff
    {
        Staff GetStaffByEmail(string email);
       string GetStaffEmailByLoginName(string user);
        void AddStaff(Staff staff);
        string GetStaffIdByEmail(string email);
        string GetEmailByStaffId(string staffId);
        void DeleteStaff(Staff staff);
        Staff GetStaff(string id);
        IEnumerable<Staff> GetStaffs();
        Staff GetStaff(Expression<Func<Staff,bool>> expression);
        void UpdateStaff(Staff staff);
        string StaffId(string matterId);
        IEnumerable<StaffForDropDown> GetAllStaffForDropDown();
        void Dispose();
        Task<int> CompleteAsync();
        int Complete();
        IEnumerable<Staff> GetStaffs(Expression<Func<Staff, bool>> expression);
        Task<IEnumerable<Staff>> GetStaffsAsync(Expression<Func<Staff, bool>> expression);
        Task<IEnumerable<Staff>> GetStaffsAsync();
        Task<Staff> GetStaffAsync(string id);
    }
}
