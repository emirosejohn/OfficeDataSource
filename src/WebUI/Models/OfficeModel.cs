using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class OfficeModel
    {
        public OfficeLocation[] Offices { get; set; }
        public OfficeLocation OfficeEdit { get; set; }
        public bool ShowOfficeEdit { get; set; }
        public Country[] Countries { get; set; }

    }

    public class OfficeEditModel
    {
        public int OfficeId { get; set; }
        public string NewOfficeName { get; set; }
        public string NewAddress { get; set; }

        public string NewCountry { get; set; }

        public string NewSwitchboard { get; set; }

        public string NewFax { get; set; }

        public string NewTimeZone { get; set; }
        public string NewOperating { get; set; }
    }
}