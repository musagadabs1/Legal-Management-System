using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LMS.Data.Interfaces
{
    public interface IEducation
    {
        void AddEducation(Education education);
        void DeleteEducation(Education education);
        Education GetEducation(int id);
        IEnumerable<Education> GetEducation();
        Education GetEducation(Expression<Func<bool>> expression);
        void UpdateEducation(Education education);
    }
}
