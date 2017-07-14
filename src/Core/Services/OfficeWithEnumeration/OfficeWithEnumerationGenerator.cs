using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;

namespace OfficeLocationMicroservice.Core.Services.OfficeWithEnumeration
{
    public class OfficeWithEnumerationGenerator
    {
        private readonly OfficeLocationRepository _officeLocationRepository;
        private readonly CountryRepository _countryRepository;

        public OfficeWithEnumerationGenerator()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
            _countryRepository = MasterFactory.GetCountryRepository();
        }

        public OfficeWithEnumerationGenerator(
            OfficeLocationRepository officeLocationRepository,
            CountryRepository countryRepository)
        {
            _officeLocationRepository = officeLocationRepository;
            _countryRepository = countryRepository;
        }

        public OfficeWithEnumeration[] GetAll()
        {
            var countries = _countryRepository.GetAllCountries();
            var offices = _officeLocationRepository.GetAll();

            var officeWithEnumerations = new List<OfficeWithEnumeration>();

            foreach (var office in offices)
            {
                var officeWithEnumeration = new OfficeWithEnumeration(office, countries, null);
                officeWithEnumerations.Add(officeWithEnumeration);
            }

            return officeWithEnumerations.ToArray();
        }

        public OfficeWithEnumeration NewOffice(OfficeLocation office)
        {
            var countries = _countryRepository.GetAllCountries();

            return new OfficeWithEnumeration(office, countries, null);
        }
    }
}
