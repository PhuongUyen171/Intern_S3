using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace UI.Models
{
    public class MailHelper
    {
        public static void SendMailAuthentication(string toEmail, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmail));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = "<p>Mã xác thực của bạn: <b style=\"color: red\">" + content + "</b>.</p>";

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }

        public static void SendMailCanhBao(string toEmail, string subject, string khach, string ngay)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmail));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = "<h3>Ngày " + ngay + " bạn đã bị đánh giá sao thấp lần thứ 3 từ khách: " + khach + " mà chưa cải thiện sao, cẩn thận vì bạn có thể bị khóa tài khoản</h3>";

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
        public static void SendMailCanhBao2(string toEmail, string subject, string mer, string ngay)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmail));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = "<h3>Ngày " + ngay + " bạn đã bị đánh giá sao thấp lần thứ 3 từ merchant vì hành vi xấu: " + mer + " mà chưa cải thiện sao, cẩn thận vì bạn có thể bị khóa tài khoản</h3>";

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }

        public static void SendMailOfMer(string displayName, string toEmail, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = displayName;
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmail));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = "<h2><b>" + content + "<b/></h2>";

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }

        public static void SendMailOrderReceived(string toEmail, string subject, string maDon, string tenKhachHang, string trangthai, string Ngay)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmail));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = "<h1><b>Đơn hàng mã #" + maDon + " " + trangthai + " " + Ngay + "<b/></h1></br><h3> Cảm ơn khách hàng " + tenKhachHang + " đã ủng hộ KnowledgeStore</h5>";

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }

    }
}