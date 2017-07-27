﻿using System.Linq;
using FluentAssertions;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;
using OfficeLocationMicroservice.WebUi.Models;
using Xunit;
using OfficeLocation = OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.OfficeLocation;

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
                    CountrySlug = "C1",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    Operating = 1
                };

                var officeDto1 = new OfficeDto()
                {
                    Name = "Berlin",
                    Address = "***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin",
                    CountrySlug = "C2",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    Operating = 0
                };

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var country1 = testHelper.GetCountryRepository().GetCountryBySlug(officeDto1.CountrySlug);
                var locationModel = new WebOfficeLocation(officeDto1, country1);

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

                offices[0].Country.Slug.Should().Be("C1");
                offices[0].Country.Name.Should().Be("Country 1");

                offices[0].Switchboard.Should().Be("***REMOVED***");
                offices[0].Fax.Should().Be("+***REMOVED***");
                offices[0].Operating.Should().Be("Active");


                offices[1].OfficeId.Should().BeGreaterThan(0);
                offices[1].Name.Should().Be("Berlin");
                offices[1].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");

                offices[1].Country.Slug.Should().Be("C2");
                offices[1].Country.Name.Should().Be("Country 2");

                offices[1].Switchboard.Should().Be("***REMOVED***");
                offices[1].Fax.Should().Be("***REMOVED***");
                offices[1].Operating.Should().Be("Closed");

                //**************************** 

                var messages = testHelper.GetEmailClient().GetSentMessage();

                messages.Count.Should().Be(1);

                var message = messages.First();

                var expectedSubject = OfficeLocationFacadeHelper.GenerateInsertEmailSubject(
                    locationModel);

                var expectedBody = OfficeLocationFacadeHelper.GenerateInsertEmailBody(
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
                    CountrySlug = "C1",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    Operating = 1
                };

                var expectedOfficeId = testHelper.InsertOfficeDto(officeDto0);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var updatedOfficeDto = SimulateUpdatingOfficeLocation(expectedOfficeId);

                var country1 = testHelper.GetCountryRepository().GetCountryBySlug(updatedOfficeDto.CountrySlug);
                var locationModel = new WebOfficeLocation(updatedOfficeDto, country1);

                locationModel.HasChanged = "True";

                var locationOffice = new OfficeModel()
                {
                    Offices = new[]
                    {
                        locationModel, null, new WebOfficeLocation()
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

                offices[0].Country.Name.Should().Be("Country 1");
                offices[0].Country.Slug.Should().Be("C1");

                offices[0].Switchboard.Should().Be("Different value here");
                offices[0].Fax.Should().Be("This had changed");
                offices[0].Operating.Should().Be("Closed");

                //**************************** 

                var messages = testHelper.GetEmailClient().GetSentMessage();

                messages.Count.Should().Be(1);

                var message = messages.First();

                var originalCountry = testHelper.GetCountryRepository().GetCountryBySlug(officeDto0.CountrySlug);
                var originalOfficeLocation = new OfficeLocation(officeDto0, originalCountry);

                var expectedSubject = OfficeLocationFacadeHelper.GenerateUpdateEmailSubject(
                    originalOfficeLocation);

                var expectedBody = OfficeLocationFacadeHelper.GenerateUpdateEmailBody(
                    locationModel, originalOfficeLocation);

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
                    CountrySlug = "C1",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    Operating = 1
                };


                var expectedOfficeId = testHelper.InsertOfficeDto(officeDto0);

                var updatedOfficeDto = SimulateUpdatingOfficeLocation(expectedOfficeId);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var updatedCountry = testHelper.GetCountryRepository().GetCountryBySlug(updatedOfficeDto.CountrySlug);
                var locationModel = new WebOfficeLocation(updatedOfficeDto, updatedCountry);

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

                offices[0].Country.Name.Should().Be("Country 1");
                offices[0].Country.Slug.Should().Be("C1");

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
                    CountrySlug = "C1",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    Operating = 1
                };

                var officeDto1 = new OfficeDto()
                {
                    Name = "Austin",
                    Address = "Dimensional Place 6300 Bee Cave Road",
                    CountrySlug = "C2",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    Operating = 1
                };

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var Country1 = testHelper.GetCountryRepository().GetCountryBySlug(officeDto1.CountrySlug);
                var locationModel = new WebOfficeLocation(officeDto1, Country1);

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

                offices[0].Country.Name.Should().Be("Country 2");
                offices[0].Country.Slug.Should().Be("C2");

                offices[0].Switchboard.Should().Be("***REMOVED***");
                offices[0].Fax.Should().Be("+***REMOVED***");
                offices[0].Operating.Should().Be("Active");

                offices[1].OfficeId.Should(). Be(expectedOfficeId1);
                offices[1].Name.Should().Be("Berlin");
                offices[1].Address.Should().Be("***REMOVED*** Kurfürstendamm 194, D - 10707 Berlin");

                offices[1].Country.Name.Should().Be("Country 1");
                offices[1].Country.Slug.Should().Be("C1");

                offices[1].Switchboard.Should().Be("***REMOVED***");
                offices[1].Fax.Should().Be("***REMOVED***");
                offices[1].Operating.Should().Be("Active");

            });
        }

        [Fact(DisplayName = "Should Reorder Offices by Operating status then Country")]
        public void ShouldReturnOfficesActiveFirst()
        {
            var testHelper = new TestHelper();

            testHelper.DatabaseDataDeleter(() =>
            {

                var officeDto0 = new OfficeDto()
                {
                    Name = "Office A",
                    Address = "Office A addr",
                    CountrySlug = "C1",
                    Switchboard = "Office A switchboard",
                    Fax = "Office A fax",
                    Operating = 0
                };
                //A should follow B, since A is closed while B is open
                var officeDto1 = new OfficeDto()
                {
                    Name = "Office B",
                    Address = "Office B addr",
                    CountrySlug = "C1",
                    Switchboard = "Office B switchboard",
                    Fax = "Office B fax",
                    Operating = 1
                };
                //C should follow A since they are both closed but A is first alphabetically.
                var officeDto2 = new OfficeDto()
                {
                    Name = "Office C",
                    Address = "Office C addr",
                    CountrySlug = "C1",
                    Switchboard = "Office C switchboard",
                    Fax = "Office C fax",
                    Operating = 0
                };

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                var expectedOfficeId2 = testHelper.InsertOfficeDto(officeDto2);

                var userWrapper = testHelper.GetUserWrapper();
                userWrapper.MakeUserPartOfGroup(userWrapper.GroupNameConstants.AdminGroup);

                var controller = testHelper.CreateController();

                var Country1 = testHelper.GetCountryRepository().GetCountryBySlug(officeDto1.CountrySlug);
                var locationModel = new WebOfficeLocation(officeDto1, Country1);

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

                offices.Length.Should().Be(3);

                offices[0].OfficeId.Should().BeGreaterThan(0);
                offices[0].Name.Should().Be("Office B");
                offices[0].Address.Should().Be("Office B addr");

                offices[0].Country.Name.Should().Be("Country 1");
                offices[0].Country.Slug.Should().Be("C1");

                offices[0].Switchboard.Should().Be("Office B switchboard");
                offices[0].Fax.Should().Be("Office B fax");
                offices[0].Operating.Should().Be("Active");

                offices[1].OfficeId.Should().Be(expectedOfficeId1);
                offices[1].Name.Should().Be("Office A");
                offices[1].Address.Should().Be("Office A addr");

                offices[0].Country.Name.Should().Be("Country 1");
                offices[0].Country.Slug.Should().Be("C1");

                offices[1].Switchboard.Should().Be("Office A switchboard");
                offices[1].Fax.Should().Be("Office A fax");
                offices[1].Operating.Should().Be("Closed");

                offices[2].OfficeId.Should().Be(expectedOfficeId2);
                offices[2].Name.Should().Be("Office C");
                offices[2].Address.Should().Be("Office C addr");

                offices[0].Country.Name.Should().Be("Country 1");
                offices[0].Country.Slug.Should().Be("C1");

                offices[2].Switchboard.Should().Be("Office C switchboard");
                offices[2].Fax.Should().Be("Office C fax");
                offices[2].Operating.Should().Be("Closed");
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
                    CountrySlug = "C1",
                    Switchboard = "***REMOVED***",
                    Fax = "***REMOVED***",
                    Operating = 0
                };

                var officeDto1 = new OfficeDto()
                {
                    Name = "Austin",
                    Address = "Dimensional Place 6300 Bee Cave Road",
                    CountrySlug = "C1",
                    Switchboard = "***REMOVED***",
                    Fax = "+***REMOVED***",
                    Operating = 1
                };

                var expectedOfficeId1 = testHelper.InsertOfficeDto(officeDto0);

                var controller = testHelper.CreateController();

                var Country1 = testHelper.GetCountryRepository().GetCountryBySlug(officeDto1.CountrySlug);
                var locationModel = new WebOfficeLocation(officeDto1, Country1);

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

                offices[0].Country.Name.Should().Be("Country 1");
                offices[0].Country.Slug.Should().Be("C1");

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
                CountrySlug = "C1",
                Switchboard = "Different value here",
                Fax = "This had changed",
                Operating = 0
            };
        }
    }
}
