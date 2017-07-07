namespace OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase
{
    public interface IOfficeDataTableGateway
    {
        OfficeDto GetByName(string name);
        OfficeDto[] GetAll();
    }
}
