using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;

namespace LegalManagementSystem.Repositories
{
    public class AdvocateGroupRepository:IAdvocateGroup
    {
        private MyCaseNewEntities db = new MyCaseNewEntities();
        public IEnumerable<AdvocateGroup> GetAdvocateGroups()
        {
            try
            {
                return db.AdvocateGroups.ToList();
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
        public async Task<AdvocateGroup> GetAdvocateGroupAsync(int? id)
        {
            try
            {
                return await db.AdvocateGroups.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<AdvocateGroupForDropDown> GetAllAdvocateGroupsForDropDown()
        {
            try
            {
                var advocateGroups = new List<AdvocateGroupForDropDown>();
                var groups = db.GetAllAdvocateGroups().ToList();

                foreach (var group in groups)
                {
                    advocateGroups.Add(new AdvocateGroupForDropDown
                    {
                        Id = group.Id,
                        GroupName = group.GroupName
                    });
                }
                return advocateGroups;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public AdvocateGroup GetAdvocateGroup(int? id)
        {
            try
            {
                return db.AdvocateGroups.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public AdvocateGroup GetAdvocateGroup(Func<AdvocateGroup, bool> expression)
        {
            try
            {
                return db.AdvocateGroups.FirstOrDefault(expression);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<AdvocateGroup> GetAdvocateGroups(Expression<Func<AdvocateGroup, bool>> expression)
        {
            try
            {
                return db.AdvocateGroups.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<AdvocateGroup>> GetAdvocateGroupsAsync()
        {
            try
            {
                return await db.AdvocateGroups.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<Client>> GetClientsAsync(Expression<Func<Client, bool>> expression)
        {
            try
            {
                return await db.Clients.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AddAdvocateGroup(AdvocateGroup advocate)
        {
            try
            {
                db.AdvocateGroups.Add(advocate);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DeleteAdvocateGroup(AdvocateGroup advocate)
        {
            try
            {
                db.AdvocateGroups.Remove(advocate);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateAdvocateGroup(AdvocateGroup advocate)
        {
            try
            {
                db.Entry(advocate).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}