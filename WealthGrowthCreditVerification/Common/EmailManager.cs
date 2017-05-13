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
        public string SendMail(string toEmail, string subject, string emailBody)
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

                using (SmtpClient smtpServer = new SmtpClient(smtp))
                {
                    smtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                    smtpServer.EnableSsl = enableSsl;
                    smtpServer.Port = Convert.ToInt32(port);
                    smtpServer.Timeout = 10000;
                    smtpServer.Send(mail);
                }

            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

            return null;
        }
    }
}
