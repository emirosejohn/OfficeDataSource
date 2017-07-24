using System.Net.Mail;

namespace OfficeLocationMicroservice.Core.Services.Email
{
    public interface IEmailClient
    {
        MailMessage SendEmailMessage(string body, string subject);

    }
}
