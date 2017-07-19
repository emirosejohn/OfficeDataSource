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
                changedName = "<span style='color:red;font-weight:bold;'>" + newOfficeLocation.Name + "</span>";
            }

            string changedAddress = newOfficeLocation.Address;
            if (newOfficeLocation.Address != originalOfficeLocation.Address)
            {
                changedAddress = "<span style='color:red;font-weight:bold;'>" + newOfficeLocation.Address + "</span>";
            }

            string changedSwitchboard = newOfficeLocation.Switchboard;
            if (newOfficeLocation.Switchboard != originalOfficeLocation.Switchboard)
            {
                changedSwitchboard = "<span style='color:red;font-weight:bold;'>" + newOfficeLocation.Switchboard + "</span>";
            }

            string changedFax = newOfficeLocation.Fax;
            if (newOfficeLocation.Fax != originalOfficeLocation.Fax)
            {
                changedFax = "<span style='color:red;font-weight:bold;'>" + newOfficeLocation.Fax + "</span>";
            }

            string changedOperating = newOfficeLocation.Operating;
            if (newOfficeLocation.Operating != originalOfficeLocation.Operating)
            {
                changedOperating = "<span style='color:red;font-weight:bold;'>" + newOfficeLocation.Operating + "</span>";
            }

            string body = @"
                Hello, <br />
                       <br />
                The following update was just made to the Office Data Source system:  <br />
                       <br />
                Office: {0} <br />

                    <table>
                        <tr>
                            <th></th>
                            <th><u>old</u></th>
                            <th><u>updated</u></th>
                        </tr>
                        <tr>
                            <td>Office Name:</td>
                            <td>{1}</td>
                            <td>{2}</td>
                        </tr>
                        <tr>
                            <td>Address: </td>
                            <td>{3}</td>
                            <td>{4}</td>
                        </tr>
                        <tr>
                            <td>Phone: </td>
                            <td>{5}</td>
                            <td>{6}</td>
                        </tr>
                        <tr>
                            <td>Fax: </td>
                            <td>{7}</td>
                            <td>{8}</td>
                        </tr>
                        <tr>
                            <td>Operating Status: </td>
                            <td>{9}</td>
                            <td>{10}</td>
                        </tr>
                    </table>

                        <br /><br>
                Please make sure that your system reflects this change, and that the proper employees are notified of the change. If you have any questions regarding this change, please reach out to the Corporate Services/Technology team for help.
                        <br /><br />
                Thanks! <br />
                        <br />
                ODS Team
             ";

            body = string.Format(
                body, originalOfficeLocation.Name,
                originalOfficeLocation.Name, changedName,
                originalOfficeLocation.Address, changedAddress,
                originalOfficeLocation.Switchboard, changedSwitchboard,
                originalOfficeLocation.Fax, changedFax,
                originalOfficeLocation.Operating, changedOperating);

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

                    <table>
                        <tr>
                            <th></th>
                            <th><u>old</u></th>
                            <th><u>updated</u></th>
                        </tr>
                        <tr>
                            <td>Office Name:</td>
                            <td>{1}</td>
                            <td>{2}</td>
                        </tr>
                        <tr>
                            <td>Address: </td>
                            <td>{3}</td>
                            <td>{4}</td>
                        </tr>
                        <tr>
                            <td>Phone: </td>
                            <td>{5}</td>
                            <td>{6}</td>
                        </tr>
                        <tr>
                            <td>Fax: </td>
                            <td>{7}</td>
                            <td>{8}</td>
                        </tr>
                        <tr>
                            <td>Operating Status: </td>
                            <td>{9}</td>
                            <td>{10}</td>
                        </tr>
                    </table>

                        <br /><br />
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

