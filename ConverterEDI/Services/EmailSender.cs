using ConverterEDI.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace ConverterEDI.Services
{
    public class EmailSender : Controller, IEmailSender
    {
        private readonly string username;
        private readonly string passsword;
        private readonly string smtpAddress;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmailSender(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            username = ReadConfigFile().UserName;
            passsword = ReadConfigFile().Password;
            smtpAddress = ReadConfigFile().SmtpAddress;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("EDI Portal", email));
            message.To.Add(new MailboxAddress(email));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = htmlMessage
            };
            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(smtpAddress, 465, true);

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(username, passsword);

                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch //(Exception ex)
            {
                //return Task.FromException(ex);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }

        public EmailConfig ReadConfigFile()
        {
            var configFile = Path.Combine(_hostingEnvironment.ContentRootPath, "config.json");
            EmailConfig emailConfig = new EmailConfig();
            try
            {
                using (StreamReader sr = new StreamReader(configFile))
                {
                    emailConfig = JsonConvert.DeserializeObject<EmailConfig>(sr.ReadToEnd());
                }
            } catch
            {
                //ignore
            }
            return emailConfig;
        }
    }
}
