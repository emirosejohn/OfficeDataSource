using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class WebApiOfficeLocation : OfficeLocation
    {
        [Required]
        public int WebOfficeId
        {
            get { return OfficeId; }
        }
    }
}