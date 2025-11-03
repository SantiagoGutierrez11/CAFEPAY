using CAFEPAY.ArqHex.Harvests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Application.HarvestAssociativeCollector
{
    public class HarvestAssociateCollector
    {
        private readonly HarvestRepository harvestRepository;

        public HarvestAssociateCollector(HarvestRepository _harvestRepository)
        {
            this.harvestRepository = _harvestRepository;
        }

        public long execute(long _idHarvest, long _idCollector)
        {
            long idAssigned = -1;

            return idAssigned = harvestRepository.associateCollector(_idHarvest, _idCollector);
        }
    }
}