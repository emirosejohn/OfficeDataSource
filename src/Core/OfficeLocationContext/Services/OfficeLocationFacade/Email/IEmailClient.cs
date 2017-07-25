using System.Net.Mail;

namespace OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade.Email
{
    public interface IEmailClient
    {
        MailMessage SendEmailMessage(string body, string subject);

    }
}
