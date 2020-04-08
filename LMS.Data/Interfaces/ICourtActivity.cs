using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Data.Interfaces
{
    public interface ICourtActivity
    {
        void AddCourtActivity(CourtActivity court);
        void Complete();
        Task CompleteAsync();
        void Dispose();

        void DeleteCourtActivity(CourtActivity court);
        CourtActivity GetCourtActivity(int id);
        Task<CourtActivity> GetCourtActivityAsync(int id);
        IEnumerable<CourtActivity> GetCourtActivities();
        IEnumerable<CourtActivity> GetCourtActivities(Expression<Func<CourtActivity,bool>>expression);
        CourtActivity GetCourtActivity(Expression<Func<CourtActivity,bool>> expression);
        Task<IEnumerable<CourtActivity>> GetCourtActivitiesAsync(Expression<Func<CourtActivity, bool>> expression);
        void UpdateCourtActivity(CourtActivity court);
    }
}
