using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LegalManagementSystem.Repositories
{
    public class LicenseTableRepository:ILicenseTable
    {
        private MyCaseNewEntities _context = new MyCaseNewEntities();
        public bool IsExpired(string clientName)
        {
            try
            {
                var isExpired = _context.LicenseTables.Where(x => x.IsExpired == true && x.ClientName.Equals(clientName)).FirstOrDefault();

                if (isExpired != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool IsLicensed(string clientName)
        {
            try
            {
                var getLicenseStatus = _context.LicenseTables.Where(x => x.IsLicensed == true && x.ClientName.Equals(clientName)).FirstOrDefault();

                if (getLicenseStatus != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string LicenseType(string clientName)
        {
            try
            {
                var getLicenseType = _context.LicenseTables.Where(x => x.IsLicensed == false && x.ClientName.Equals(clientName)).FirstOrDefault();

                if (getLicenseType != null)
                {
                    return getLicenseType.SoftwareVersion;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int TrialPeriod(string clientName)
        {
            try
            {
                //DateTime startTrial;
                //DateTime endTrial;
                int period = 0;
                var getLicenseType = _context.LicenseTables.Where(x => x.IsLicensed == false && x.ClientName.Equals(clientName)).FirstOrDefault();

                if (getLicenseType != null)
                {
                    period = getLicenseType.ValidityTo.Day - getLicenseType.ValidityFrom.Day;
                    return period;
                }
                return period;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AddLicense(LicenseTable license)
        {
            try
            {
                _context.LicenseTables.Add(license);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int Complete
        {
            get
            {
                try
                {
                    return _context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
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
                _context.Dispose();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<LicenseTable> GetLicenseAsync(int? id)
        {
            try
            {
                return await _context.LicenseTables.FindAsync(id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<LicenseTable>> GetLicensesAsync()
        {
            try
            {
                return await _context.LicenseTables.ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void UpdateLicense(LicenseTable license)
        {
            try
            {
                _context.Entry(license).State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void DeleteLicense(LicenseTable license)
        {
            try
            {
                _context.LicenseTables.Remove(license);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
    }
}