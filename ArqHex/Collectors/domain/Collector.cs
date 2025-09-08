using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    internal class Collector
    {
        public CollectorId Id;
        public CollectorName Name;
        public CollectorPhone Phone;
        public CollectorStatus Status;
        public Collector(CollectorId _collectorId, CollectorName _collectorName, 
                        CollectorPhone _collectorPhone, CollectorStatus _collectorStatus)
        {
            this.Id = _collectorId;
            this.Name = _collectorName;
            this.Phone = _collectorPhone;
            this.Status = _collectorStatus;
        }

        public void calculatePayment()
        {
            
        }
    }
}
