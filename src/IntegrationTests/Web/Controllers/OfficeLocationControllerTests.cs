using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;
using Xunit;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class OfficeLocationControllerTests
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
                    TimeZone = "CST",
                    Operating = 1
                };

                var officeDto1 = new OfficeDto()
                {
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

                officesArray[0].Name.Should().Be("Austin");
                officesArray[0].Address.Should().Be("Dimensional Place 6300 Bee Cave Road");
                officesArray[0].Country.Should().Be("United States");
                officesArray[0].Switchboard.Should().Be("***REMOVED***");
                officesArray[0].Fax.Should().Be("+***REMOVED***");
                officesArray[0].TimeZone.Should().Be("CST");
                officesArray[0].Operating.Should().Be(true);

                officesArray[1].Name.Should().Be("Berlin");
                officesArray[1].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                officesArray[1].Country.Should().Be("Germany");
                officesArray[1].Switchboard.Should().Be("***REMOVED***");
                officesArray[1].Fax.Should().Be("***REMOVED***");
                officesArray[1].TimeZone.Should().Be("CET");
                officesArray[1].Operating.Should().Be(false);

            });
        }
    }
}
