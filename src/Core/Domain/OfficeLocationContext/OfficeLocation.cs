using System.Collections.Generic;
using System.Web.Mvc;

namespace OfficeLocationMicroservice.Core.Domain.OfficeLocationContext
{
    public class OfficeLocation
    {
        public int OfficeId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string Switchboard { get; set; }

        public string Fax { get; set; }

        public string TimeZone { get; set; }

        public string Operating { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> Timezones { get; set; }
        public IEnumerable<SelectListItem> OperatingOptions { get; set; }

    }
}
