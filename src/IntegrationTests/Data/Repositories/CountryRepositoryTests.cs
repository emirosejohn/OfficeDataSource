using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.IntegrationTests;
using Xunit;

namespace OfficeLocationMicroservice.IntegrationTests.Data.Repositories
{
    public class CountryRepositoryTests : RepositoryTestsBase
    {
        private readonly CountryRepository _countryRepository;

        public CountryRepositoryTests()
        {
            _countryRepository = MasterFactory.GetCountryRepository();
        }

        [Fact]
        public void ShouldReturnListOfCountries()
        {
            var countries =  _countryRepository.getAllCountries();

            countries.Should().NotBeNull();

            countries.Length.Should().BeGreaterThan(0);
        }

        [Fact]
        public void CountriesShouldBeUnique()
        {
            var countries = _countryRepository.getAllCountries();

            countries.Should().NotBeNull();

            countries.Length.Should().BeGreaterThan(0);

            var result = UniqueCountriesCheck(countries);

            result.Should().BeTrue();
        }

        private static bool UniqueCountriesCheck(Country[] countries)
        {
            var uniquecountries = new List<Country>();

            foreach (var country in countries)
            {
                if (uniquecountries.Contains(country))
                {
                    return false;
                }
                uniquecountries.Add(country);
            }
            return true;
        }

        [Fact(Skip="Number of countries may change")]
        public void CountriesShouldBe137()
        {
            var countries = _countryRepository.getAllCountries();

            countries.Should().NotBeNull();

            countries.Length.Should().Be(137);
        }
    }
}
