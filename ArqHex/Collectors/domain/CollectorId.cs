using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorId
    {
        private Decimal collectorIdValue;
        public CollectorId(Decimal _collectorIdValue) { 
            this.collectorIdValue = _collectorIdValue;
            validateFormat();
        }
        public void validateFormat()
        {
            if (this.collectorIdValue.ToString().Length > 10) // 10 digits max 
            {
                throw new ArgumentException("CollectorId cannot have more than 10 digits");
            }
            if (!Decimal.Truncate(this.collectorIdValue).Equals(this.collectorIdValue)) // No decimals allowed
            {
                throw new ArgumentException("CollectorId cannot have decimals");
            }
            if (!int.TryParse(collectorIdValue.ToString(), out int id) || id < 0) // Only positive integers allowed
            {
                throw new ArgumentException("CollectorId must be a positive integer");
            }
        }
        public Decimal getValue() { 
            return this.collectorIdValue;
        }
    }
}
