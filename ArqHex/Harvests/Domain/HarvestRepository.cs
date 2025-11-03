using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public interface HarvestRepository
    {
        long save(Harvest harvest); // Save a new collector , update or insert if not exists
        void update(Harvest harvest); // Update an existing collector
        List<Harvest> queryAll(); // Get all collectors
        long associateCollector(long idHarvest, long idCollector);

    }
}
