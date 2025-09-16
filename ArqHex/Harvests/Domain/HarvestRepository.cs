using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    internal interface HarvestRepository
    {
        void save(Harvest harvest); // Save a new collector , update or insert if not exists
    }
}
