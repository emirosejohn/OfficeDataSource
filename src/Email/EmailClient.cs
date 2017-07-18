using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public class EmailClient
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

        public void SendEmailMessage(string body, string subject)
        {
            var client = new SmtpClient(_serverName);

            MailMessage message = new MailMessage();

            message.From = new MailAddress(_from);

            message.To.Add(_to);

            message.Body = body;

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

            message.Dispose();
        }

        public void SendEmailMessage(List<string> to, string from, string body, string subject)
        {
            var client = new SmtpClient(_serverName);

            MailMessage message = new MailMessage();

            message.From = new MailAddress(from);

            foreach (var mailto in to)
            {
                message.To.Add(mailto);

            }

            message.Body = body;

            message.Subject = subject;

            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }

            message.Dispose();
        }





    }
}
