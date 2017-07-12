namespace OfficeLocationMicroservice.Core.SharedContext.OfficeLocationDatabase
{
    public interface IOfficeDataTableGateway
    {
        OfficeDto GetByName(string name);
        OfficeDto[] GetAll();
        void Insert(OfficeDto officeDto);

        OfficeDto GetById(int id);
    }
}
