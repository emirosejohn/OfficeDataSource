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
        
    }
}
