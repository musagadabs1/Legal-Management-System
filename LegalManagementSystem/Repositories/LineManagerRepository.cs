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
    public class LineManagerRepository : ILineManager
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddManager(LineManager lineManager)
        {
            try
            {
                db.LineManagers.Add(lineManager);
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

        public void DeleteManager(LineManager lineManager)
        {
            try
            {
                //var client = GetClient(id);
                db.LineManagers.Remove(lineManager);
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
        public async Task<LineManager> GetManagerAsync(int? id)
        {
            try
            {
                return await db.LineManagers.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<ManagerForDropDown> GetAllManagersForDropDown()
        {
            try
            {
                var managersList = new List<ManagerForDropDown>();
                var managers = db.sp_GetLineManagers().ToList();

                foreach (var manager in managers)
                {
                    managersList.Add(new ManagerForDropDown
                    {
                        ManagerId= manager.LineManagerId,
                        ManagerName=manager.ManagerName
                    });
                }
                return managersList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<LineManager>> GetManagersAsync()
        {
            try
            {
                return await db.LineManagers.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public LineManager GetLineManager(int? id)
        {
            try
            {
                return db.LineManagers.Find(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<LineManager> GetManagers()
        {
            try
            {
                return db.LineManagers.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<LineManager>> GetManagersAsync(Expression<Func<LineManager, bool>> expression)
        {
            try
            {
                return await db.LineManagers.Where(expression).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public IEnumerable<LineManager> GetManagers(Expression<Func<LineManager, bool>> expression)
        {
            try
            {
                return db.LineManagers.Where(expression).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateClient(LineManager lineManager)
        {
            try
            {
                db.Entry(lineManager).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}