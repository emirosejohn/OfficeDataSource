using System;
using System.Linq;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core.Domain.OfficeLocationContext
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

            var office = officeDto.ExtractOfficeLocation();

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

        public void Update(OfficeLocation editedOfficeLocation)
        {
            var offices = GetAll();

            var officeDto = editedOfficeLocation.ExtractDto();

            if (offices.All(x => x.OfficeId != editedOfficeLocation.OfficeId))
            {
                _officeDataTableGateway.Insert(officeDto);

            }
            else
            {
                _officeDataTableGateway.Update(officeDto);
            }
        }
    }

    public static class OfficeLocationExtensions
    {
        public static OfficeDto ExtractDto(this OfficeLocation officeLocation)
        {
            var officeDto = new OfficeDto()
            {
                OfficeId = officeLocation.OfficeId,
                Name = officeLocation.Name,
                Address = officeLocation.Address,
                Country = officeLocation.Country,
                Switchboard = officeLocation.Switchboard,
                Fax = officeLocation.Fax,
                TimeZone = officeLocation.TimeZone,
                Operating = Convert.ToInt16(officeLocation.Operating)
            };

            return officeDto;
        }
    }

    public static class OfficeDtoExtensions
    {
        public static OfficeLocation ExtractOfficeLocation(this OfficeDto officeDto)
        {
            var officeLocation = new OfficeLocation()
            {
                OfficeId = officeDto.OfficeId,
                Name = officeDto.Name,
                Address = officeDto.Address,
                Country = officeDto.Country,
                Switchboard = officeDto.Switchboard,
                Fax = officeDto.Fax,
                TimeZone = officeDto.TimeZone,
                Operating = Convert.ToBoolean(officeDto.Operating)
            };

            return officeLocation;
        }
    }
}
