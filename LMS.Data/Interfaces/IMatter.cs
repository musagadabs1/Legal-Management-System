using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Data.Interfaces
{
    public interface IMatter
    {
        void AddMatter(Matter matter);
        int Complete();
        Task<int> CompleteAsync();
        void DeleteMatter(Matter matter);
        Matter GetMatter(string id);
        Task<Matter> GetMatterAsync(string id);
        IEnumerable<Matter> GetMatters();
        Task<IEnumerable<Matter>> GetMattersAsync();
        Task<IEnumerable<Matter>> GetMattersIncludeClientAsync();
        Matter GetMatter(Expression<Func<Matter,bool>> expression);
        void UpdateMatter(Matter matter);
        Matter GetMatterByMatterNumber(string matterNumber);
        //IEnumerable<MatterForDropDown> GetMatterForDropDowns();
        IEnumerable<Matter> GetMatters(Expression<Func<Matter, bool>> expression);
        Task<IEnumerable<Matter>> GetMattersAsync(Expression<Func<Matter, bool>> expression);
        Task<IEnumerable<Matter>> GetMattersIncludeClientAsync(Expression<Func<Matter, bool>> expression);
        int GetCurrentId();
        void ConFigProxy();
        void Dispose();
        //IEnumerable<Advocate> AdvocateGroup();
    }
}
