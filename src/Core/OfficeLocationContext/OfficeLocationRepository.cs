using System;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core.OfficeLocationContext
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
                Id = officeDto.OfficeId,
                Name = officeDto.Name,
                Address = officeDto.Address,
                Country = officeDto.Country,
                Switchboard = officeDto.Switchboard,
                Fax = officeDto.Fax,
                TimeZone = officeDto.TimeZone,
                //Operating = officeDto.Operating ? "Active" : "Closed"
            };
            switch (officeDto.Operating)
            {
                case 1:
                    office.Operating = "Active";
                    break;
                case 0:
                    office.Operating = "Closed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return office;
        }

        public OfficeLocation GetById(int id)
        {
            //var tz = TimeZoneInfo.GetSystemTimeZones();
            OfficeDto officeDto = _officeDataTableGateway.GetById(id);
            
            var office = new OfficeLocation
            {
                Id = officeDto.OfficeId,
                Name = officeDto.Name,
                Address = officeDto.Address,
                Country = officeDto.Country,
                Switchboard = officeDto.Switchboard,
                Fax = officeDto.Fax,
                TimeZone = officeDto.TimeZone,
                //Operating = officeDto.Operating ? "Active" : "Closed"
            };
            switch (officeDto.Operating)
            {
                case 1:
                    office.Operating = "Active";
                    break;
                case 0:
                    office.Operating = "Closed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
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
