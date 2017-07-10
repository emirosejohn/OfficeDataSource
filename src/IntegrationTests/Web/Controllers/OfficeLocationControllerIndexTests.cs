using System.Linq;
using FluentAssertions;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;
using Xunit;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class OfficeLocationControllerIndexTests
    {
        [Fact]
        public void ShouldReturnNoOfficesWhenNoOfficesAreFound()
        {
            var testHelper = new TestHelper();

            testHelper.DatabaseDataDeleter(() =>
            {
                var controller = testHelper.CreateController();

                var actionResult = controller.Index();

                var viewResultModel = testHelper.GetOfficeModelModelFromActionResult(actionResult);

                var officesArray = viewResultModel.Offices.ToArray();

                officesArray.Length.Should().Be(0);

                var countryArray = viewResultModel.Countries;

                countryArray.Length.Should().Be(6);

                countryArray[0].CountryId.Should().Be(1);
                countryArray[0].Name.Should().Be("Country 1");

                countryArray[1].CountryId.Should().Be(2);
                countryArray[1].Name.Should().Be("Country 2");

                countryArray[2].CountryId.Should().Be(3);
                countryArray[2].Name.Should().Be("Country 3");

                countryArray[3].CountryId.Should().Be(4);
                countryArray[3].Name.Should().Be("Country 4");

                countryArray[4].CountryId.Should().Be(5);
                countryArray[4].Name.Should().Be("Country 5");

                countryArray[5].CountryId.Should().Be(6);
                countryArray[5].Name.Should().Be("Country 6");


            });
        }

        [Fact]
        public void ShouldReturnOfficesWhenOfficesAreFound()
        {
            var testHelper = new TestHelper();

            testHelper.DatabaseDataDeleter(() =>
            {
                var officeDto0 = new OfficeDto()
                {
                    OfficeId = 1,
                    Name = "Austin",
                    Address = "Dimensional Place 6300 Bee Cave Road",
                    Country = "United States",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    TimeZone = "CST",
                    Operating = 1
                };

                var officeDto1 = new OfficeDto()
                {
                    OfficeId = 2,
                    Name = "Berlin",
                    Address = "***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin",
                    Country = "Germany",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    TimeZone = "CET",
                    Operating = 0
                };

                testHelper.InsertOfficeDto(officeDto0);

                testHelper.InsertOfficeDto(officeDto1);

                var controller = testHelper.CreateController();

                var actionResult = controller.Index();

                var viewResultModel = testHelper.GetOfficeModelModelFromActionResult(actionResult);

                var officesArray = viewResultModel.Offices.ToArray();

                officesArray.Length.Should().Be(2);

                officesArray[0].OfficeId.Should().Be(1);
                officesArray[0].Name.Should().Be("Austin");
                officesArray[0].Address.Should().Be("Dimensional Place 6300 Bee Cave Road");
                officesArray[0].Country.Should().Be("United States");
                officesArray[0].Switchboard.Should().Be("***REMOVED***");
                officesArray[0].Fax.Should().Be("+***REMOVED***");
                officesArray[0].TimeZone.Should().Be("CST");
                officesArray[0].Operating.Should().Be(true);

                officesArray[1].OfficeId.Should().Be(2);
                officesArray[1].Name.Should().Be("Berlin");
                officesArray[1].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                officesArray[1].Country.Should().Be("Germany");
                officesArray[1].Switchboard.Should().Be("***REMOVED***");
                officesArray[1].Fax.Should().Be("***REMOVED***");
                officesArray[1].TimeZone.Should().Be("CET");
                officesArray[1].Operating.Should().Be(false);

                var countryArray = viewResultModel.Countries;

                countryArray.Length.Should().Be(6);

                countryArray[0].CountryId.Should().Be(1);
                countryArray[0].Name.Should().Be("Country 1");

                countryArray[1].CountryId.Should().Be(2);
                countryArray[1].Name.Should().Be("Country 2");

                countryArray[2].CountryId.Should().Be(3);
                countryArray[2].Name.Should().Be("Country 3");

                countryArray[3].CountryId.Should().Be(4);
                countryArray[3].Name.Should().Be("Country 4");

                countryArray[4].CountryId.Should().Be(5);
                countryArray[4].Name.Should().Be("Country 5");

                countryArray[5].CountryId.Should().Be(6);
                countryArray[5].Name.Should().Be("Country 6");

            });
        }
    }
}
