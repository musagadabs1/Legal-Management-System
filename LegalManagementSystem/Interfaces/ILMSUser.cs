using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface ILMSUser
    {
        LoginUser GetLoginUser(string userName, string password);
        int GetAdvocateGroupIdByUsername(string username);
        string GetStaffEmailByLoginName(string username);
        string GetUserFullNameByLoginName(string username);
        LoginUser GetLoginUser(Expression<Func<LoginUser, bool>> expression);
        void CreateUser(LoginUser user);
        void UpdateUser(LoginUser user);
        IEnumerable<LoginUser> GetLoginUsers();
        IEnumerable<LoginUser> GetLoginUsers(Expression<Func<LoginUser, bool>> expression);
        int Complete();
        Task<int> CompleteAsync();
        void Dispose();

    }
}
