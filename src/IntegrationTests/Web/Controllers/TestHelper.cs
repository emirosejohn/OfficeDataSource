using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Database;
using OfficeLocationMicroservice.WebUi;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    public class TestHelper
    {
        private readonly OfficeDataTableGateway _officeDataTableGateway;

        public TestHelper()
        {
            var databaseSettings = new DatabaseSettings();
        }
    }
}
