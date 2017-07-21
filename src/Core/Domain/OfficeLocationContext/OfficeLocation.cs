using System;
using System.ComponentModel;

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

        public string HasChanged { get; set; }


        public static bool operator ==(OfficeLocation x, OfficeLocation y)
        {
            if ((object)x==null && (object)y == null) //checks for nulls
            {
                return true
            }
            else if ((object) x == null || (object) y == null)
            {
                return false;
            }


                return (
                x.OfficeId == y.OfficeId &&
                 x.Operating == y.Operating &&
                 x.Address == y.Address &&
                 x.Country == y.Country &&
                 x.Fax == y.Fax &&
                 x.Name == y.Name &&
                 x.Switchboard == y.Switchboard);
        }

        public static bool operator !=(OfficeLocation x, OfficeLocation y)
        {
            return !(x == y);
        }
    }
}
