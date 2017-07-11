using System.Collections.Generic;
using System.Linq;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;

namespace OfficeLocationMicroservice.Core.Domain.CountryContext
{
    public class CountryRepository
    {
        private ICountryWebApiGateway _countryWebApiGateway;


        public CountryRepository(ICountryWebApiGateway countryWebApiGateway)
        {
            _countryWebApiGateway = countryWebApiGateway;
        }

        public Country[] GetAllCountries()
        {
            var RegionScheme = _countryWebApiGateway.GetRegionScheme();

            var ListOfRegions = RegionScheme.Regions;

            var countries = new List<Country>();

            foreach (var Region in ListOfRegions)
            {
                foreach (var country in Region.Countries)
                {
                    countries.Add(new Country()
                    {
                        CountryId = country.CountryId,
                        Name = country.Name,
                    });
                }
            }
            return countries.ToArray();
        }
    }
}