using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class Harvest
    {
        private HarvestId Id;
        private HarvestStartDate StartDate;
        private HarvestEndDate EndDate;
        private HarvestPricePerKilo PricePerKilo;
        private HarvestLocation Location;
        public Harvest(
            HarvestId id,
            HarvestStartDate startDate,
            HarvestEndDate endDate,
            HarvestPricePerKilo pricePerKilo,
            HarvestLocation location)
        {
            this.Id = id;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.PricePerKilo = pricePerKilo;
            this.Location = location;
        }

        public decimal getId() {
            return this.Id.getValue();
        }
        public DateTime getStartDate() { 
            return this.StartDate.getValue(); 
        }
        public DateTime getEndDate() {
            return this.EndDate.getValue(); 
        }
        public decimal getPricePerKilo() {
            return this.PricePerKilo.getValue(); 
        }
        public string getLocation() {
            return this.Location.getValue(); 
        }
    }
}