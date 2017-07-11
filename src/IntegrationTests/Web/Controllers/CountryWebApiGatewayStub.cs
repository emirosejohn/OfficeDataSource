using System.Collections.Generic;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;

namespace OfficeLocationMicroservice.IntegrationTests.Web.Controllers
{
    internal class CountryWebApiGatewayStub : ICountryWebApiGateway
    {
        public RegionSchemeResponseJson GetRegionScheme()
        {
            var regionScheme = new RegionSchemeResponseJson();

            regionScheme.Regions = new List<Region>();

            var region1 = new Region()
            {
                RegionId = 1,
                Name = "Region1",
                Slug = "R1",
                Countries = new List<Country>()
            };

            var region2 = new Region()
            {
                RegionId = 1,
                Name = "Region2",
                Slug = "R2",
                Countries = new List<Country>()
            };

            var region3 = new Region()
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

            region1.Countries.Add(country1);
            region1.Countries.Add(country2);

            region2.Countries.Add(country3);
            region2.Countries.Add(country4);

            region3.Countries.Add(country5);
            region3.Countries.Add(country6);

            regionScheme.Regions.Add(region1);
            regionScheme.Regions.Add(region2);
            regionScheme.Regions.Add(region3);

            return regionScheme;
        }
    }
}