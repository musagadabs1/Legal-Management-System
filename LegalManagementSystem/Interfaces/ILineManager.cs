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
    public interface ILineManager
    {
        void AddManager(LineManager lineManager);
        void DeleteManager(LineManager lineManager);
        Task<IEnumerable<LineManager>> GetManagersAsync(Expression<Func<LineManager, bool>> expression);
        LineManager GetLineManager(int? id);
        void UpdateClient(LineManager lineManager);
        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
        IEnumerable<ManagerForDropDown> GetAllManagersForDropDown();
        Task<IEnumerable<LineManager>> GetManagersAsync();
        IEnumerable<LineManager> GetManagers();
        Task<LineManager> GetManagerAsync(int? id);
        IEnumerable<LineManager> GetManagers(Expression<Func<LineManager, bool>> expression);
    }
}
