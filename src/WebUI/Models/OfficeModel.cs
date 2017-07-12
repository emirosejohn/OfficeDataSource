using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> Timezones { get; set; }
        public IEnumerable<SelectListItem> OperatingOptions { get; set; }

    }
}