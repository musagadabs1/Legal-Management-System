using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Repositories
{
    public class DependantRepository : IDependant
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddDependant(Dependant dependant)
        {
            try
            {
                db.Dependants.Add(dependant);
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
        public void DeleteDependant(Dependant dependant)
        {
            try
            {
                db.Dependants.Remove(dependant);
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
        public Dependant GetDependant(int? id)
        {
            try
            {
                return db.Dependants.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Dependant> GetDependantAsync(int? id)
        {
            try
            {
                return await db.Dependants.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Dependant> GetDependants()
        {
            try
            {
                return db.Dependants.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<Dependant> GetDependants(Expression<Func<Dependant, bool>> expression)
        {
            try
            {
                return db.Dependants.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public Dependant GetDependant(Expression<Func<Dependant, bool>> expression)
        {
            try
            {
                return db.Dependants.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Dependant>> GetDependantsAsync()
        {
            try
            {
                return await db.Dependants.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Dependant>> GetDependantsAsync(Expression<Func<Dependant, bool>> expression)
        {
            try
            {
                return await db.Dependants.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Dependant>> GetDependantsWithStaffAsync(Expression<Func<Dependant, bool>> expression)
        {
            try
            {
                return await db.Dependants.Include(e => e.Staff).Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Dependant>> GetDependantsWithStaffAsync()
        {
            try
            {
                return await db.Dependants.Include(e => e.Staff).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateDependant(Dependant dependant)
        {
            try
            {
                db.Entry(dependant).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}