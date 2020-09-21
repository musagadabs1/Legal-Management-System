using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Data.Interfaces
{
    public interface ICertification//:IGenericInterface<Certification>
    {
        IEnumerable<Certification> GetCertificationsWithStaff(string staffId);
        Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync(string staffId);
        Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync(Expression<Func<Certification,bool>> expression);
    }
}
