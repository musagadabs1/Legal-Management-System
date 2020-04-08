using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace LegalManagementSystem.Repositories
{
    public class UserRepository : ILMSUser
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();

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

        public void CreateUser(LoginUser user)
        {
            try
            {
                db.LoginUsers.Add(user);
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

        public int GetAdvocateGroupIdByUsername(string username)
        {
            int id = 0;
            try
            {
                var getId = db.LoginUsers.Where(x => x.Username.Equals(username)).FirstOrDefault();

                if (getId != null)
                {
                    id = (int)getId.AdvocateGroupId;
                }
                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public LoginUser GetLoginUser(string userName, string password)
        {
            try
            {
                return db.LoginUsers.FirstOrDefault(x => x.Username.Equals(userName) && x.Password.Equals(password));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public LoginUser GetLoginUser(Expression<Func<LoginUser, bool>> expression)
        {
            try
            {
                return db.LoginUsers.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<LoginUser> GetLoginUsers()
        {
            try
            {
                return db.LoginUsers.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<LoginUser> GetLoginUsers(Expression<Func<LoginUser, bool>> expression)
        {
            try
            {
                return db.LoginUsers.Where(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetStaffEmailByLoginName(string username)
        {
            try
            {
                string email = string.Empty;
                var loginUser = db.LoginUsers.Where(x => x.Username.Equals(username)).FirstOrDefault();
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

        public string GetUserFullNameByLoginName(string username)
        {
            try
            {
                string fullName = string.Empty;
                var loginUser = db.LoginUsers.Where(x => x.Username.Equals(username)).FirstOrDefault();
                if (loginUser != null)
                {
                    fullName = loginUser.FullName;
                }
                return fullName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateUser(LoginUser user)
        {
            try
            {
                db.Entry(user).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}