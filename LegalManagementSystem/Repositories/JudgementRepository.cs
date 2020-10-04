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
    public class JudgementRepository:IJudgement
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public int Complete()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> CompleteAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
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

        public void AddJudgement(Judgement judgement)
        {
            try
            {
                db.Judgements.Add(judgement);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteJudgement(Judgement judgement)
        {
            try
            {
                db.Judgements.Remove(judgement);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Judgement GetJudgement(int? id)
        {
            try
            {
                return db.Judgements.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Judgement> GetJudgementAsync(int? id)
        {
            try
            {
                return await db.Judgements.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Judgement>> GetJudgementsAsync()
        {
            try
            {
                return await db.Judgements.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Judgement>> GetJudgementsAsync(Expression<Func<Judgement, bool>> expression)
        {
            try
            {
                return await db.Judgements.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Judgement> GetJudgements(Expression<Func<Judgement, bool>> expression)
        {
            try
            {
                return db.Judgements.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Judgement> GetJudgements()
        {
            throw new NotImplementedException();
        }

        public void UpdateJudgement(Judgement judgement)
        {
            try
            {
                db.Entry(judgement).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}