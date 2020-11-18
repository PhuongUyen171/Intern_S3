using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace UI.Models
{
    public class EmailManager
    {
        public static void AppSettings(out string UserID, out string Password, out string SMTPPort, out string Host)
        {
            UserID = ConfigurationManager.AppSettings.Get("UserID");
            Password = ConfigurationManager.AppSettings.Get("Password");
            SMTPPort = ConfigurationManager.AppSettings.Get("SMTPPort");
            Host = ConfigurationManager.AppSettings.Get("Host");
        }
        //public static void SendEmail(string From, string Subject, string Body, string To, string UserID, string Password, string SMTPPort, string Host)
        //{
        //    System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //    mail.To.Add(To);
        //    mail.From = new MailAddress(From);
        //    mail.Subject = Subject;
        //    mail.Body = Body;
        //    SmtpClient smtp = new SmtpClient();
        //    smtp.Host = Host;
        //    smtp.Port = Convert.ToInt16(SMTPPort);
        //    smtp.Credentials = new NetworkCredential(UserID, Password);
        //    smtp.EnableSsl = true;
        //    smtp.Send(mail);
        //}
        public void SendPasswordResetEmail(string ToEmail, string UserName, string UniqueId)
        {
            // MailMessage class is present is System.Net.Mail namespace
            MailMessage mailMessage = new MailMessage("YourEmail@gmail.com", ToEmail);
            // StringBuilder class is present in System.Text namespace
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("Dear " + UserName + ",<br/><br/>");
            sbEmailBody.Append("Please click on the following link to reset your password");
            sbEmailBody.Append("<br/>"); sbEmailBody.Append("http://localhost/WebApplication1/Registration/ChangePassword.aspx?uid=" + UniqueId);
            sbEmailBody.Append("<br/><br/>");
            sbEmailBody.Append("<b>Pragim Technologies</b>");

            mailMessage.IsBodyHtml = true;

            mailMessage.Body = sbEmailBody.ToString();
            mailMessage.Subject = "Reset Your Password";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "nguyenthiphuonguyen.17.01.1999@gmail.com",
                Password = "ntpu170199"
            };

            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }

    }

}