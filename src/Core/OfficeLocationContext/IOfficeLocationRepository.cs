using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeLocationMicroservice.Core.OfficeLocationContext;

namespace OfficeLocationMicroservice.Core
{
    public interface IOfficeLocationRepository
    {
        OfficeLocation GetByName(string name);
        OfficeLocation[] GetAll();
    }
}
