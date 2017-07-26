using System;
using System.Web.Mvc;
using FluentAssertions;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;
using OfficeLocationMicroservice.Data.CountryWebApi;
using OfficeLocationMicroservice.Data.OfficeLocationDatabase;
using OfficeLocationMicroservice.IntegrationTests.Email;
using OfficeLocationMicroservice.WebUi.Controllers;
using OfficeLocationMicroservice.WebUi.Models;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class TestHelper
    {
        private readonly AllTablesDeleter _allTablesDeleter;
        private readonly OfficeDataTableGateway _officeDataTableGateway;
        private readonly CountryWebApiGatewayStub _countryWebApiGateway;
        private readonly EmailClientFake _emailClient;
        private readonly UserWrapperFake _userWrapperFake;

        public TestHelper()
        {
            var databaseSettings = new DataConnectionStringsForIntegrationTests();
            var systemLogForIntegrationTests = new SystemLogForIntegrationTests();

            CurrentDate = new DateTime(2017, 2, 3);

            _allTablesDeleter = new AllTablesDeleter();

            _officeDataTableGateway = new OfficeDataTableGateway(databaseSettings, systemLogForIntegrationTests);

            _countryWebApiGateway = new CountryWebApiGatewayStub();

            _emailClient = new EmailClientFake(databaseSettings);
            

            _userWrapperFake = new UserWrapperFake("user");
            _userWrapperFake.GroupNameConstants = databaseSettings;
        }

        public OfficeLocationRepository GetOfficeLocationRepository()
        {
            var countryRepostiory = GetCountryRepository();
            return new OfficeLocationRepository(_officeDataTableGateway, countryRepostiory);
        }

        public CountryRepository GetCountryRepository()
        {
            return new CountryRepository(_countryWebApiGateway);
        }

        public OfficeLocationFacade GetOfficeLocationFacade()
        {
            var officeLocationRepository = this.GetOfficeLocationRepository();
            return new OfficeLocationFacade(officeLocationRepository, _emailClient);
        }

        public OfficeLocationController CreateController()
        {
            var officeLocationFacade = GetOfficeLocationFacade();
            return new OfficeLocationController(officeLocationFacade, _userWrapperFake);
        }

        public DateTime CurrentDate { get; set; }

        public int  InsertOfficeDto(
            OfficeDto dto)
        {
            return _officeDataTableGateway.Insert(dto);
        }

        public void DatabaseDataDeleter(
            Action act)
        {
            var tablesToSkip = new AllTablesDeleter.TableInfoDto[0];

            var databaseSettings = new DataConnectionStringsForIntegrationTests();

            _allTablesDeleter.DeleteAllDataFromTables(
                databaseSettings.ConnectionString,
                tablesToSkip);

            act();

            _allTablesDeleter.DeleteAllDataFromTables(
                databaseSettings.ConnectionString, tablesToSkip);
        }

        public OfficeModel GetOfficeModelFromActionResult(
            ActionResult actionResult)
        {
            actionResult.Should().BeAssignableTo<ViewResult>();
            var viewResult = (ViewResult) actionResult;

            viewResult.Model.Should().BeAssignableTo<OfficeModel>();
            var officeModel = (OfficeModel) viewResult.Model;

            return officeModel;
        }

        public RedirectToRouteResult GetRedirectToRouteFromActionResult(
            ActionResult actionResult)
        {
            actionResult.Should().BeAssignableTo<RedirectToRouteResult>();
            var viewResult = (RedirectToRouteResult)actionResult;
            return viewResult;
        }

        public EmailClientFake GetEmailClient()
        {
            return _emailClient;
        }

        public UserWrapperFake GetUserWrapper()
        {
            return _userWrapperFake;
        }
    }
}
