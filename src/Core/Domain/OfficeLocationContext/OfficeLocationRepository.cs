using System;
using System.Linq;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core.Domain.OfficeLocationContext
{
    public class OfficeLocationRepository 
    {
        private readonly IOfficeDataTableGateway _officeDataTableGateway;

        public OfficeLocationRepository(IOfficeDataTableGateway officeGateway)
        {
            _officeDataTableGateway = officeGateway;
        }

        public OfficeLocation GetByName(string name)
        {
            var officeDto = _officeDataTableGateway.GetByName(name);

            var office = officeDto.ExtractOfficeLocation();

            return office;
        }

        public OfficeLocation[] GetAll()
        {
            OfficeDto[] officeDtos = _officeDataTableGateway.GetAll();

            OfficeLocation[] officeLocations = new OfficeLocation[officeDtos.Length];

            for (int k = 0; k < officeDtos.Length; k++)
            {
                OfficeLocation office = GetByName(officeDtos[k].Name);
                officeLocations[k] = office;
            }

            return officeLocations;

        }

        public OfficeLocation Update(OfficeLocation editedOfficeLocation)
        {
            var offices = GetAll();

            var officeDto = editedOfficeLocation.ExtractDto();

            if (offices.All(x => x.OfficeId != editedOfficeLocation.OfficeId))
            {
                var id = _officeDataTableGateway.Insert(officeDto);

                editedOfficeLocation.OfficeId = id;
                 

            }
            else
            {
                _officeDataTableGateway.Update(officeDto);
            }
            return editedOfficeLocation;
        }

        public OfficeLocation GetById(int id)
        {
            OfficeDto officeDto = _officeDataTableGateway.GetById(id);

            var office = officeDto.ExtractOfficeLocation();
                
            return office;
        }
    }

    public static class OfficeLocationExtensions
    {
        public static OfficeDto ExtractDto(this OfficeLocation officeLocation)
        {
            if (officeLocation == null)
            {
                return null;
            }

            var officeDto = new OfficeDto()
            {
                OfficeId = officeLocation.OfficeId,
                Name = officeLocation.Name,
                Address = officeLocation.Address,
                Country = officeLocation.Country,
                Switchboard = officeLocation.Switchboard,
                Fax = officeLocation.Fax,
                TimeZone = officeLocation.TimeZone,  
            };

            switch (officeLocation.Operating)
            {
                case "Active":
                    officeDto.Operating = 1;
                    break;
                case "Closed":
                    officeDto.Operating = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return officeDto;
        }
    }

    public static class OfficeDtoExtensions
    {
        public static OfficeLocation ExtractOfficeLocation(this OfficeDto officeDto)
        {
            if (officeDto == null)
            {
                return null;
            }

            var officeLocation = new OfficeLocation()
            {
                OfficeId = officeDto.OfficeId,
                Name = officeDto.Name,
                Address = officeDto.Address,
                Country = officeDto.Country,
                Switchboard = officeDto.Switchboard,
                Fax = officeDto.Fax,
                TimeZone = officeDto.TimeZone,
            };

            switch (officeDto.Operating)
            {
                case 1:
                    officeLocation.Operating = "Active";
                    break;
                case 0:
                    officeLocation.Operating = "Closed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return officeLocation;
        }
    }
}
