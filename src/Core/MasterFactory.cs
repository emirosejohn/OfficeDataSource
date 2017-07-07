using OfficeLocationMicroservice.Core.OfficeLocationContext;
using OfficeLocationMicroservice.Core.SharedContext;
using OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core
{
    public static class MasterFactory
    {
        public static ISystemLog SystemLog { get; set; }
        public static IOfficeDataTableGateway OfficeDataTableGateway { get; set; }


        public static OfficeLocationRepository GetOfficeLocationRepository()
        {
            return new OfficeLocationRepository(OfficeDataTableGateway);
        }
    }
}