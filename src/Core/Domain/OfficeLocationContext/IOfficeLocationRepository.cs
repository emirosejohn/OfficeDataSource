namespace OfficeLocationMicroservice.Core.Domain.OfficeLocationContext
{
    public interface IOfficeLocationRepository
    {
        OfficeLocation GetByName(string name);

        OfficeLocation[] GetAll();

        void Update(OfficeLocation editedOfficeLocation);
        OfficeLocation GetById(int id);
    }
}
