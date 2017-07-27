using System.Linq;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade.Email;

namespace OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade
{
    public class OfficeLocationFacade
    {
        private readonly IEmailClient _client;
        private readonly OfficeLocationRepository _officeLocationRepository;

        public OfficeLocationFacade(
            OfficeLocationRepository officeLocationRepository,
            IEmailClient client)
        {
            _officeLocationRepository = officeLocationRepository;
            _client = client;
        }

        public OfficeLocation GetById(int id)
        {
            return _officeLocationRepository.GetById(id);
        }

        public OfficeLocation[] GetAll()
        {
            return _officeLocationRepository.GetAll();
        }

        public Country[] GetAllCountries()
        {
            return _officeLocationRepository.GetAllCountries();
        }

        public OfficeLocation Update(OfficeLocation changedOfficeLocation)
        {

            var offices = GetAll();

            if (offices.All(x => x.OfficeId != changedOfficeLocation.OfficeId))
            {
                var id = _officeLocationRepository.Insert(changedOfficeLocation);

                changedOfficeLocation.OfficeId = id;
                SendInsertEmail(changedOfficeLocation);
            }
            else
            {
                var originalOfficeLocation = GetById(changedOfficeLocation.OfficeId);
                _officeLocationRepository.Update(changedOfficeLocation);

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
            var body = OfficeLocationFacadeHelper.GenerateInsertEmailBody(
                changedOfficeLocation);

            var subject = OfficeLocationFacadeHelper.GenerateInsertEmailSubject(
                changedOfficeLocation);

            _client.SendEmailMessage(body, subject);
        }

        private void SendUpdateEmail(
            OfficeLocation changedOfficeLocation,
            OfficeLocation originalOfficeLocation)
        {
            var body = OfficeLocationFacadeHelper.GenerateUpdateEmailBody(
                changedOfficeLocation, originalOfficeLocation);

            var subject = OfficeLocationFacadeHelper.GenerateUpdateEmailSubject(
                originalOfficeLocation);

            _client.SendEmailMessage(body, subject);
        }
    }
}
