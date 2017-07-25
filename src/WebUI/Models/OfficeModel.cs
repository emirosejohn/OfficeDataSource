using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class OfficeModel
    {
        public OfficeLocation[] Offices { get; set; }
        public OfficeLocation NewOffice { get; set; }
        public bool? NotificationFlag { get; set; }
        public bool RegularView { get; set; }

        public Country[] Countries { get; set; }
        public OperatingOption[] OperatingOptions { get; set; }

        public IUserWrapper User { get; set; }
    }
}