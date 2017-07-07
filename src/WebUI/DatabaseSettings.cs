using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeLocationMicroservice.Core;

namespace OfficeLocationMicroservice.WebUi
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString
        {
            get
            {
                return
                    "Initial Catalog=OfficeLocationMicroservice;Data Source=(local)\\sqlexpress;Integrated Security=SSPI;Connection Timeout=0;";
            }
        }
    }
}