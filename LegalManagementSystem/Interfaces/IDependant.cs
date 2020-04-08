using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
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
