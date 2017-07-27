namespace OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository
{
    public class Country
    {
        public Country(){ }

        public string Name { get; set; }
        public string Slug { get; set; }

        public static bool operator ==(Country x, Country y)
        {
            if ((object) x == null && (object) y == null) //checks for nulls
            {
                return true;
            }
            else if ((object) x == null || (object) y == null)
            {
                return false;
            }

            return (
                x.Name == y.Name &&
                x.Slug == y.Slug);
        }

        public static bool operator !=(Country x, Country y)
        {
            return !(x == y);
        }
    }
}



