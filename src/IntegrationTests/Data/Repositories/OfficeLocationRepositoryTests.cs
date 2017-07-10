using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;

namespace OfficeLocationMicroservice.IntegrationTests.Data.Repositories
{
    public class OfficeLocationRepositoryTests : RepositoryTestsBase
    {
        private readonly OfficeLocationRepository _officeLocationRepository;
            
        public OfficeLocationRepositoryTests()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
        }

        public void ShouldReturnOfficeLocations()
        {
            var officeLocations = _officeLocationRepository.GetAll();

            officeLocations.Should().NotBeNull();

            officeLocations.Length.Should().BeGreaterThan(0);

        }
    }
}
