using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var config = new ConfigurationBuilder()
                .SetBasePath(_hostingEnvironment.ContentRootPath)
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .Build();

            username = config.GetValue<string>("username");
            passsword = config.GetValue<string>("password");
            smtpAddress = config.GetValue<string>("smtpAddress");
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
            catch (Exception ex)
            {
                return Task.FromException(ex);
            }

            return Task.CompletedTask;
        }
    }
}
