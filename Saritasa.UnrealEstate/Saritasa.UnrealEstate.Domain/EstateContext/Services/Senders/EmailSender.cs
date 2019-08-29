using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Saritasa.UnrealEstate.Domain.EstateContext.Services.Senders
{
    /// <summary>
    /// Static class that stores logic for sending emails.
    /// </summary>
    public static class EmailSender
    {
       /// <summary>
       /// Static method to send email async.
       /// </summary>
       /// <param name="email">Email.</param>
       /// <param name="subject">Email subject.</param>
       /// <param name="message">Email message.</param>
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var from = new MailAddress("string@string.com", "<From Name>");
            var to = new MailAddress(email, "<To Name>");

            string password = "sar1tasa";

            using (var smtp = new SmtpClient
            {
                Host = "smtp.saritasa-hosting.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, password)
            })

            using (var mailMessage = new MailMessage(from, to)
            {
                Subject = subject,
                Body = message
            })
            {
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}
