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

                var messages = testHelper.GetEmailClient().GetSentMessage();

                messages.Should().BeEmpty();
            });
        }

        [Fact]
        public void ShouldInsertIntoDatabase()
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
                    Operating = 1
                };

                var officeDto1 = new OfficeDto()
                {
                    Name = "Berlin",
                    Address = "***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin",
                    Country = "Germany",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    Operating = 0
                };

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var locationModel = officeDto1.ExtractOfficeLocation();

                locationModel.HasChanged = "True";

                var locationOffice = new OfficeModel()
                {
                    Offices = new []
                    {
                        locationModel
                    },                     
                };

                var actionResult = controller.Save(locationOffice);

                //**************************** 

                var officeLocationRepository = testHelper.GetOfficeLocationRepository();
                var offices = officeLocationRepository.GetAll();

                offices.Length.Should().Be(2);
                    
                offices[0].OfficeId.Should().Be(expectedOfficeId1);
                offices[0].Name.Should().Be("Austin");
                offices[0].Address.Should().Be("Dimensional Place 6300 Bee Cave Road");
                offices[0].Country.Should().Be("United States");
                offices[0].Switchboard.Should().Be("***REMOVED***");
                offices[0].Fax.Should().Be("+***REMOVED***");
                offices[0].Operating.Should().Be("Active");

                offices[1].OfficeId.Should().BeGreaterThan(0);
                offices[1].Name.Should().Be("Berlin");
                offices[1].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                offices[1].Country.Should().Be("Germany");
                offices[1].Switchboard.Should().Be("***REMOVED***");
                offices[1].Fax.Should().Be("***REMOVED***");
                offices[1].Operating.Should().Be("Closed");

                //**************************** 

                var messages = testHelper.GetEmailClient().GetSentMessage();

                messages.Count.Should().Be(1);

                var message = messages.First();

                var expectedSubject = OfficeLocationRepositoryHelper.GenerateInsertEmailSubject(
                    locationModel);

                var expectedBody = OfficeLocationRepositoryHelper.GenerateInsertEmailBody(
                    locationModel);

                message.Body.Should().Be(expectedBody);
                message.Subject.Should().Be(expectedSubject);

                message.To.Count.Should().Be(2);
                var toArray = message.To.ToArray();

                toArray[0].Should().Be("testTo1@dimensional.com");
                toArray[1].Should().Be("testTo2@dimensional.com");

                message.From.Should().Be("testFrom@dimensional.com");

            });
        }

        [Fact(DisplayName= "Make sure that update works with offices array")]
        public void ShouldUpdateOfficesWithNewInfo()
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
                    Operating = 1
                };

                var expectedOfficeId = testHelper.InsertOfficeDto(officeDto0);

                var updatedOfficeDto = SimulateUpdatingOfficeLocation(expectedOfficeId);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var locationModel = updatedOfficeDto.ExtractOfficeLocation();

                locationModel.HasChanged = "True";

                var locationOffice = new OfficeModel()
                {
                    Offices = new[]
                    {
                        locationModel, null, new OfficeLocation()
                    },
                };

                var actionResult = controller.Save(locationOffice);


                //************************
                var officeLocationRepository = testHelper.GetOfficeLocationRepository();

                var offices = officeLocationRepository.GetAll();

                offices.Length.Should().Be(1);

                offices[0].OfficeId.Should().Be(expectedOfficeId);
                offices[0].Name.Should().Be("Changed");
                offices[0].Address.Should().Be("Updated");
                offices[0].Country.Should().Be("New string");
                offices[0].Switchboard.Should().Be("Different value here");
                offices[0].Fax.Should().Be("This had changed");
                offices[0].Operating.Should().Be("Closed");

                //**************************** 

                var messages = testHelper.GetEmailClient().GetSentMessage();

                messages.Count.Should().Be(1);

                var message = messages.First();

                var expectedSubject = OfficeLocationRepositoryHelper.GenerateUpdateEmailSubject(
                    officeDto0.ExtractOfficeLocation());

                var expectedBody = OfficeLocationRepositoryHelper.GenerateUpdateEmailBody(
                    locationModel, officeDto0.ExtractOfficeLocation());

                message.Body.Should().Be(expectedBody);
                message.Subject.Should().Be(expectedSubject);

                message.To.Count.Should().Be(2);
                var toArray = message.To.ToArray();

                toArray[0].Should().Be("testTo1@dimensional.com");
                toArray[1].Should().Be("testTo2@dimensional.com");

                message.From.Should().Be("testFrom@dimensional.com");
            });
        }

        [Fact(DisplayName = "Make sure that update works with newoffice")]
        public void ShouldUpdateNewOfficeWithNewInfo()
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
                    Operating = 1
                };


                var expectedOfficeId = testHelper.InsertOfficeDto(officeDto0);

                var updatedOfficeDto = SimulateUpdatingOfficeLocation(expectedOfficeId);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var locationModel = updatedOfficeDto.ExtractOfficeLocation();

                locationModel.HasChanged = "True";

                var locationOffice = new OfficeModel()
                {
                    NewOffice = locationModel      
                };

                locationOffice.NewOffice.HasChanged = "true";

                var actionResult = controller.Save(locationOffice);

                var officeLocationRepository = testHelper.GetOfficeLocationRepository();

                var offices = officeLocationRepository.GetAll();

                //***************************

                offices.Length.Should().Be(1);

                offices[0].OfficeId.Should().Be(expectedOfficeId);
                offices[0].Name.Should().Be("Changed");
                offices[0].Address.Should().Be("Updated");
                offices[0].Country.Should().Be("New string");
                offices[0].Switchboard.Should().Be("Different value here");
                offices[0].Fax.Should().Be("This had changed");
                offices[0].Operating.Should().Be("Closed");
                
            });
        }

        [Fact(DisplayName="Should Reorder Offices by Name")]
        public void ShouldReturnOfficesInAlphabeticalOrder()
        {
            var testHelper = new TestHelper();

            testHelper.DatabaseDataDeleter(() =>
            {

                var officeDto0 = new OfficeDto()
                {
                    Name = "Berlin",
                    Address = "***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin",
                    Country = "Germany",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    Operating = 0
                };

                var officeDto1 = new OfficeDto()
                {
                    Name = "Austin",
                    Address = "Dimensional Place 6300 Bee Cave Road",
                    Country = "United States",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    Operating = 1
                };

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var locationModel = officeDto1.ExtractOfficeLocation();

                locationModel.HasChanged = "True";

                var locationOffice = new OfficeModel()
                {
                    Offices = new[]
                    {
                        locationModel
                    },
                };

                var actionResult = controller.Save(locationOffice);

                var officeLocationRepository = testHelper.GetOfficeLocationRepository();
                var offices = officeLocationRepository.GetAll();

                //*********************************

                offices.Length.Should().Be(2);

                offices[0].OfficeId.Should().BeGreaterThan(0);
                offices[0].Name.Should().Be("Austin");
                offices[0].Address.Should().Be("Dimensional Place 6300 Bee Cave Road");
                offices[0].Country.Should().Be("United States");
                offices[0].Switchboard.Should().Be("***REMOVED***");
                offices[0].Fax.Should().Be("+***REMOVED***");
                offices[0].Operating.Should().Be("Active");

                offices[1].OfficeId.Should(). Be(expectedOfficeId1);
                offices[1].Name.Should().Be("Berlin");
                offices[1].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                offices[1].Country.Should().Be("Germany");
                offices[1].Switchboard.Should().Be("***REMOVED***");
                offices[1].Fax.Should().Be("***REMOVED***");
                offices[1].Operating.Should().Be("Closed");

            });
        }      

        [Fact(DisplayName = "Should deny users with invalid permissions the ability to save.")]
        public void ShouldNotAllowInvalidUsersToSave()
        {

            var testHelper = new TestHelper();

            testHelper.DatabaseDataDeleter(() =>
            {

                var officeDto0 = new OfficeDto()
                {
                    Name = "Berlin",
                    Address = "***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin",
                    Country = "Germany",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    Operating = 0
                };

                var officeDto1 = new OfficeDto()
                {
                    Name = "Austin",
                    Address = "Dimensional Place 6300 Bee Cave Road",
                    Country = "United States",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    Operating = 1
                };

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                var controller = testHelper.CreateController();

                var locationModel = officeDto1.ExtractOfficeLocation();
                locationModel.HasChanged = "True";

                var locationOffice = new OfficeModel()
                {
                    Offices = new[]
                    {
                        locationModel
                    },
                };

                var actionResult = controller.Save(locationOffice);

                var officeLocationRepository = testHelper.GetOfficeLocationRepository();
                var offices = officeLocationRepository.GetAll();

                //*********************************

                offices.Length.Should().Be(1);

                offices[0].OfficeId.Should().Be(expectedOfficeId1);
                offices[0].Name.Should().Be("Berlin");
                offices[0].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");
                offices[0].Country.Should().Be("Germany");
                offices[0].Switchboard.Should().Be("***REMOVED***");
                offices[0].Fax.Should().Be("***REMOVED***");
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
                Operating = 0
            };
        }
    }
}
