using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core.Services.SharedContext.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core.Domain.OfficeLocationContext
{
    public static class OfficeLocationRepositoryHelper
    {
        public static string GenerateInsertEmailSubject(OfficeLocation officeLocation)
        {
            var subject = "ACTION REQUIRED: New Office Data Source Created for the "
                          + officeLocation.Name + " Office";
            return subject;
        }

        public static string GenerateUpdateEmailSubject(OfficeLocation officeLocation)
        {
            var subject = "ACTION REQUIRED: New Office Data Source Update for the "
                          + officeLocation.Name + " Office";
            return subject;
        }

        public static string GenerateUpdateEmailBody(OfficeLocation newOfficeLocation,
            OfficeLocation originalOfficeLocation)
        {
            string changedName = newOfficeLocation.Name;
            if (newOfficeLocation.Name != originalOfficeLocation.Name)
            {
                changedName = "<font color='red'>" + newOfficeLocation.Name + "</font>";
            }

            string changedAddress = newOfficeLocation.Address;
            if (newOfficeLocation.Address != originalOfficeLocation.Address)
            {
                changedAddress = "<font color='red'>" + newOfficeLocation.Address + "</font>";
            }

            string changedSwitchboard = newOfficeLocation.Switchboard;
            if (newOfficeLocation.Switchboard != originalOfficeLocation.Switchboard)
            {
                changedSwitchboard = "<font color='red'>" + newOfficeLocation.Switchboard + "</font>";
            }

            string changedFax = newOfficeLocation.Fax;
            if (newOfficeLocation.Fax != originalOfficeLocation.Fax)
            {
                changedFax = "<font color='red'>" + newOfficeLocation.Fax + "</font>";
            }

            string body = @"
                Hello, <br />
                       <br />
                The following update was just made to the Office Data Source system:  <br />
                       <br />
                Office: {0} <br />
                        <br />
                {1}     <br />
                {2}     <br />
                {3}     <br />
                {4}     <br />
                        <br />
                Please make sure that your system reflects this change, and that the proper employees are notified of the change. If you have any questions regarding this change, please reach out to the Corporate Services/Technology team for help.
                        <br /><br />
                Thanks! <br />
                        <br />
                ODS Team
             ";

            body = string.Format(body, changedName, changedName,
                changedAddress, changedSwitchboard, changedFax);

            return body;
        }

        public static string GenerateInsertEmailBody(OfficeLocation officeLocation)
        {
            string body = @"
                Hello, <br />
                       <br />
                The following update was just made to the Office Data Source system:  <br />
                       <br />
                Office: {0} <br />
                        <br />
                {1}     <br />
                {2}     <br />
                {3}     <br />
                {4}     <br />
                        <br />
                Please make sure that your system reflects this change, and that the proper employees are notified of the change. If you have any questions regarding this change, please reach out to the Corporate Services/Technology team for help.
                        <br /><br />
                Thanks! <br />
                        <br />
                ODS Team
             ";

            body = string.Format(body, officeLocation.Name, officeLocation.Name,
                officeLocation.Name, officeLocation.Name, officeLocation);

            return body;
        }
    }

    public static class OfficeLocationExtensions
    {
        public static OfficeDto ExtractDto(this OfficeLocation officeLocation)
        {
            if (officeLocation == null)
            {
                return null;
            }

            var officeDto = new OfficeDto()
            {
                OfficeId = officeLocation.OfficeId,
                Name = officeLocation.Name,
                Address = officeLocation.Address,
                Country = officeLocation.Country,
                Switchboard = officeLocation.Switchboard,
                Fax = officeLocation.Fax,
                TimeZone = officeLocation.TimeZone,
            };

            switch (officeLocation.Operating)
            {
                case "Active":
                    officeDto.Operating = 1;
                    break;
                case "Closed":
                    officeDto.Operating = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return officeDto;
        }
    }

    public static class OfficeDtoExtensions
    {
        public static OfficeLocation ExtractOfficeLocation(this OfficeDto officeDto)
        {
            if (officeDto == null)
            {
                return null;
            }

            var officeLocation = new OfficeLocation()
            {
                OfficeId = officeDto.OfficeId,
                Name = officeDto.Name,
                Address = officeDto.Address,
                Country = officeDto.Country,
                Switchboard = officeDto.Switchboard,
                Fax = officeDto.Fax,
                TimeZone = officeDto.TimeZone,
            };

            switch (officeDto.Operating)
            {
                case 1:
                    officeLocation.Operating = "Active";
                    break;
                case 0:
                    officeLocation.Operating = "Closed";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return officeLocation;
        }
    }
}

