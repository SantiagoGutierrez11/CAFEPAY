using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collector.domain
{
    internal class CollectorPhone
    {
        private decimal collectorPhoneValue;
        public CollectorPhone(decimal _collectorPhoneValue) { 
            this.collectorPhoneValue = _collectorPhoneValue;
            validateFormat();
        }
        public void validateFormat()
        {
            if (this.collectorPhoneValue.ToString().Length > 10) // 10 digits max 
            {
                throw new ArgumentException("CollectorId cannot have more than 10 digits");
            }
            if (!Decimal.Truncate(this.collectorPhoneValue).Equals(this.collectorPhoneValue)) // No decimals allowed
            {
                throw new ArgumentException("CollectorId cannot have decimals");
            }
            if (!int.TryParse(collectorPhoneValue.ToString(), out int id) || id < 0) // Only positive integers allowed
            {
                throw new ArgumentException("CollectorId must be a positive integer");
            }
        }
    }
}
