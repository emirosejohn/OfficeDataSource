using OfficeLocationMicroservice.Core.Services.OfficeWithEnumeration;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class OfficeModel
    {
        public OfficeWithEnumeration[] Offices { get; set; }
        public OfficeWithEnumeration NewOffice { get; set; }
        public bool? NotificationFlag { get; set; }
    }
}