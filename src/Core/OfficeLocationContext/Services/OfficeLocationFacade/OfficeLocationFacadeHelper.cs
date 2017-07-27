﻿using System;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.SharedContext.Services.OfficeLocationDatabase;

namespace OfficeLocationMicroservice.Core.OfficeLocationContext.Services.OfficeLocationFacade
{
    public static class OfficeLocationFacadeHelper
    {
        private const string CRLF = @"
";

        public static string GenerateInsertEmailSubject(OfficeLocation officeLocation)
        {
            var subject = "ACTION REQUIRED: New Office - "
                          + officeLocation.Name;
            return subject;
        }

        public static string GenerateUpdateEmailSubject(OfficeLocation officeLocation)
        {
            var subject = "ACTION REQUIRED: Office Data Source Update for the "
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

            string changedAddress = newOfficeLocation.Address.Replace(CRLF, "<br/>") + "<br/>" + newOfficeLocation.Country.Name;
            if (newOfficeLocation.Address != originalOfficeLocation.Address ||
                newOfficeLocation.Country != originalOfficeLocation.Country)
            {
                changedAddress = "<span style='color:red;font-weight:bold;'>" + newOfficeLocation.Address.Replace(CRLF, "<br/>")
                    + "<br/>" + newOfficeLocation.Country.Name + "</span>";
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

                    <table cellpadding='10'>
                        <tr>
                            <th></th>
                            <th><u>Old</u></th>
                            <th><u>Updated</u></th>
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

            var originalOfficeAddress = originalOfficeLocation.Address.Replace(CRLF, "<br/>") + "<br/>" + originalOfficeLocation.Country.Name;

            body = string.Format(body,
                originalOfficeLocation.Name,
                originalOfficeLocation.Name, changedName,
                originalOfficeAddress, changedAddress,
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

                    <table cellpadding='10'>
                        <tr>
                            <td>Office Name:</td>
                            <td style='color:red;font-weight:bold;'>{1}</td>
                        </tr>
                        <tr>
                            <td>Address: </td>
                            <td style='color:red;font-weight:bold;'>{2}</td>
                        </tr>
                        <tr>
                            <td>Phone: </td>
                            <td style='color:red;font-weight:bold;'>{3}</td>
                        </tr>
                        <tr>
                            <td>Fax: </td>
                            <td style='color:red;font-weight:bold;'>{4}</td>
                        </tr>
                        <tr>
                            <td>Operating Status: </td>
                            <td style='color:red;font-weight:bold;'>{5}</td>
                        </tr>
                    </table>
                        <br /><br />
                Please make sure that your system reflects this change, and that the proper employees are notified of the change. If you have any questions regarding this change, please reach out to the Corporate Services/Technology team for help.
                        <br /><br />
                Thanks! <br />
                        <br />
                ODS Team
             ";

            var officeAddress = officeLocation.Address.Replace(CRLF, "<br/>") + "<br/>" + officeLocation.Country.Name;

            body = string.Format(body,
                officeLocation.Name, officeLocation.Name,
                officeAddress, officeLocation.Switchboard,
                officeLocation.Fax, officeLocation.Operating);

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
                CountrySlug = officeLocation.Country.Slug,
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

//    public static class OfficeDtoExtensions
//    {
//        public static OfficeLocation ExtractOfficeLocation(this OfficeDto officeDto)
//        {
//            if (officeDto == null)
//            {
//                return null;
//            }
//
//            var officeLocation = new OfficeLocation()
//            {
//                OfficeId = officeDto.OfficeId,
//                Name = officeDto.Name,
//                Address = officeDto.Address,
//                Country = officeDto.Country,
//                Switchboard = officeDto.Switchboard,
//                Fax = officeDto.Fax,
//            };
//
//            switch (officeDto.Operating)
//            {
//                case 1:
//                    officeLocation.Operating = "Active";
//                    break;
//                case 0:
//                    officeLocation.Operating = "Closed";
//                    break;
//                default:
//                    throw new ArgumentOutOfRangeException();
//            }
//
//            return officeLocation;
//        }
//    }
}

