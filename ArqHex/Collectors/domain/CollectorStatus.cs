using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorStatus
    {
        private Boolean collectorStatusValue;
        public CollectorStatus(Boolean _collectorStatusValue) { 
            this.collectorStatusValue = _collectorStatusValue;
        }
        public Boolean getValue() { 
            return this.collectorStatusValue;
        }
    }
}
