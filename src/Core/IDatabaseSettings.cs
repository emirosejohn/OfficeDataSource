using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeLocationMicroservice.Core
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; }
    }
}
