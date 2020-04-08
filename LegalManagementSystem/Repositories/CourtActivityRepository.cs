using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace LegalManagementSystem.Repositories
{
    public class CourtActivityRepository : ICourtActivity
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddCourtActivity(CourtActivity court)
        {
            try
            {
                db.CourtActivities.Add(court);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Complete()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task CompleteAsync()
        {
            try
            {
              await  db.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteCourtActivity(CourtActivity court)
        {
            try
            {
                //var cert = GetCertification(id);
                db.CourtActivities.Remove(court);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                db.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CourtActivity>> GetCourtActivitiesAsync(Expression<Func<CourtActivity, bool>> expression)
        {
            try
            {
                return await db.CourtActivities.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CourtActivity GetCourtActivity(int id)
        {
            try
            {
                return db.CourtActivities.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<CourtActivity> GetCourtActivities()
        {
            try
            {
                return db.CourtActivities.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CourtActivity GetCourtActivity(Expression<Func<CourtActivity,bool>> expression)
        {
            try
            {
                return db.CourtActivities.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<CourtActivity> GetCourtActivityAsync(int id)
        {
            try
            {
                return await db.CourtActivities.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateCourtActivity(CourtActivity court)
        {
            try
            {
                //var cert = GetCertification(id);
                db.Entry(court).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<CourtActivity> GetCourtActivities(Expression<Func<CourtActivity, bool>> expression)
        {
            try
            {
                return db.CourtActivities.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}