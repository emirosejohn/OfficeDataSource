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
            var listofCountries =  _countryRepository.getAllCountries();

            listofCountries.Should().NotBeNull();

            listofCountries.Count.Should().BeGreaterThan(0);
        }

    }
}
