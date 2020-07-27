using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
//using System.Net;
//using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net;
using MailKit.Net.Smtp;
using MailKit;
using MailKit.Security;
using MimeKit;

namespace Microsoft.eShopWeb.Infrastructure.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public EmailSettings Options { get; }
        public EmailSender(IOptions<EmailSettings> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            await Execute(Options.MailServer, subject, message,email,Options.Sender);
           
        }

        public async Task Execute(string smtpServer, string subject, string body, string toEmail,string fromEmail)
        {

            var message = new MimeMessage();
            //message.From.Add(new MailboxAddress("Joey", "joey@friends.com"));
            message.To.Add(new MailboxAddress(toEmail));
            //message.From.Add(new MailboxAddress(fromEmail));
            message.Subject = subject;
            message.Sender = (new MailboxAddress(fromEmail)); 

            message.Body = new TextPart("plain", body);

            var client = new SmtpClient(new ProtocolLogger("smtp.log"));
            client.Connect(smtpServer, Options.MailPort, SecureSocketOptions.SslOnConnect);
            client.Authenticate(fromEmail, Options.Password);  
            await client.SendAsync(message);
            client.Disconnect(true);

            //return Task;
            //{


            //    UseDefaultCredentials = false,
            //    EnableSsl = true,
            //    Credentials = new NetworkCredential(fromEmail, Options.Password),

            //};
            //var mailMessage = new MailMessage
            // {
            //     From = new MailAddress(fromEmail)
            //};
            //mailMessage.To.Add(toEmail);
            ///mailMessage.Subject = subject;
            //mailMessage.Body = body;
            //mailMessage.IsBodyHtml = true;
            //return client.SendMailAsync(mailMessage);
        }
    }
}
