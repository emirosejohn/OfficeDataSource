using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using OfficeLocationMicroservice.Core.Services.Email;

namespace Email
{
    public class EmailClient : IEmailClient
    {
        private readonly string _serverName;
        private readonly string _to;
        private readonly string _from;

        public EmailClient(IEmailSettings emailSettings)
        {
            _serverName = emailSettings.EmailServerName;
            _to = emailSettings.EmailTo;
            _from = emailSettings.EmailFrom;
        }

        public MailMessage SendEmailMessage(string body, string subject)
        {
            var client = new SmtpClient(_serverName);

            MailMessage message = new MailMessage();

            message.From = new MailAddress(_from, "Office Data Source" );

            var toList = _to.Split(',').ToList();

            foreach (var to in toList)
            {
                message.To.Add(to);
            }
            message.Body = body;
            message.IsBodyHtml = true;
            message.Subject = subject;

            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return message;
        }

    }
}
