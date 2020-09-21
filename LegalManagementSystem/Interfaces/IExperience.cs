using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface IExperience
    {
        void AddExperience(Experience experience);
        void DeleteExperience(Experience experience);
        Experience GetExperience(int? id);
        IEnumerable<Experience> GetExperiences();
        Experience GetExperience(Expression<Func<Experience,bool>> expression);
        void UpdateExperience(Experience experience);
        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
        IEnumerable<Experience> GetExperiences(Expression<Func<Experience, bool>> expression);
        Task<IEnumerable<Experience>> GetExperiencesAsync();
        Task<IEnumerable<Experience>> GetExperiencesAsync(Expression<Func<Experience, bool>> expression);
        Task<IEnumerable<Experience>> GetExperiencesWithStaffAsync(Expression<Func<Experience, bool>> expression);
        Task<IEnumerable<Experience>> GetExperiencesWithStaffAsync();
        Task<Experience> GetExperienceAsync(int? id);
    }
}
