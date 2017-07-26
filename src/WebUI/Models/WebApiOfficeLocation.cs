using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain;
using OfficeLocationMicroservice.Core.OfficeLocationContext.Domain.CountryRepository;

namespace OfficeLocationMicroservice.WebUi.Models
{
    
    /// <summary>
    /// 
    /// </summary>
    public class OfficeLocation 
    {
        /// <summary>
        /// 
        /// </summary>
        public OfficeLocation(){ }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="officeLocation"></param>
        public OfficeLocation(Core.OfficeLocationContext.Domain.OfficeLocation officeLocation)
        {
            this.OfficeId = officeLocation.OfficeId;
            this.Address = officeLocation.Address;
            this.Country = officeLocation.Country;
            this.Fax = officeLocation.Fax;
            this.Name = officeLocation.Name;
            this.Operating = officeLocation.Operating;
            this.Switchboard = officeLocation.Switchboard;
        }

        /// <summary>
        /// Generated Id identifying the office
        /// </summary>
        [Required]
        public int OfficeId { get; set; }

        /// <summary>
        /// Name of the city where the office is located
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Address information about the office
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Country information of the office
        /// </summary>
        [Required]
        public Country Country { get; set; }

        /// <summary>
        /// Phone number used to contact the office
        /// </summary>
        [Required]
        public string Switchboard { get; set; }

        /// <summary>
        /// Fax number used to contact the office
        /// </summary>
        /// [DataMember]
        public string Fax { get; set; }

        /// <summary>
        /// Operating status of the office
        /// </summary>
        [Required]
        public string Operating { get; set; }

    }
}