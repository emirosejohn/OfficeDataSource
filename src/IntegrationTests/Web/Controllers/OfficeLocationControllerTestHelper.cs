using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentAssertions;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.Data.CountryWebApi;
using OfficeLocationMicroservice.Data.OfficeLocationDatabase;
using OfficeLocationMicroservice.WebUi.Controllers;
using OfficeLocationMicroservice.WebUi.Models;
using Country = OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi.Country;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class TestHelper
    {
        private readonly AllTablesDeleter _allTablesDeleter;
        private readonly OfficeDataTableGateway _officeDataTableGateway;
        private readonly CountryWebApiGatewayFake _countryWebApiGateway;

        public TestHelper()
        {
            var databaseSettings = new DataConnectionStringsForIntegrationTests();
            var systemLogForIntegrationTests = new SystemLogForIntegrationTests();

            CurrentDate = new DateTime(2017, 2, 3);

            _allTablesDeleter = new AllTablesDeleter();

            _officeDataTableGateway = new OfficeDataTableGateway(databaseSettings, systemLogForIntegrationTests);

            _countryWebApiGateway = new CountryWebApiGatewayFake();
        }

        public OfficeLocationRepository GetOfficeLocationRepository()
        {
            return new OfficeLocationRepository(_officeDataTableGateway);
        }

        public OfficeLocationController CreateController()
        {
            var officeLocationRepository = new OfficeLocationRepository(_officeDataTableGateway);
            var countryRepository = new CountryRepository(_countryWebApiGateway);
            return new OfficeLocationController(officeLocationRepository, countryRepository);
        }

        public DateTime CurrentDate { get; set; }

        public void InsertOfficeDto(
            OfficeDto dto)
        {
            _officeDataTableGateway.Insert(dto);
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

        public OfficeLocationModel GetOfficeModelModelFromActionResult(
            ActionResult actionResult)
        {
            actionResult.Should().BeAssignableTo<ViewResult>();
            var viewResult = (ViewResult) actionResult;

            viewResult.Model.Should().BeAssignableTo<OfficeLocationModel>();
            var officeModel = (OfficeLocationModel) viewResult.Model;

            return officeModel;
        }

    }

    internal class CountryWebApiGatewayFake : ICountryWebApiGateway
    {
        public RegionSchemeResponseJson GetRegionScheme()
        {
            var regionScheme = new RegionSchemeResponseJson();

            regionScheme.Regions = new List<Region>();

            var Region1 = new Region()
            {
                RegionId = 1,
                Name = "Region1",
                Slug = "R1",
                Countries = new List<Country>()
            };

            var Region2 = new Region()
            {
                RegionId = 1,
                Name = "Region2",
                Slug = "R2",
                Countries = new List<Country>()
            };

            var Region3 = new Region()
            {
                RegionId = 1,
                Name = "Region3",
                Slug = "R3",
                Countries = new List<Country>()
            };

            var country1 = new Country()
            {
                  CountryId = 1,
                  Name  = "Country 1",
                  Slug = "1",
                  ISOCountryCode = "1",
                  DFACountryCode = "1"
            };

            var country2 = new Country()
            {
                CountryId = 2,
                Name = "Country 2",
                Slug = "2",
                ISOCountryCode = "2",
                DFACountryCode = "2"
            };

            var country3 = new Country()
            {
                CountryId = 3,
                Name = "Country 3",
                Slug = "3",
                ISOCountryCode = "3",
                DFACountryCode = "3"
            };

            var country4 = new Country()
            {
                CountryId = 4,
                Name = "Country 4",
                Slug = "4",
                ISOCountryCode = "4",
                DFACountryCode = "4"
            };

            var country5 = new Country()
            {
                CountryId = 5,
                Name = "Country 5",
                Slug = "5",
                ISOCountryCode = "5",
                DFACountryCode = "5"
            };

            var country6 = new Country()
            {
                CountryId = 6,
                Name = "Country 6",
                Slug = "6",
                ISOCountryCode = "6",
                DFACountryCode = "6"
            };

            Region1.Countries.Add(country1);
            Region1.Countries.Add(country2);

            Region2.Countries.Add(country3);
            Region2.Countries.Add(country4);

            Region3.Countries.Add(country5);
            Region3.Countries.Add(country6);

            regionScheme.Regions.Add(Region1);
            regionScheme.Regions.Add(Region2);
            regionScheme.Regions.Add(Region3);

            return regionScheme;
        }
    }
}
