using System.Collections.Generic;
using System.Linq;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core.OfficeLocationContext.Domain
{
    public class OfficeLocationRepository
    {
        private readonly IOfficeDataTableGateway _officeDataTableGateway;
        private readonly CountryRepository.CountryRepository _countryRepository;

        public OfficeLocationRepository(
            IOfficeDataTableGateway officeDataTableGateway,
            CountryRepository.CountryRepository countryRepository)
        {
            _officeDataTableGateway = officeDataTableGateway;
            _countryRepository = countryRepository;
        }
       
        public OfficeLocation GetById(int id)
        {
            OfficeDto officeDto = _officeDataTableGateway.GetById(id);
            var country = _countryRepository.GetCountryBySlug(officeDto.CountrySlug);

            return new OfficeLocation(officeDto, country);
        }

        public OfficeLocation[] GetAll()
        {
            OfficeDto[] officeDtos = _officeDataTableGateway.GetAll();

            Country[] countries = _countryRepository.GetAllCountries();

            var officeLocations = new List<OfficeLocation>();

            foreach (var officeDto in officeDtos)
            {
                var country = countries.Single(x => x.Slug == officeDto.CountrySlug);
                officeLocations.Add(new OfficeLocation(officeDto, country));
            }

            return officeLocations.ToArray();
        }

        public Country[] GetAllCountries()
        {
            return _countryRepository.GetAllCountries();
        }

        public void Update(OfficeLocation changedOfficeLocation)
        {
            var officeDto = changedOfficeLocation.ExtractDto();

            _officeDataTableGateway.Update(officeDto);

        }

        public int Insert(OfficeLocation changedOfficeLocation)
        {
            var officeDto = changedOfficeLocation.ExtractDto();
            var id =_officeDataTableGateway.Insert(officeDto);

            return id;
        }
    }
}
