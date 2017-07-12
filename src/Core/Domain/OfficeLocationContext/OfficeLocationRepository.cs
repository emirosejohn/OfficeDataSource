﻿using System;
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

        public OfficeLocation GetById(int id)
        {
            //var tz = TimeZoneInfo.GetSystemTimeZones();
            OfficeDto officeDto = _officeDataTableGateway.GetById(id);

            var office = new OfficeLocation
            {
                OfficeId = officeDto.OfficeId,
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
