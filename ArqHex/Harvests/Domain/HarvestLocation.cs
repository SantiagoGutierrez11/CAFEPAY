using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    internal class HarvestLocation
    {
        public string harvestLocationValue;

        public HarvestLocation(string _harvestLocationValue)
        {
            this.harvestLocationValue = _harvestLocationValue;
            ValidateFormat();
        }

        private void ValidateFormat()
        {
            if (string.IsNullOrWhiteSpace(this.harvestLocationValue))
            {
                throw new ArgumentException("Harvest location cannot be null or empty");
            }

            if (this.harvestLocationValue.Length > 100) 
            {
                throw new ArgumentException("Harvest location cannot exceed 100 characters");
            }
        }

        public string GetValue()
        {
            return this.harvestLocationValue;
        }
    }
}
