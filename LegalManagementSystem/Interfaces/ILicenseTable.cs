namespace LegalManagementSystem.Interfaces
{
    public interface ILicenseTable
    {
        bool IsExpired(string clientName);
        bool IsLicensed(string clientName);
        string LicenseType(string clientName);
        int TrialPeriod(string clientName);
    }
}
