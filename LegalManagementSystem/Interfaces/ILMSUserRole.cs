using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface ILMSUserRole
    {
        IEnumerable<UserRole> GetUserRoles();
        IEnumerable<UserRole> GetUserRoles(Expression<Func<UserRole,bool>>expression);
        UserRole GetUserRole();
        UserRole GetUserRole(Expression<Func<UserRole, bool>> expression);
        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
    }
}
