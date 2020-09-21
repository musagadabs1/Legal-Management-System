using LMS.Data.Interfaces;
using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Data.Repositories
{
    public class CertificationRepository :GenericRepository<Certification>, ICertification
    {
        private DbContext _context;
        public CertificationRepository(DbContext context):base(context)
        {
            _context = context;
        }



        public IEnumerable<Certification> GetCertificationsWithStaff(string staffId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync(string staffId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync(Expression<Func<Certification, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}