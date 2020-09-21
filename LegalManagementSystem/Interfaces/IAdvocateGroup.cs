using LegalManagementSystem.Models;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface IAdvocateGroup
    {
        Task<AdvocateGroup> GetAdvocateGroupAsync(int? id);
        AdvocateGroup GetAdvocateGroup(int? id);
        void AddAdvocateGroup(AdvocateGroup advocate);
        void DeleteAdvocateGroup(AdvocateGroup advocate);
        void Dispose();
        IEnumerable<AdvocateGroup> GetAdvocateGroups();
        Task<int> CompleteAsync();
        IEnumerable<AdvocateGroupForDropDown> GetAllAdvocateGroupsForDropDown();
        AdvocateGroup GetAdvocateGroup(Func<AdvocateGroup, bool> expression);
        IEnumerable<AdvocateGroup> GetAdvocateGroups(Expression<Func<AdvocateGroup, bool>> expression);
        Task<IEnumerable<AdvocateGroup>> GetAdvocateGroupsAsync();
        void UpdateAdvocateGroup(AdvocateGroup advocate);

        int Complete { get; }
    }
}
