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
        public static string StaffId { get; set; }
        public static string MatterId { get; set; }
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
        public static int GetAdvocateGroupIdByUsername(string username)
        {
            int id = 0;
            try
            {
                var getId = db.LoginUsers.Where(x => x.Username.Equals(username)).FirstOrDefault();

                if (getId !=null)
                {
                    id =(int)getId.AdvocateGroupId;
                }
                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string GetStaffEmailByLoginName(string username)
        {
            try
            {
                string email = string.Empty;
                var loginUser = db.LoginUsers.Where(x => x.Username.Equals(username)).FirstOrDefault();
                if (loginUser != null)
                {
                    email = loginUser.EmailAddress;
                }
                return email;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static string GetStaffIdByEmail(string email)
        {
            try
            {
                string staffId = string.Empty;
                var staff = db.Staffs.Where(x => x.EmailAddress.Equals(email)).FirstOrDefault();
                if (staff !=null)
                {
                    staffId = staff.StaffId;
                    return staffId;
                }
                return staffId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}