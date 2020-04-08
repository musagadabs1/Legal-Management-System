using LMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Data.Interfaces
{
    public interface ICertification:IGenericInterface<Certification>
    {
        //void AddCertification(Certification certification);
        //void DeleteCertification(int id);
        void Dispose();
        Certification GetCertification(int id);
        Task<Certification> GetCertificationAsync(int id);
        IEnumerable<Certification> GetCertifications();
        IEnumerable<Certification> GetCertificationsWithStaff();
        Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync();
        Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync(Expression<Func<Certification,bool>> expression);
        IEnumerable<Certification> GetCertifications(Expression<Func<Certification,bool>> expression);
        Task<IEnumerable<Certification>> GetCertificationsAsync();
        Task<IEnumerable<Certification>> GetCertificationsAsync(Expression<Func<Certification, bool>> expression);
        Certification GetCertification(Expression<Func<Certification, bool>> expression);
        void UpdateCertification(int id);
    }
}
