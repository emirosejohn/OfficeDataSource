using System;
using System.Collections.Generic;
using System.Web.Mvc;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;

namespace OfficeLocationMicroservice.Core.Services.OfficeWithEnumeration
{
    public class OfficeWithEnumeration
    {
        public OfficeLocation Office { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> Timezones { get; set; }
        public IEnumerable<SelectListItem> OperatingOptions { get; set; }

        public OfficeWithEnumeration(
            OfficeLocation office,
            Country[] countries,
            TimeZone[] timezones 
            )
        {
            Office = office;
            Countries = new SelectList(countries, "Name", "Name", office.Country);
            OperatingOptions = GenerateOperatingOptions(office.Operating);
            Timezones = null;
        }

        public static IEnumerable<SelectListItem> GenerateOperatingOptions(string operating)
        {
            var selectedItems = new List<SelectListItem>();
            selectedItems.Add(new SelectListItem()
            {
                Value = "Active",
                Text = "Active",
                Selected= (operating=="Active")
            });
            selectedItems.Add(new SelectListItem()
            {
                Value = "Closed",
                Text = "Closed",
                Selected = (operating == "Closed")
            });

            return selectedItems;
        }
    }
}