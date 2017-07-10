using System.Configuration;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.WebUi.Helpers
{
    public class WebSiteSettings : IOfficeLocationDatabaseSettings
    {
        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["OfficeLocationDatabase"].ConnectionString; }
        }
    }
}