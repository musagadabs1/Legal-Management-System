using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace LegalManagementSystem.Models
{
    public static class LegalGuideUtility
    {
        private static readonly MyCaseNewEntities db = new MyCaseNewEntities();

        public const string ADMINISTRATOR = "Admin";
        public const string ATTORNEY = "Attorney";
        public const string LAWYER = "Lawyer";
        public const string User = "User";
        public const string Dismissed = "Dismissed";
        public const string JudgementDelivered = "Judgement Delivered";
        public const string StrikeOut = "Strike Out";
        public const string Adjourned = "Adjourned";
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
        public static void SendEmail(string toAddress, string subject,string body)
        {
            var networkCredentialEmail = ConfigurationManager.AppSettings["CredentialEmail"];
            var networkCredentialPassword = ConfigurationManager.AppSettings["CredentialPassword"];
            var fromEmail = ConfigurationManager.AppSettings["fromEmail"];
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(toAddress);
            mail.Subject = subject;
            mail.Body = body;

            //Attachment attachment;
            //attachment = new Attachment("c:/textfile.txt");
            //mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(networkCredentialEmail, networkCredentialPassword);
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

        }
        public static string RemoveHTMLTags(string value)
        {
            Regex regex = new Regex("\\<[^\\>]*\\>");
            value = regex.Replace(value, String.Empty);
            return value;
        }
        public class FormattedDbEntityValidationException : Exception
        {
            public FormattedDbEntityValidationException(DbEntityValidationException innerException) :
                base(null, innerException)
            {
            }

            public override string Message
            {
                get
                {
                    var innerException = InnerException as DbEntityValidationException;
                    if (innerException != null)
                    {
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine();
                        sb.AppendLine();
                        foreach (var eve in innerException.EntityValidationErrors)
                        {
                            sb.AppendLine(string.Format("- Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().FullName, eve.Entry.State));
                            foreach (var ve in eve.ValidationErrors)
                            {
                                sb.AppendLine(string.Format("-- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                                    ve.PropertyName,
                                    eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                                    ve.ErrorMessage));
                            }
                        }
                        sb.AppendLine();

                        return sb.ToString();
                    }

                    return base.Message;
                }
            }
        }
    }
    

}


