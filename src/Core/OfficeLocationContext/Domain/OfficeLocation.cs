using System;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core.OfficeLocationContext.Domain
{
    public class OfficeLocation
    {
        public OfficeLocation()
        {
            Country= new Country();
        }

        public OfficeLocation(OfficeDto officeDto, Country country)
        {
            this.OfficeId = officeDto.OfficeId;
            this.Name = officeDto.Name;
            this.Address = officeDto.Address;
            this.Country = country;
            this.Switchboard = officeDto.Switchboard;
            this.Fax = officeDto.Fax;
            switch (officeDto.Operating)
            {
                case 1:
                    this.Operating = "Active";
                    break;
                case 0:
                    this.Operating = "Closed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int OfficeId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public Country Country { get; set; }

        public string Switchboard { get; set; }

        public string Fax { get; set; }

        public string Operating { get; set; }


        public static bool operator ==(OfficeLocation x, OfficeLocation y)
        {
            if ((object)x==null && (object)y == null) //checks for nulls
            {
                return true;
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
