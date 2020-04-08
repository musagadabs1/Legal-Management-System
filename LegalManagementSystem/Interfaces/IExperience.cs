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
        Experience GetExperience(int id);
        IEnumerable<Experience> GetSExperience();
        Experience GetExperience(Expression<Func<bool>> expression);
        void UpdateExperience(Experience experience);
    }
}
