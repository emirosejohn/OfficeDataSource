using OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi;

namespace OfficeLocationMicroservice.Data.CountryWebApi
{
    public class CountryWebApiGateway : ICountryWebApiGateway
    {
        private readonly ICountryWebApiSettings _countryWebApiSettings;
        private readonly WebApiServiceCaller _webApiServiceCaller;

        public CountryWebApiGateway(
            ICountryWebApiSettings countryWebApiSettings,
            WebApiServiceCaller webApiServiceCaller)
        {
            _countryWebApiSettings = countryWebApiSettings;
            _webApiServiceCaller = webApiServiceCaller;
        }

        public RegionSchemeResponseJson GetRegionScheme()
        {
            var url = _countryWebApiSettings.CountryWebApiUrl + "api/regionScheme?regionSchemeSlug=mfr-reporting";

              //  "***REMOVED***/api/RegionScheme?regionSchemeSlug=public-site";

            _webApiServiceCaller.GetDataFromJsonUrl<RegionSchemeResponseJson>(url);

            return _webApiServiceCaller.GetDataFromJsonUrl<RegionSchemeResponseJson>(url);
        }

    }
}
