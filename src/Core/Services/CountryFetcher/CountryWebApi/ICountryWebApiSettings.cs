using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeLocationMicroservice.Core.Services.CountryFetcher.CountryWebApi
{
    public interface ICountryWebApiSettings
    {
        string CountryWebApiUrl { get; }

    }
}
