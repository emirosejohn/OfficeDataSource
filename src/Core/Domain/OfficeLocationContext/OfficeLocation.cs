using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        public string Operating { get; set; }
    }
}
