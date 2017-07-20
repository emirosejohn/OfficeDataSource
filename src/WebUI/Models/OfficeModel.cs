using System.Collections.Generic;
using System.Web.Mvc;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class OfficeModel
    {
        public OfficeLocation[] Offices { get; set; }
        public OfficeLocation NewOffice { get; set; }
        public bool? NotificationFlag { get; set; }

        public Country[] Countries { get; set; }
        public OperatingOption[] OperatingOptions { get; set; }

        public IUserWrapper User { get; set; }
    }
}