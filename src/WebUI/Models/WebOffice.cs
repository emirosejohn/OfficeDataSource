using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class WebOffice : Core.OfficeLocationContext.Domain.Office
    {
        public WebOffice() : base()
        {
        }

        public WebOffice(OfficeDto officeDto, Country country) :
            base(officeDto, country)
        {
            HasChanged = "False";
        }

        public WebOffice(Core.OfficeLocationContext.Domain.Office office) 
        {
            this.HasChanged = "False";
            this.OfficeId = office.OfficeId;
            this.Address = office.Address;
            this.Country = office.Country;
            this.Fax = office.Fax;
            this.Name = office.Name;
            this.Operating = office.Operating;
            this.Switchboard = office.Switchboard;
        }

        public string HasChanged { get; set; }
    }
}