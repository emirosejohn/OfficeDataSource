﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeLocationMicroservice.Core
{
    public interface IOfficeLocationRepository
    {
        OfficeLocation GetByName(string name);
        OfficeLocation[] GetAll();
    }
}