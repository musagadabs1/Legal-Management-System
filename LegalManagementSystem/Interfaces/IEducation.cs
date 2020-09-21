using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface IEducation
    {
        void AddEducation(Education education);
        int Complete { get; }

        void DeleteEducation(Education education);
        Education GetEducation(int? id);
        IEnumerable<Education> GetEducations();
        Education GetEducation(Expression<Func<Education, bool>> expression);
        void UpdateEducation(Education education);
        Task<int> CompleteAsync();
        void Dispose();
        Task<Education> GetEducationAsync(int? id);
        IEnumerable<Education> GetEducations(Expression<Func<Education, bool>> expression);
        Task<IEnumerable<Education>> GetEducationsAsync();
        Task<IEnumerable<Education>> GetEducationsAsync(Expression<Func<Education, bool>> expression);
        Task<IEnumerable<Education>> GetEducationsWithStaffAsync(Expression<Func<Education, bool>> expression);
        Task<IEnumerable<Education>> GetEducationsWithStaffAsync();
    }
}
