using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WealthGrowthCreditVerification.Common
{
    public class EmailManager
    {
        public string SendMail(string toEmail, string subject, string emailBody, Attachment attachment)
        {
            try
            {
                NameValueCollection appsettings = ConfigurationManager.AppSettings;
                string fromEmail = appsettings.Get("DefaultFromEmail");
                string smtp = appsettings.Get("SMTP");
                string port = appsettings.Get("SMTPPort");
                string username = appsettings.Get("SMTPUsername");
                string password = appsettings.Get("SMTPPassword");
                bool enableSsl = Convert.ToBoolean(appsettings.Get("EnableSsl"));

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(fromEmail);
                mail.To.Add(new MailAddress(toEmail));
                mail.Subject = subject;
                mail.Body = emailBody;
                mail.Attachments.Add(attachment);

                using (SmtpClient smtpServer = new SmtpClient(smtp))
                {
                    smtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                    smtpServer.EnableSsl = enableSsl;
                    smtpServer.Port = Convert.ToInt32(port);
                    smtpServer.Timeout = 20000;
                    smtpServer.Send(mail);
                    smtpServer.SendCompleted += SmtpServer_SendCompleted;
                }

            }
            catch (Exception ex)
            {
                //System.IO.File.AppendAllText(
                //    $@"{CommonMethods.GetAppDataPath()}\CreditVerification_Log_{DateTime.Today.ToString("yyyy-mm-dd")}.txt",
                //    $"{ex.Message}\r\n\r\n{ex.InnerException}\r\n\r\n{ex.StackTrace}\r\n\r\n\r\n\r\n");

                return ex.ToString();
            }

            return null;
        }

        private void SmtpServer_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            string msg = $"UserState: {e.UserState}\r\nCancelled: {e.Cancelled}\r\nError: {e.Error}";
            System.IO.File.AppendAllText(@"c:\CreditVerification_Log.txt", $"Exception {DateTime.Now}\r\n\r\n");
        }
    }
}
