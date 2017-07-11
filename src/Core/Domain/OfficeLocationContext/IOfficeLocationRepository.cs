namespace OfficeLocationMicroservice.Core.OfficeLocationContext
{
    public interface IOfficeLocationRepository
    {
        OfficeLocation GetByName(string name);

        OfficeLocation[] GetAll();
    }
}
