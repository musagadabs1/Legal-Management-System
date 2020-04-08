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
        void DeleteEducation(Education education);
        Education GetEducation(int id);
        IEnumerable<Education> GetEducation();
        Education GetEducation(Expression<Func<bool>> expression);
        void UpdateEducation(Education education);
    }
}
