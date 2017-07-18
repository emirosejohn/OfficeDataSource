using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using Email;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class EmailClientFake : IEmailClient
    {
        private readonly string _to;
        private readonly string _from;

        public EmailClientFake(IEmailSettings emailSettings)
        {
            _to = emailSettings.EmailTo;
            _from = emailSettings.EmailFrom;
        }

        public MailMessage SendEmailMessage(string body, string subject)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(_from);
            message.To.Add(_to);
            message.Body = body;
            message.Subject = subject;

            return message;
        }

        public MailMessage SendEmailMessage(List<string> to, string from, string body, string subject)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(from);
            foreach (var mailto in to)
            {
                message.To.Add(mailto);
            }
            message.Body = body;
            message.Subject = subject;

            return message;
        }
    }
}