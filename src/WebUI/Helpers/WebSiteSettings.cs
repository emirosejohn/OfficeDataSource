using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.WebUi.Helpers
{
    public class WebSiteSettings : IDatabaseSettings
    {
        public string ConnectionString
        {
            get
            {
                return
                    "Initial Catalog=OfficeLocationMicroservice;Data Source=(local)\\sqlexpress;Integrated Security=SSPI;Connection Timeout=0;";
            }
        }
    }
}