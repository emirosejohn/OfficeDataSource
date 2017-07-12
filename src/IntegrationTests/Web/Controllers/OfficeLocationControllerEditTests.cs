using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.WebUi.Models;
using Xunit;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class OfficeLocationControllerEditTests
    {
        [Fact(Skip="Needs to be fixed.")]
        public void ShouldReturnNoOfficesWhenNoOfficesAreFound()
        {
            var testHelper = new TestHelper();

            testHelper.DatabaseDataDeleter(() =>
            {
                var controller = testHelper.CreateController();

                var actionResult = controller.Save(new OfficeLocationModel());

                var viewResultModel = testHelper.GetOfficeModelModelFromActionResult(actionResult);

                viewResultModel.Should().NotBeNull();
            });
        }

        [Fact(Skip = "Needs to be fixed.")]
        public void ShouldInsertDtoIntoDatabase()
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
                    TimeZone = "Central Standard Time",
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
                    TimeZone = "Central European Timezone",
                    Operating = 0
                };

                testHelper.InsertOfficeDto(officeDto0);
                Debug.WriteLine("1");
                var controller = testHelper.CreateController();

                var locationModel = new OfficeLocationModel();
                locationModel.EditedOffice = officeDto1.ExtractOfficeLocation();

                var actionResult = controller.Save(locationModel);
                Debug.WriteLine("2");
                var viewResultModel = testHelper.GetOfficeModelModelFromActionResult(actionResult);
                viewResultModel.OfficeEdit.Should().NotBeNull();

                var officeLocationRepository = testHelper.GetOfficeLocationRepository();
                var offices = officeLocationRepository.GetAll();

                offices.Length.Should().Be(2);
                    
                offices[0].OfficeId.Should().Be(1);
                offices[0].Name.Should().Be("Austin");
                offices[0].Address.Should().Be("Dimensional Place 6300 Bee Cave Road");
                offices[0].Country.Should().Be("United States");
                offices[0].Switchboard.Should().Be("***REMOVED***");
                offices[0].Fax.Should().Be("+***REMOVED***");
                offices[0].TimeZone.Should().Be("Central Standard Time");
                offices[0].Operating.Should().Be("Active");

                offices[1].OfficeId.Should().Be(2);
                offices[1].Name.Should().Be("Berlin");
                offices[1].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                offices[1].Country.Should().Be("Germany");
                offices[1].Switchboard.Should().Be("***REMOVED***");
                offices[1].Fax.Should().Be("***REMOVED***");
                offices[1].TimeZone.Should().Be("Central European Timezone");
                offices[1].Operating.Should().Be("Closed");
            });
        }

        [Fact(Skip = "Needs to be fixed.")]
        public void ShouldUpdateExistingDtoWithNewInfo()
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
                    TimeZone = "Central Standard Time",
                    Operating = 1
                };

                var officeDto1 = new OfficeDto()
                {
                    OfficeId = 1,
                    Name = "Berlin",
                    Address = "***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin",
                    Country = "Germany",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    TimeZone = "Central European Timezone",
                    Operating = 0
                };

                testHelper.InsertOfficeDto(officeDto0);
                Debug.WriteLine("3");

                var controller = testHelper.CreateController();

                var locationModel = new OfficeLocationModel();
                locationModel.EditedOffice = officeDto1.ExtractOfficeLocation();

                var actionResult = controller.Save(locationModel);

                Debug.WriteLine("4");

                var viewResultModel = testHelper.GetOfficeModelModelFromActionResult(actionResult);
                viewResultModel.OfficeEdit.Should().NotBeNull();

                var officeLocationRepository = testHelper.GetOfficeLocationRepository();
                var offices = officeLocationRepository.GetAll();

                offices.Length.Should().Be(1);

                offices[0].OfficeId.Should().Be(1);
                offices[0].Name.Should().Be("Berlin");
                offices[0].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                offices[0].Country.Should().Be("Germany");
                offices[0].Switchboard.Should().Be("***REMOVED***");
                offices[0].Fax.Should().Be("***REMOVED***");
                offices[0].TimeZone.Should().Be("Central European Timezone");
                offices[0].Operating.Should().Be("Closed");
            });
        }
    }
}
