using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core;

namespace OfficeLocationMicroservice.Database
{
    public class OfficeLocationRepository : IOfficeLocationRepository
    {
        private readonly IOfficeDataTableGateway _officeDataTableGateway;

        public OfficeLocationRepository(IOfficeDataTableGateway officeGateway)
        {
            _officeDataTableGateway = officeGateway;
        }

        public OfficeLocation GetByName(string name)
        {
            //var tz = TimeZoneInfo.GetSystemTimeZones();
            
            var officeDto = _officeDataTableGateway.GetByName(name);
            var office = new OfficeLocation
            {
                Name = officeDto.Name,
                Address = officeDto.Address,
                Country = officeDto.Country,
                Switchboard = officeDto.Switchboard,
                Fax = officeDto.Fax,
                TimeZone = officeDto.TimeZone,
                Operating = officeDto.Operating
            };

            return office;
        }
        public OfficeLocation[] GetAll()
        {
            //want to use OfficeDataTableGateway.GetAll
            OfficeDto[] officeDtos = _officeDataTableGateway.GetAll();

            OfficeLocation[] officeLocations = new OfficeLocation[officeDtos.Length];

            for (int k = 0; k < officeDtos.Length; k++)
            {
                OfficeLocation office = GetByName(officeDtos[k].Name);
                officeLocations[k] = office;
            }

            return officeLocations;

        }
    }
}
