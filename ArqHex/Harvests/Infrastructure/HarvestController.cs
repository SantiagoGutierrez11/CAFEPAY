using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Infrastucture
{
    internal class HarvestController
    {
        public void saveHarvest(Decimal harvestId, String harvestLocation, Decimal harvestPricePerKilo, DateTime harvestStartDate, DateTime harvestEndDate){
            ServiceContainer.Harvest.save.execute(harvestId, harvestLocation, harvestPricePerKilo, harvestStartDate, harvestEndDate);
        }
        public Dictionary<HarvestId, Harvest> listHarvests(){
            return ServiceContainer.Harvest.query.execute();
        }
    }
}
