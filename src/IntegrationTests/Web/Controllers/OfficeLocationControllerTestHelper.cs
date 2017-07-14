using System;
using System.Web.Mvc;
using FluentAssertions;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.Data.CountryWebApi;
using OfficeLocationMicroservice.Data.OfficeLocationDatabase;
using OfficeLocationMicroservice.WebUi.Controllers;
using OfficeLocationMicroservice.WebUi.Models;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class TestHelper
    {
        private readonly AllTablesDeleter _allTablesDeleter;
        private readonly OfficeDataTableGateway _officeDataTableGateway;
        private readonly CountryWebApiGatewayStub _countryWebApiGateway;

        public TestHelper()
        {
            var databaseSettings = new DataConnectionStringsForIntegrationTests();
            var systemLogForIntegrationTests = new SystemLogForIntegrationTests();

            CurrentDate = new DateTime(2017, 2, 3);

            _allTablesDeleter = new AllTablesDeleter();

            _officeDataTableGateway = new OfficeDataTableGateway(databaseSettings, systemLogForIntegrationTests);

            _countryWebApiGateway = new CountryWebApiGatewayStub();
        }

        public OfficeLocationRepository GetOfficeLocationRepository()
        {
            

            return new OfficeLocationRepository(_officeDataTableGateway);
        }

        public Country[] GetAllCountries()
        {
            var countryRepository = new CountryRepository(_countryWebApiGateway);
            return countryRepository.GetAllCountries();
        }

        public OfficeLocationController CreateController()
        {
            var officeLocationRepository = new OfficeLocationRepository(_officeDataTableGateway);
            var countryRepository = new CountryRepository(_countryWebApiGateway);
            return new OfficeLocationController(officeLocationRepository, countryRepository);
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
    }
}
