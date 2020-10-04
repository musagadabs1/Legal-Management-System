using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface IJudgement
    {
        void AddJudgement(Judgement judgement);
        void DeleteJudgement(Judgement judgement);
        Judgement GetJudgement(int? id);
        Task<Judgement> GetJudgementAsync(int? id);
        Task<IEnumerable<Judgement>> GetJudgementsAsync();
        Task<IEnumerable<Judgement>> GetJudgementsAsync(Expression<Func<Judgement, bool>> expression);
        IEnumerable<Judgement> GetJudgements(Expression<Func<Judgement, bool>> expression);
        IEnumerable<Judgement> GetJudgements();
        void UpdateJudgement(Judgement judgement);
        int Complete();
        Task<int> CompleteAsync();
        void Dispose();
    }
}
