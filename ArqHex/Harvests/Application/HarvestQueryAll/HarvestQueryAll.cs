using CAFEPAY.ArqHex.Harvests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Application.HarvestQueryAll
{
    public class HarvestQueryAll
    {
        private readonly HarvestRepository harvestRepository;

        public HarvestQueryAll(HarvestRepository harvestRepository)
        {
            this.harvestRepository = harvestRepository;
        }

        public Dictionary<HarvestId, Harvest> execute()
        {
            return this.harvestRepository.queryAll();
        }
    }
}
