namespace OfficeLocationMicroservice.Core.Domain.OfficeLocationContext
{
    public interface IOfficeLocationRepository
    {
        OfficeLocation GetByName(string name);

        OfficeLocation[] GetAll();

        OfficeLocation GetById(int id);
    }
}
