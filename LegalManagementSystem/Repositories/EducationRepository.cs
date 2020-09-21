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
    public class EducationRepository : IEducation
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddEducation(Education education)
        {
            try
            {
                db.Educations.Add(education);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int Complete
        {
            get
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
        public void DeleteEducation(Education education)
        {
            try
            {
                db.Educations.Remove(education);
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
        public Education GetEducation(int? id)
        {
            try
            {
                return db.Educations.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Education> GetEducationAsync(int? id)
        {
            try
            {
                return await db.Educations.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Education> GetEducations()
        {
            try
            {
                return db.Educations.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Education> GetEducations(Expression<Func<Education, bool>> expression)
        {
            try
            {
                return db.Educations.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Education GetEducation(Expression<Func<Education,bool>> expression)
        {
            try
            {
                return db.Educations.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Education>> GetEducationsAsync()
        {
            try
            {
                return await db.Educations.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Education>> GetEducationsAsync(Expression<Func<Education, bool>> expression)
        {
            try
            {
                return await db.Educations.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Education>> GetEducationsWithStaffAsync(Expression<Func<Education, bool>> expression)
        {
            try
            {
                return await db.Educations.Include(e => e.Staff).Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Education>> GetEducationsWithStaffAsync()
        {
            try
            {
                return await db.Educations.Include(e => e.Staff).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateEducation(Education education)
        {
            try
            {
                db.Entry(education).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}