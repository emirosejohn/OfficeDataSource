using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OfficeLocationMicroservice.IntegrationTests.Web.Controllers;
using Xunit;

namespace OfficeLocationMicroservice.IntegrationTests.Email
{
    public class EmailTests
    {
        public class TestHelper
        {
            private readonly EmailClientFake _emailClient;

            public TestHelper()
            {
                var databaseSettings = new DataConnectionStringsForIntegrationTests();

                _emailClient = new EmailClientFake(databaseSettings);
            }

            public EmailClientFake GetEmailClient()
            {
                return _emailClient;
            }
        }

        [Fact]
        public void ShouldReturnMessageWhenSending()
        {
            var testhelper = new TestHelper();
            var client = testhelper.GetEmailClient();

            var message = client.SendEmailMessage("test body", "test subject");

            message.Body.Should().Be("test body");
            message.Subject.Should().Be("test subject");
            message.From.Should().Be("testFrom@dimensional.com");
            message.To.First().Should().Be("testTo@dimensional.com");

        }
    }
}
