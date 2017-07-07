using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeLocationMicroservice.Core
{
    public class OfficeLocation
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Switchboard { get; set; }
        public string Fax { get; set; }
        public string TimeZone { get; set; }
        public bool Operating { get; set; }
    }
}
