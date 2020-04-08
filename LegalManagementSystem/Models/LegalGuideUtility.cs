using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LegalManagementSystem.Models
{
    public static class LegalGuideUtility
    {
        private static MyCaseNewEntities db = new MyCaseNewEntities();

        public const string ADMINISTRATOR = "Admin";
        public const string ATTORNEY = "Attorney";
        public const string LAWYER = "Lawyer";
        public const string User = "User";
        public static string LicenseMessage { get; set; }
        public static string UserFullName {get;set;}
        public static string ErrorMessage { get; set; }
        public static string StaffId { get; set; }
        public static string MatterId { get; set; }
        public static string MatterNumber { get; set; }
        public static string Status { get; set; }

        public static List<string> Roles = new List<string> { "Administrator", "Attorney", "Lawyer", "User" };
        public static string Encrypt(string str)
        {
            string hash = string.Empty;
            try
            {
                using (SHA512 sha512Hash = SHA512.Create())
                {
                    //From String to byte array
                    byte[] sourceBytes = Encoding.UTF8.GetBytes(str);
                    byte[] hashBytes = sha512Hash.ComputeHash(sourceBytes);
                    hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                }
                return hash;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
    }

}


