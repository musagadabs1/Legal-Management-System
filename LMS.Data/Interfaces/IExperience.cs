using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LMS.Data.Interfaces
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
