using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    internal class HarvestId
    {
        public Decimal harvestIdValue;
        public HarvestId(Decimal _harvestIdValue) { 
            this.harvestIdValue = _harvestIdValue;
            validateFormat();
        }
        public void validateFormat()
        {
            if (this.harvestIdValue.ToString().Length > 10) // 10 digits max 
            {
                throw new ArgumentException("HarvestId cannot have more than 10 digits");
            }
            if (!Decimal.Truncate(this.harvestIdValue).Equals(this.harvestIdValue)) // No decimals allowed
            {
                throw new ArgumentException("HarvestId cannot have decimals");
            }
            if (!int.TryParse(harvestIdValue.ToString(), out int id) || id < 0) // Only positive integers allowed
            {
                throw new ArgumentException("HarvestId must be a positive integer");
            }
        }
        public Decimal getValue() { 
            return this.harvestIdValue;
        }
    }
}
