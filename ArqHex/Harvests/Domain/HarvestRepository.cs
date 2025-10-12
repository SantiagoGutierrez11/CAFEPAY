using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public interface HarvestRepository
    {
        void save(CAFEPAY.Harvest harvest); // Save a new collector , update or insert if not exists
        Dictionary<HarvestId, CAFEPAY.Harvest> queryAll();
    }
}
