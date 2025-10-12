using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorStatus
    {
        private decimal collectorStatusValue;
        public CollectorStatus(decimal _collectorStatusValue)
        {
            this.collectorStatusValue = _collectorStatusValue;
        }
        public decimal getValue()
        {
            return this.collectorStatusValue;
        }
    }
}