using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using LegalManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LegalManagementSystem.Repositories
{
    public class ManagerRepository : ILineManager
    {
        private readonly MyCaseNewEntities db = new MyCaseNewEntities();
        public void AddManager(LineManager lineManager)
        {
            throw new NotImplementedException();
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

        public void DeleteManager(int id)
        {
            throw new NotImplementedException();
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

        public IEnumerable<ManagerForDropDown> GetAllManagersForDropDown()
        {
            try
            {
                var managers = new List<ManagerForDropDown>();
                var lineManagers = db.sp_GetLineManagers().ToList();

                foreach (var manager in lineManagers)
                {
                    managers.Add(new ManagerForDropDown
                    {
                        ManagerId = manager.LineManagerId,
                        ManagerName = manager.ManagerName
                    });
                }
                return managers;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<IEnumerable<LineManager>> GetManagersAsync(Expression<Func<LineManager, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public LineManager GetLineManager(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateClient(int id)
        {
            throw new NotImplementedException();
        }
    }
}