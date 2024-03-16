using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;

namespace Rent2Own_API.Helper
{
    public class EmailSender : IEmailSender
    {

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse("hello@gmail.com"));
                emailToSend.To.Add(MailboxAddress.Parse(email));
                emailToSend.Subject = subject;
                emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

                //send email
                using var emailClient = new SmtpClient();
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("salmansidd372@gmail.com", "dmdy hbtu zfij bffr");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
