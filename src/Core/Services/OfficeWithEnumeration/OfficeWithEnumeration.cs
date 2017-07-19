using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OfficeLocationMicroservice.Core.Domain.CountryContext;
using OfficeLocationMicroservice.Core.Domain.OfficeLocationContext;

namespace OfficeLocationMicroservice.Core.Services.OfficeWithEnumeration
{
    public class OfficeWithEnumeration
    {
        public OfficeLocation Office { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SelectListItem> OperatingOptions { get; set; }
        public string HasChanged { get; set; }

        public OfficeWithEnumeration()
        {
            Office = new OfficeLocation();
            Countries = new List<SelectListItem>();
            OperatingOptions = new List<SelectListItem>();
            HasChanged = "false";
        }

        public OfficeWithEnumeration(
            OfficeLocation office,
            Country[] countries)
        {
            Office = office;
            Countries = new SelectList(countries, "Name", "Name", office.Country);
            OperatingOptions = GenerateOperatingOptions(office.Operating);
        }

        public OfficeWithEnumeration(
            OfficeLocation office,
            Country[] countries,
            string hasChanged
        )
        {
            Office = office;
            Countries = new SelectList(countries, "Name", "Name", office.Country);
            OperatingOptions = GenerateOperatingOptions(office.Operating);
            HasChanged = hasChanged;
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

        public void SetHasChanged(string hasChanged)
        {
            HasChanged = hasChanged;
        }
    }
}