using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services.OfficeWithEnumeration;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.WebUi.Models;
using Xunit;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class OfficeLocationControllerSaveTests
    {
        [Fact]
        public void ShouldReturnNoOfficesWhenNoOfficesAreFound()
        {
            var testHelper = new TestHelper();

            testHelper.DatabaseDataDeleter(() =>
            {
                var controller = testHelper.CreateController();



                var emptyOffice = new OfficeModel();

                var actionResult = controller.Save(emptyOffice);

                var viewResult = testHelper.GetRedirectToRouteFromActionResult(actionResult);

                viewResult.Should().NotBeNull();
            });
        }

        [Fact]
        public void ShouldInsertDtoIntoDatabase()
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
                    Country = "Germany",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    TimeZone = "Central European Timezone",
                    Operating = 0
                };

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                var controller = testHelper.CreateController();

                var locationModel = officeDto1.ExtractOfficeLocation();

                var locationOffice = new OfficeModel()
                {
                    Offices = new OfficeWithEnumeration[]
                    {
                        new OfficeWithEnumeration(locationModel, testHelper.GetAllCountries(), null)
                    }
                    
                };

                var actionResult = controller.Save(locationOffice);

                var officeLocationRepository = testHelper.GetOfficeLocationRepository();
                var offices = officeLocationRepository.GetAll();

                offices.Length.Should().Be(2);
                    
                offices[0].OfficeId.Should().Be(expectedOfficeId1);
                offices[0].Name.Should().Be("Austin");
                offices[0].Address.Should().Be("Dimensional Place 6300 Bee Cave Road");
                offices[0].Country.Should().Be("United States");
                offices[0].Switchboard.Should().Be("***REMOVED***");
                offices[0].Fax.Should().Be("+***REMOVED***");
                offices[0].TimeZone.Should().Be("Central Standard Time");
                offices[0].Operating.Should().Be("Active");

                offices[1].OfficeId.Should().BeGreaterThan(0);
                offices[1].Name.Should().Be("Berlin");
                offices[1].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                offices[1].Country.Should().Be("Germany");
                offices[1].Switchboard.Should().Be("***REMOVED***");
                offices[1].Fax.Should().Be("***REMOVED***");
                offices[1].TimeZone.Should().Be("Central European Timezone");
                offices[1].Operating.Should().Be("Closed");
            });
        }

        [Fact]
        public void ShouldUpdateExistingDtoWithNewInfo()
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



                var expectedOfficeId = testHelper.InsertOfficeDto(officeDto0);

                var updatedOfficeDto = SimulateUpdatingOfficeLocation(expectedOfficeId);

                var controller = testHelper.CreateController();

                var locationModel = updatedOfficeDto.ExtractOfficeLocation();

                var locationOffice = new OfficeModel()
                {
                    Offices = new OfficeWithEnumeration[]
                    {
                        new OfficeWithEnumeration(locationModel, testHelper.GetAllCountries(), null)
                    }
                };

                var actionResult = controller.Save(locationOffice);

                var officeLocationRepository = testHelper.GetOfficeLocationRepository();

                var offices = officeLocationRepository.GetAll();

                offices.Length.Should().Be(1);

                offices[0].OfficeId.Should().Be(expectedOfficeId);
                offices[0].Name.Should().Be("Changed");
                offices[0].Address.Should().Be("Updated");
                offices[0].Country.Should().Be("New string");
                offices[0].Switchboard.Should().Be("Different value here");
                offices[0].Fax.Should().Be("This had changed");
                offices[0].TimeZone.Should().Be("Not the same string");
                offices[0].Operating.Should().Be("Closed");
            });
        }

        private OfficeDto SimulateUpdatingOfficeLocation(int expectedOfficeId)
        {
            return new OfficeDto()
            {
                OfficeId = expectedOfficeId,
                Name = "Changed",
                Address = "Updated",
                Country = "New string",
                Switchboard = "Different value here",
                Fax = "This had changed",
                TimeZone = "Not the same string",
                Operating = 0
            };
        }
    }
}
