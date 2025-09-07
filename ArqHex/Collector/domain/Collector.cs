using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collector.domain
{
    internal class Collector
    {
        private CollectorId collectorId;
        private CollectorName collectorName;
        private CollectorPhone collectorPhone;
        private CollectorStatus collectorStatus;
        public Collector(CollectorId _collectorId, CollectorName _collectorName, 
                        CollectorPhone _collectorPhone, CollectorStatus _collectorStatus)
        {
            this.collectorId = _collectorId;
            this.collectorName = _collectorName;
            this.collectorPhone = _collectorPhone;
            this.collectorStatus = _collectorStatus;
        }

        public void calculatePayment()
        {
            
        }
    }
}
