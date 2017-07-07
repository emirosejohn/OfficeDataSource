using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FluentAssertions;
using OfficeLocationMicroservice.Core.OfficeLocationContext;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;
using OfficeLocationMicroservice.Database.OfficeLocationDatabase;
using OfficeLocationMicroservice.WebUi.Controllers;
using OfficeLocationMicroservice.WebUi.Models;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class TestHelper
    {
        private readonly AllTablesDeleter _allTablesDeleter;
        private readonly OfficeDataTableGateway _officeDataTableGateway;

        public TestHelper()
        {
            var databaseSettings = new DatabaseSettings();
            var systemLogForIntegrationTests = new SystemLogForIntegrationTests();

            CurrentDate = new DateTime(2017, 2, 3);

            _allTablesDeleter = new AllTablesDeleter();

            _officeDataTableGateway = new OfficeDataTableGateway(databaseSettings, systemLogForIntegrationTests);
        }

        public OfficeLocationController CreateController()
        {
            var officeLocationRepository = new OfficeLocationRepository(_officeDataTableGateway);

            return new OfficeLocationController(officeLocationRepository);
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

            var databaseSettings = new DatabaseSettings();

            _allTablesDeleter.DeleteAllDataFromTables(
                databaseSettings.ConnectionString,
                tablesToSkip);

            act();

            _allTablesDeleter.DeleteAllDataFromTables(
                databaseSettings.ConnectionString, tablesToSkip);
        }

        public OfficeModel GetOfficeModelModelFromActionResult(
            ActionResult actionResult)
        {
            actionResult.Should().BeAssignableTo<ViewResult>();
            var viewResult = (ViewResult)actionResult;

            viewResult.Model.Should().BeAssignableTo<OfficeModel>();
            var officeModel = (OfficeModel)viewResult.Model;

            return officeModel;
        }

    }

}
