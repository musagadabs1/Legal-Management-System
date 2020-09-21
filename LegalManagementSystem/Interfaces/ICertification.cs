using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface ICertification
    {
        void AddCertification(Certification certification);
        void DeleteCertification(Certification  certification);
        void Dispose();
        int Complete();
        Task<int> CompleteAsync();
        Certification GetCertification(int? id);
        Task<Certification> GetCertificationAsync(int? id);
        IEnumerable<Certification> GetCertifications();
        IEnumerable<Certification> GetCertificationsWithStaff();
        Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync();
        Task<IEnumerable<Certification>> GetCertificationsWithStaffAsync(Expression<Func<Certification,bool>> expression);
        IEnumerable<Certification> GetCertifications(Expression<Func<Certification,bool>> expression);
        Task<IEnumerable<Certification>> GetCertificationsAsync();
        Task<IEnumerable<Certification>> GetCertificationsAsync(Expression<Func<Certification, bool>> expression);
        Certification GetCertification(Expression<Func<Certification, bool>> expression);
        void UpdateCertification(Certification certification);
    }
}
