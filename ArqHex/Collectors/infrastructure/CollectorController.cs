using CAFEPAY.ArqHex.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.infrastructure
{
    internal class CollectorController
    {
       public void saveCollector(Decimal collectorId, String collectorName, Decimal collectorPhone, Boolean collectorStatus) { 
            ServiceContainer.Collector.save.execute(collectorId, collectorName, collectorPhone, collectorStatus);
        }
    }
}
