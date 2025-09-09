using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    internal class Harvest
    {
        public HarvestId Id;
        public HarvestStartDate StartDate;
        public HarvestEndDate EndDate;
        public HarvestPricePerKilo PricePerKilo;
        public HarvestLocation Location;

        public Harvest(HarvestId _harvestId, HarvestStartDate _harvestStartDate, HarvestEndDate _harvestEndDate,
            HarvestPricePerKilo _harvestPricePerKilo, HarvestLocation _harvestLocation)
        {
            this.Id = _harvestId;
            this.StartDate = _harvestStartDate;
            this.EndDate = _harvestEndDate;
            this.PricePerKilo = _harvestPricePerKilo;
            this.Location = _harvestLocation;
        }
        public void recordHarvest()
        {

        }
    }
}
