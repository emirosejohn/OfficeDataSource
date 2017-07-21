using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;

namespace OfficeLocationMicroservice.Core.Domain.CountryContext
{
    public class CountryRepository
    {
        private readonly ICountryWebApiGateway _countryWebApiGateway;


        public CountryRepository(ICountryWebApiGateway countryWebApiGateway)
        {
            _countryWebApiGateway = countryWebApiGateway;
        }

        public Country[] GetAllCountries()
        {
            var regionScheme = _countryWebApiGateway.GetRegionScheme();

            var listOfRegions = regionScheme.Regions;

            var countries = new List<Country>();

            countries.Add(new Country());

            foreach (var region in listOfRegions)
            {
                foreach (var country in region.Countries)
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

    public static class CountryExtensions
    {
        public static IEnumerable<SelectListItem> AsEnumerable(this Country[] countries)
        {
            return countries.Select((country) => 
                new SelectListItem { Text = country.Name, Value = country.Name });
        }
    }
}