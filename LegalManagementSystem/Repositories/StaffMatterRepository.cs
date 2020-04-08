using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Repositories
{
    public class StaffMatterRepository : IStaffMatter
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddStaffMatter(StaffMatter staffMatter)
        {
            try
            {
                db.StaffMatters.Add(staffMatter);
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

        public void DeleteStaffMatter(StaffMatter staffMatter)
        {
            try
            {
                //var client = GetClient(id);
                db.StaffMatters.Remove(staffMatter);
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

        public StaffMatter GetStaffMatter(int id)
        {
            try
            {
                return db.StaffMatters.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public StaffMatter GetStaffMatter(Expression<Func<StaffMatter, bool>> expression)
        {
            try
            {
                return db.StaffMatters.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<StaffMatter> GetStaffMatters()
        {
            try
            {
                return db.StaffMatters.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<StaffMatter> GetStaffMatters(Expression<Func<StaffMatter, bool>> expression)
        {
            try
            {
                return db.StaffMatters.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string StaffIdInStaffMatters(string matterNumber)
        {
            try
            {
                return db.StaffMatters.Where(x => x.MatterNumber.Equals(matterNumber)).Distinct().FirstOrDefault().StaffId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateStaffMatter(StaffMatter staff)
        {
            try
            {
                //var client = GetClient(id);
                db.Entry(staff).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}