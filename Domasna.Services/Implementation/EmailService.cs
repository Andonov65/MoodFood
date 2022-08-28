using Domasna.Services.Interface;
using Domasna.Domain;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;
using Domasna.Domain.DomainModels;
using MimeKit.Text;

namespace Domasna.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmailAsync(List<EmailMessage> allEmails)
        {
            List<MimeMessage> messages = new List<MimeMessage>();

            foreach (var item in allEmails)
            {
                var emailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(_settings.SendersName, _settings.SmtpUserName),
                    Subject = item.Subject
                };

                emailMessage.From.Add(new MailboxAddress(_settings.EmailDisplayName, _settings.SmtpUserName));
                emailMessage.Body = new TextPart(TextFormat.Plain) { Text = item.Content };

                emailMessage.To.Add(new MailboxAddress(item.MailTo));

                messages.Add(emailMessage);
            }

            try
            {
                using(var smtp = new MailKit.Net.Smtp.SmtpClient()) //so pomos na ovoj klient se komunicira
                {
                    var socketOption = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;

                    await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpServerPort, socketOption);


                    if (!string.IsNullOrEmpty(_settings.SmtpUserName))
                    {
                        await smtp.AuthenticateAsync(_settings.SmtpUserName, _settings.SmtpPassword);
                    }

                    foreach (var item in messages)
                    {
                        await smtp.SendAsync(item);
                    }

                    await smtp.DisconnectAsync(true); //da se diskonektirame od samiot servis

                }
            }
            catch (SmtpException ex)
            {

                throw ex;
            }
        }

        
    }
}
