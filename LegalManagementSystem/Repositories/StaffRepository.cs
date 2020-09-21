using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace LegalManagementSystem.Repositories
{
    public class StaffRepository : IStaff
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddStaff(Staff staff)
        {
            try
            {
                db.Staffs.Add(staff);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int Complete()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> CompleteAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Dispose()
        {
            try
            {
                db.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DeleteStaff(Staff staff)
        {
            try
            {
                //var client = GetClient(id);
                db.Staffs.Remove(staff);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<StaffForDropDown> GetAllStaffForDropDown()
        {
            try
            {
                var stffDropDown = new List<StaffForDropDown>();
                var getStaff= db.GetAllStaffForDropDown().ToList();
                foreach (var staff in getStaff)
                {
                    stffDropDown.Add(new StaffForDropDown
                    {
                        StaffId = staff.StaffId,
                        StaffName = staff.StaffName
                    });
                }

                return stffDropDown;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Staff GetStaff(string id)
        {
            try
            {
                return db.Staffs.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Staff> GetStaffAsync(string id)
        {
            try
            {
                return await db.Staffs.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Staff> GetStaffs()
        {
            try
            {
                return db.Staffs.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Staff> GetStaffs(Expression<Func<Staff, bool>> expression)
        {
            try
            {
                return db.Staffs.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Staff>> GetStaffsAsync(Expression<Func<Staff, bool>> expression)
        {
            try
            {
                return await db.Staffs.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Staff GetStaff(Expression<Func<Staff,bool>> expression)
        {
            try
            {
                return db.Staffs.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Staff GetStaffByEmail(string email)
        {
            try
            {
                return db.Staffs.FirstOrDefault(x => x.EmailAddress.Equals(email));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string StaffId(string matterId)
        {
            try
            {
                return db.StaffMatters.Where(x => x.MatterNumber.Equals(matterId)).Distinct().FirstOrDefault().StaffId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateStaff(Staff staff)
        {
            try
            {
                db.Entry(staff).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Staff>> GetStaffsAsync()
        {
            try
            {
                return await db.Staffs.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GetStaffIdByEmail(string email)
        {
            try
            {
                string staffId = string.Empty;
                var staff = db.Staffs.FirstOrDefault(x => x.EmailAddress.Equals(email));
                if (staff != null)
                {
                    staffId = staff.StaffId;
                    return staffId;
                }
                return staffId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string GetStaffEmailByLoginName(string user)
        {
            try
            {
                string email = string.Empty;
                var loginUser = db.LoginUsers.FirstOrDefault(x => x.Username.Equals(user));
                if (loginUser != null)
                {
                    email = loginUser.EmailAddress;
                }
                return email;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetEmailByStaffId(string staffId)
        {
            try
            {
                var email = (from staff in db.Staffs where staff.StaffId.Equals(staffId) select staff.EmailAddress).FirstOrDefault();
                if (email==null || string.IsNullOrEmpty(email))
                {
                    return string.Empty;
                }
                return email;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}