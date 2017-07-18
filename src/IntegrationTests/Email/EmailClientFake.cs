using System.Collections.Generic;
using System.Net.Mail;
using OfficeLocationMicroservice.Core.Services.Email;

namespace OfficeLocationMicroservice.IntegrationTests.Email
{
    public class EmailClientFake : IEmailClient
    {
        private readonly string _to;
        private readonly string _from;
        private readonly List<MailMessage> _sentMessages;

        public EmailClientFake(IEmailSettings emailSettings)
        {
            _to = emailSettings.EmailTo;
            _from = emailSettings.EmailFrom;
            _sentMessages = new List<MailMessage>();
        }

        public MailMessage SendEmailMessage(string body, string subject)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(_from);
            message.To.Add(_to);
            message.Body = body;
            message.Subject = subject;

            _sentMessages.Add(message);

            return message;
        }

        public List<MailMessage> GetSentMessage()
        {
            return _sentMessages;
        }
    }
}