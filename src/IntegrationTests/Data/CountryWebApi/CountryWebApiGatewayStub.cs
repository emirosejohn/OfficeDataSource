using System.Collections.Generic;
using OfficeLocationMicroservice.Core.SharedContext.Services.CountryWebApi;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    internal class CountryWebApiGatewayStub : ICountryWebApiGateway
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
                Slug = "C1",
                ISOCountryCode = "1",
                DFACountryCode = "1"
            };

            var country2 = new Country()
            {
                CountryId = 2,
                Name = "Country 2",
                Slug = "C2",
                ISOCountryCode = "2",
                DFACountryCode = "2"
            };

            var country3 = new Country()
            {
                CountryId = 3,
                Name = "Country 3",
                Slug = "C3",
                ISOCountryCode = "3",
                DFACountryCode = "3"
            };

            var country4 = new Country()
            {
                CountryId = 4,
                Name = "Country 4",
                Slug = "C4",
                ISOCountryCode = "4",
                DFACountryCode = "4"
            };

            var country5 = new Country()
            {
                CountryId = 5,
                Name = "Country 5",
                Slug = "C5",
                ISOCountryCode = "5",
                DFACountryCode = "5"
            };

            var country6 = new Country()
            {
                CountryId = 6,
                Name = "Country 6",
                Slug = "C6",
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