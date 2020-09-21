using LegalManagementSystem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LegalManagementSystem.Interfaces
{
    public interface ILicenseTable
    {
        void AddLicense(LicenseTable license);
        int Complete { get; }

        bool IsExpired(string clientName);
        bool IsLicensed(string clientName);
        string LicenseType(string clientName);
        int TrialPeriod(string clientName);
        Task<int> CompleteAsync();
        void Dispose();
        Task<LicenseTable> GetLicenseAsync(int? id);
        Task<IEnumerable<LicenseTable>> GetLicensesAsync();
        void UpdateLicense(LicenseTable license);
        void DeleteLicense(LicenseTable license);
    }
}
