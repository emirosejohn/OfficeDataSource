namespace OfficeLocationMicroservice.Core
{
    public class OfficeDto
    {
        /*  never used
         *      public OfficeDto()
                {

                }
         *  only used for fake gateway testing
                public OfficeDto(string name, string address, )
                {

                }*/

        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Switchboard { get; set; }
        public string Fax { get; set; }
        public string TimeZone { get; set; }
        public bool Operating { get; set; }

    }
}