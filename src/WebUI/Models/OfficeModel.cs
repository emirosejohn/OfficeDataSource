using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeLocationMicroservice.Core;
using OfficeLocationMicroservice.Core.OfficeLocationContext;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class OfficeModel
    {
        public OfficeLocation[] Offices { get; set; }
    }
}