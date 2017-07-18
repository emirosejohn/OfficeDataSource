using System.Linq;
using FluentAssertions;
using OfficeLocationMicroservice.Core;
using Xunit;
using OfficeLocationMicroservice.DependencyManagement;

namespace OfficeLocationMicroservice.IntegrationTests.Data.CountryWebApi
{
    public class CountryWebApiGatewayTests
    {
        [Fact]
        public void WebApiShouldReturnRegionScheme()
        {
            var dataConnectionStrings = new DataConnectionStringsForIntegrationTests();

            var systemLog = new SystemLogForIntegrationTests();

            DependencyManager.BootstrapForTests(systemLog, dataConnectionStrings, dataConnectionStrings, dataConnectionStrings);

            var countryWebApiGateway = MasterFactory.CountryWebApiGateway;

            var regionScheme = countryWebApiGateway.GetRegionScheme();

            regionScheme.Should().NotBeNull();

            regionScheme.Regions.Should().NotBeNull();

            regionScheme.Regions.Select(x => x.Countries).All(x => x != null).Should().BeTrue();
        }
    }
}
