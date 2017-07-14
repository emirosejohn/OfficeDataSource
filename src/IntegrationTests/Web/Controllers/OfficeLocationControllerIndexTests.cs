using System.Diagnostics;
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

                var viewResultModel = testHelper.GetOfficeModelFromActionResult(actionResult);

                var officesArray = viewResultModel.Offices.ToArray();

                officesArray.Length.Should().Be(0);

                //var countryArray = viewResultModel.Offices[0].Countries.ToArray();




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
                    Name = "Austin",
                    Address = "Dimensional Place 6300 Bee Cave Road",
                    Country = "United States",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    TimeZone = "Central Standard Time",
                    Operating = 1
                };

                var officeDto1 = new OfficeDto()
                {
                    Name = "Berlin",
                    Address = "***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin",
                    Country = "Country 2",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    TimeZone = "Central European Timezone",
                    Operating = 0
                };

                Debug.WriteLine("5");

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                Debug.WriteLine("6");

                var expectedOfficeId2 = testHelper.InsertOfficeDto(officeDto1);

                var controller = testHelper.CreateController();

                var actionResult = controller.Index();

                var viewResultModel = testHelper.GetOfficeModelFromActionResult(actionResult);

                var officesArray = viewResultModel.Offices.ToArray();

                officesArray.Length.Should().Be(2);

                officesArray[0].Office.OfficeId.Should().Be(expectedOfficeId1);
                officesArray[0].Office.Name.Should().Be("Austin");
                officesArray[0].Office.Address.Should().Be("Dimensional Place 6300 Bee Cave Road");
                officesArray[0].Office.Country.Should().Be("United States");
                officesArray[0].Office.Switchboard.Should().Be("***REMOVED***");
                officesArray[0].Office.Fax.Should().Be("+***REMOVED***");
                officesArray[0].Office.TimeZone.Should().Be("Central Standard Time");

                officesArray[0].Office.Operating.Should().Be("Active");

                officesArray[1].Office.OfficeId.Should().Be(expectedOfficeId2);
                officesArray[1].Office.Name.Should().Be("Berlin");
                officesArray[1].Office.Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                officesArray[1].Office.Country.Should().Be("Country 2");
                officesArray[1].Office.Switchboard.Should().Be("***REMOVED***");
                officesArray[1].Office.Fax.Should().Be("***REMOVED***");
                officesArray[1].Office.TimeZone.Should().Be("Central European Timezone");

                officesArray[1].Office.Operating.Should().Be("Closed");

                var countryArray1 = viewResultModel.Offices[0].Countries.ToArray();
                var countryArray2 = viewResultModel.Offices[1].Countries.ToArray();


                countryArray1.Length.Should().Be(6);

                countryArray1[0].Value.Should().Be("Country 1");
                countryArray1[0].Text.Should().Be("Country 1");
                countryArray1[0].Selected.Should().Be(false);

                countryArray1[1].Value.Should().Be("Country 2");
                countryArray1[1].Text.Should().Be("Country 2");
                countryArray1[1].Selected.Should().Be(false);

                countryArray1[2].Value.Should().Be("Country 3");
                countryArray1[2].Text.Should().Be("Country 3");
                countryArray1[2].Selected.Should().Be(false);

                countryArray1[3].Value.Should().Be("Country 4");
                countryArray1[3].Text.Should().Be("Country 4");
                countryArray1[3].Selected.Should().Be(false);

                countryArray1[4].Value.Should().Be("Country 5");
                countryArray1[4].Text.Should().Be("Country 5");
                countryArray1[4].Selected.Should().Be(false);

                countryArray1[5].Value.Should().Be("Country 6");
                countryArray1[5].Text.Should().Be("Country 6");
                countryArray1[5].Selected.Should().Be(false);

                countryArray2.Length.Should().Be(6);

                countryArray2[0].Value.Should().Be("Country 1");
                countryArray2[0].Text.Should().Be("Country 1");
                countryArray2[0].Selected.Should().Be(false);

                countryArray2[1].Value.Should().Be("Country 2");
                countryArray2[1].Text.Should().Be("Country 2");
                countryArray2[1].Selected.Should().Be(true);

                countryArray2[2].Value.Should().Be("Country 3");
                countryArray2[2].Text.Should().Be("Country 3");
                countryArray2[2].Selected.Should().Be(false);

                countryArray2[3].Value.Should().Be("Country 4");
                countryArray2[3].Text.Should().Be("Country 4");
                countryArray2[3].Selected.Should().Be(false);

                countryArray2[4].Value.Should().Be("Country 5");
                countryArray2[4].Text.Should().Be("Country 5");
                countryArray2[4].Selected.Should().Be(false);

                countryArray2[5].Value.Should().Be("Country 6");
                countryArray2[5].Text.Should().Be("Country 6");
                countryArray2[5].Selected.Should().Be(false);



            });
        }
    }
}