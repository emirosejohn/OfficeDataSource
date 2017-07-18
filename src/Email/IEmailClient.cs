using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Email
{
    public interface IEmailClient
    {
        MailMessage SendEmailMessage(string body, string subject);

        MailMessage SendEmailMessage(List<string> to, string from, string body, string subject);
    }
}
