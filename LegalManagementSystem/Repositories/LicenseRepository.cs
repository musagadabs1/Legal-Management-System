using LegalManagementSystem.Interfaces;
using LegalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegalManagementSystem.Repositories
{
    public class LicenseRepository:ILicenseTable
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
    }
}