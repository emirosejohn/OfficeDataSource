using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.OfficeLocationContext;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class OfficeLocationModel
    {
        public OfficeLocation[] Offices { get; set; }

        public Country[] Countries { get; set; }
    }
}