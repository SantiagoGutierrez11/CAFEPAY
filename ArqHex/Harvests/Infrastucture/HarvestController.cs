using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Infrastucture
{
    internal class HarvestController
    {
        public void saveHarvest(decimal harvestId, DateTime harvestStartDate, DateTime harvestEndDate, decimal harvestPricePerKilo, string harvestLocation)
        {
            ServiceContainer.Harvest.save.execute(harvestId, harvestStartDate, harvestEndDate, harvestPricePerKilo, harvestLocation);
        }
    }
}
