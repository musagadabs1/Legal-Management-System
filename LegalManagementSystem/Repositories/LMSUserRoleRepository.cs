using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace LegalManagementSystem.Repositories
{
    public class LMSUserRoleRepository : ILMSUserRole
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();
        public UserRole GetUserRole()
        {
            try
            {
                return db.UserRoles.FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public UserRole GetUserRole(Expression<Func<UserRole, bool>> expression)
        {
            try
            {
                return db.UserRoles.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<UserRole> GetUserRoles()
        {
            try
            {
                return db.UserRoles.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<UserRole> GetUserRoles(Expression<Func<UserRole, bool>> expression)
        {
            try
            {
                return db.UserRoles.Where(expression).ToList();
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
    }
}