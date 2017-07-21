using System.Linq;
using OfficeLocationMicroservice.Core.Services.Email;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core.Domain.OfficeLocationContext
{
    public class OfficeLocationRepository
    {
        private readonly IOfficeDataTableGateway _officeDataTableGateway;
        private IEmailClient _client;

        public OfficeLocationRepository(
            IOfficeDataTableGateway officeGateway,
            IEmailClient client)
        {
            _officeDataTableGateway = officeGateway;
            _client = client;
        }

        public OfficeLocation GetByName(string name)
        {
            var officeDto = _officeDataTableGateway.GetByName(name);

            var office = officeDto.ExtractOfficeLocation();

            return office;
        }

        public OfficeLocation GetById(int id)
        {
            OfficeDto officeDto = _officeDataTableGateway.GetById(id);

            var office = officeDto.ExtractOfficeLocation();

            return office;
        }

        public OfficeLocation[] GetAll()
        {
            OfficeDto[] officeDtos = _officeDataTableGateway.GetAll();

            OfficeLocation[] officeLocations = new OfficeLocation[officeDtos.Length];

            for (int k = 0; k < officeDtos.Length; k++)
            {
                OfficeLocation office = GetById(officeDtos[k].OfficeId);
                officeLocations[k] = office;
            }

            return officeLocations;

        }

        public OfficeLocation Update(OfficeLocation changedOfficeLocation)
        {

            var offices = GetAll();

            var officeDto = changedOfficeLocation.ExtractDto();

            if (offices.All(x => x.OfficeId != changedOfficeLocation.OfficeId))
            {
                var id = _officeDataTableGateway.Insert(officeDto);

                changedOfficeLocation.OfficeId = id;
                SendInsertEmail(changedOfficeLocation);

            }
            else
            {
                var originalOfficeLocation = GetById(officeDto.OfficeId);
                _officeDataTableGateway.Update(officeDto);

                if (originalOfficeLocation != changedOfficeLocation)
                {
                    SendUpdateEmail(changedOfficeLocation, originalOfficeLocation);
                }
                
            }
            return changedOfficeLocation;
        }

        private void SendInsertEmail(
            OfficeLocation changedOfficeLocation)
        {
            var body = OfficeLocationRepositoryHelper.GenerateInsertEmailBody(
                changedOfficeLocation);

            var subject = OfficeLocationRepositoryHelper.GenerateInsertEmailSubject(
                changedOfficeLocation);

            _client.SendEmailMessage(body, subject);
        }

        private void SendUpdateEmail(
            OfficeLocation changedOfficeLocation,
            OfficeLocation originalOfficeLocation)
        {
            var body = OfficeLocationRepositoryHelper.GenerateUpdateEmailBody(
                changedOfficeLocation, originalOfficeLocation);

            var subject = OfficeLocationRepositoryHelper.GenerateUpdateEmailSubject(
                originalOfficeLocation);

            _client.SendEmailMessage(body, subject);
        }
    }
}
