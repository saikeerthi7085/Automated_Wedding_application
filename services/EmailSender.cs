    using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace Automated_Wedding_Application.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        //possible to be setup in config files
        private const string MAIL_HOST = NoReplyConstants.SMTPHost;
        private const string EMAIL_USERNAME = NoReplyConstants.Email;
        private const string EMAIL_PASSWORD = NoReplyConstants.Pass;
        /// <summary>
        /// This method is to be used for emails with only a message i.e. Link to confirm email
        /// </summary>
        /// <param name="email">The users email</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="message">The message body</param>
        /// <returns>The email is sent</returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = MAIL_HOST;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(EMAIL_USERNAME, EMAIL_PASSWORD);
            var mail = new MailMessage(EMAIL_USERNAME, email, subject, message);
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);

        }

        public Task SendEmailWithSenderAsync( string sender, string email, string subject, string message )
        {

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = MAIL_HOST;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(EMAIL_USERNAME, EMAIL_PASSWORD);
            var mail = new MailMessage(sender, email, subject, message);
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);

        }

        /// <summary>
        /// This method is to be used for emails with a message and a file i.e. Message = "Your report is ready" Attachment = "Report123.csv"
        /// </summary>
        /// <param name="email">The users email</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="message">The message body</param>
        /// <param name="attachmentPath">The exact path for the file you wish to send</param>
        /// <returns>The email is sent</returns>
        public Task SendEmailAsync(string email, string subject, string message, string attachmentPath)
        {

            Attachment attachment = new Attachment(attachmentPath);
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = MAIL_HOST;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(EMAIL_USERNAME, EMAIL_PASSWORD);
            var mail = new MailMessage(EMAIL_USERNAME, email, subject, message);
            mail.IsBodyHtml = true;
            if (attachment != null)
            {
                mail.Attachments.Add(attachment);
            }
            return client.SendMailAsync(mail);

        }

        /// <summary>
        /// This method is to be used for emails with a message and a files i.e. Message = "Your reports are ready" Attachment = "Report123.csv, Report456.csv"
        /// </summary>
        /// <param name="email">The users email</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="message">The message body</param>
        /// <param name="attachmentPath">The exact path for the file you wish to send</param>
        /// <returns>The email is sent</returns>
        public Task SendEmailAsync(string email, string subject, string message, IEnumerable<string> attachmentPaths)
        {

            List<Attachment> attachments = new List<Attachment>();
            foreach (string s in attachmentPaths)
            {
                attachments.Add(new Attachment(s));
            }

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = MAIL_HOST;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(EMAIL_USERNAME, EMAIL_PASSWORD);
            var mail = new MailMessage(EMAIL_USERNAME, email, subject, message);
            mail.IsBodyHtml = true;

            if (attachments != null)
            {
                foreach (Attachment item in attachments)
                {
                    mail.Attachments.Add(item);
                }
            }

            return client.SendMailAsync(mail);

        }
    }
}
