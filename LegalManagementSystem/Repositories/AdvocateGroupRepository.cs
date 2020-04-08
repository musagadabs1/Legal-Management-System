using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}