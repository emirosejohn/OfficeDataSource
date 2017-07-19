namespace OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase
{
    public class OfficeDto
    {
        public int OfficeId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string Switchboard { get; set; }

        public string Fax { get; set; }

        public string TimeZone { get; set; }

        public int Operating { get; set; }
    }
}