using CAFEPAY.ArqHex.Harvests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Application.HarvestSave
{
    internal class HarvestSave
    {
        private readonly HarvestRepository harvestRepository;
        public HarvestSave(HarvestRepository _harvestRepository) { 
            this.harvestRepository = _harvestRepository;
        }
        public void Execute(decimal harvestId, DateTime harvestStartDate,DateTime harvestEndDate, decimal harvestPricePerKilo, string harvestLocation)
        {
            HarvestId id = new HarvestId(harvestId);
            HarvestStartDate startDate = new HarvestStartDate(harvestStartDate);
            HarvestEndDate endDate = new HarvestEndDate(harvestEndDate);
            HarvestPricePerKilo pricePerKilo = new HarvestPricePerKilo(harvestPricePerKilo);
            HarvestLocation location = new HarvestLocation(harvestLocation);
            Domain.Harvest harvest = new Domain.Harvest(id, startDate, endDate, pricePerKilo, location);
            harvestRepository.save(harvest);
        }

    }
}
