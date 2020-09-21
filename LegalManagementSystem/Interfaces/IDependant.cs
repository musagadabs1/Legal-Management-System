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
        int Complete();
        Task<int> CompleteAsync();
        void DeleteDependant(Dependant dependant);
        void Dispose();
        Dependant GetDependant(int? id);
        IEnumerable<Dependant> GetDependants();
        Dependant GetDependant(Expression<Func<Dependant,bool>> expression);
        void UpdateDependant(Dependant dependant);
        IEnumerable<Dependant> GetDependants(Expression<Func<Dependant, bool>> expression);
        Task<IEnumerable<Dependant>> GetDependantsAsync();
        Task<IEnumerable<Dependant>> GetDependantsAsync(Expression<Func<Dependant, bool>> expression);
        Task<IEnumerable<Dependant>> GetDependantsWithStaffAsync();
        Task<IEnumerable<Dependant>> GetDependantsWithStaffAsync(Expression<Func<Dependant, bool>> expression);
        Task<Dependant> GetDependantAsync(int? id);
    }
}
