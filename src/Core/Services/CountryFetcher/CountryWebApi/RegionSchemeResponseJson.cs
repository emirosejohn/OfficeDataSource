using System.Collections.Generic;

namespace OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi
{

    public class RegionSchemeResponseJson
    {
        public int SchemeId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<Region> Regions { get; set; }
    }

    public class Region
    {
        public int RegionId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public List<Country> Countries { get; set; }
    }

    public class Country
    {
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string ISOCountryCode { get; set; }
        public string DFACountryCode { get; set; }
    }
}
