namespace OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase
{
    public interface IOfficeDataTableGateway
    {

        OfficeDto GetByName(string name);

        OfficeDto[] GetAll();

        int Insert(OfficeDto dto);

        void Update(OfficeDto dto);

        OfficeDto GetById(int id);

    }
}
