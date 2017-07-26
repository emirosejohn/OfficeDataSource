using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;

namespace OfficeLocationMicroservice.WebUi.Models
{
    public class Office
    {
        public Office(Core.OfficeLocationContext.Domain.Office office)
        {
            this.OfficeId = office.OfficeId;
            this.Address = office.Address;
            this.Country = office.Country;
            this.Fax = office.Fax;
            this.Name = office.Name;
            this.Operating = office.Operating;
            this.Switchboard = office.Switchboard;
        }

        /// <summary>
        /// Generated Id for the Office
        /// </summary>
        [Required]
        public int OfficeId { get; set; }

        /// <summary>
        /// city Name of the Office
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Address of the Office
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Country of the office
        /// </summary>
        [Required]
        public Country Country { get; set; }

        /// <summary>
        /// Phone number of the office
        /// </summary>
        [Required]
        public string Switchboard { get; set; }
        
        /// <summary>
        /// Fax number of the office
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Operating status of the office
        /// </summary>
        [Required]
        public string Operating { get; set; }
    }


}