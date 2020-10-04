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
    public class ExperienceRepository : IExperience
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();

        public void AddExperience(Experience experience)
        {
            try
            {
                db.Experiences.Add(experience);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
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
        public void DeleteExperience(Experience experience)
        {
            try
            {
                db.Experiences.Remove(experience);
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
        public Experience GetExperience(int? id)
        {
            try
            {
                return db.Experiences.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Experience> GetExperienceAsync(int? id)
        {
            try
            {
                return await db.Experiences.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Experience GetExperience(Expression<Func<Experience,bool>> expression)
        {
            try
            {
                return db.Experiences.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Experience> GetExperiences()
        {
            try
            {
                return db.Experiences.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Experience> GetExperiences(Expression<Func<Experience, bool>> expression)
        {
            try
            {
                return db.Experiences.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Experience>> GetExperiencesAsync()
        {
            try
            {
                return await db.Experiences.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Experience>> GetExperiencesAsync(Expression<Func<Experience, bool>> expression)
        {
            try
            {
                return await db.Experiences.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Experience>> GetExperiencesWithStaffAsync(Expression<Func<Experience, bool>> expression)
        {
            try
            {
                return await db.Experiences.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Experience>> GetExperiencesWithStaffAsync()
        {
            try
            {
                return await db.Experiences.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateExperience(Experience experience)
        {
            try
            {
                db.Entry(experience).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}