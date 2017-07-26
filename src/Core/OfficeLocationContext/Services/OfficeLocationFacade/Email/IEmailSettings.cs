namespace OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade.Email
{
    public interface IEmailSettings
    {
        string EmailSubject { get;  }
        string EmailServerName { get;  }
        string EmailTo { get;  }
        string EmailFrom { get; }
    }
}
