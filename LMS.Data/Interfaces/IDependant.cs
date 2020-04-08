using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LMS.Data.Interfaces
{
    public interface IDependant
    {
        void AddDependant(Dependant dependant);
        void DeleteDependant(Dependant dependant);
        Dependant GetDependant(int id);
        IEnumerable<Dependant> GetDependant();
        Dependant GetDependant(Expression<Func<bool>> expression);
        void UpdateDependant(Dependant dependant);
    }
}
