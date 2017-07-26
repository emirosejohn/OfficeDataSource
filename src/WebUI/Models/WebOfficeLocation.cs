using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class WebOfficeLocation :OfficeLocation
    {
        public WebOfficeLocation() : base()
        {
        }

        public WebOfficeLocation(OfficeDto officeDto, Country country) :
            base(officeDto, country)
        {
            HasChanged = "False";
        }

        public WebOfficeLocation(OfficeLocation officeLocation) 
        {
            this.HasChanged = "False";
            this.OfficeId = officeLocation.OfficeId;
            this.Address = officeLocation.Address;
            this.Country = officeLocation.Country;
            this.Fax = officeLocation.Fax;
            this.Name = officeLocation.Name;
            this.Operating = officeLocation.Operating;
            this.Switchboard = officeLocation.Switchboard;
        }

        public string HasChanged { get; set; }
    }
}