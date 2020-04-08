using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface ICompany
    {
        void AddCompany(Company company);
        void DeleteCompany(Company company);
        Company GetCompany(int id);
        IEnumerable<Company> GetCompany();
        Company GetCompany(Expression<Func<bool>> expression);
        void UpdateCompany(Company company);
    }
}
