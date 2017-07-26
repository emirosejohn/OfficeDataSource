using FluentAssertions;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using Xunit;

namespace OfficeLocationMicroservice.IntegrationTests.Data.Repositories
{
    public class OfficeLocationRepositoryTests : RepositoryTestsBase
    {
        private readonly OfficeLocationRepository _officeLocationRepository;
            
        public OfficeLocationRepositoryTests()
        {
            _officeLocationRepository = MasterFactory.GetOfficeLocationRepository();
        }

        [Fact]
        public void ShouldReturnOfficeLocations()
        {
            var officeLocations = _officeLocationRepository.GetAll();

            officeLocations.Should().NotBeNull();      
        }

    }
}
