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
        private readonly int _port;
        private readonly string _host;

        public EmailClient(string serverName)
        {
            _serverName = serverName;
            _host = "smtp-mail.outlook.com";
            _port = 587;
        }

        public void SendEmailMessage(List<string> to, string from, string body, string subject)
        {
            var client = new SmtpClient(_serverName)
            {
                Host = _host,
                Port = _port
            };

            MailMessage message = new MailMessage();

            message.From = new MailAddress(from);

            foreach (var mailto in to)
            {
                message.To.Add(mailto);

            }

            message.Body = body;

            message.Subject = subject;

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
