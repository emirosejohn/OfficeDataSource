using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;

namespace OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase
{
    public interface IOfficeDataTableGateway
    {

        OfficeDto GetByName(string name);

        OfficeDto[] GetAll();

        void Insert(OfficeDto dto);

        void Update(OfficeDto dto);

        OfficeDto GetById(int id);

    }
}
