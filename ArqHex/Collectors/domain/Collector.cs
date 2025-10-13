using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class Collector
    {
        public CollectorWorkerCode workerCode { get; }
        public CollectorId id { get; }
        public CollectorFirstName firstName { get; }
        public CollectorLastName lastName { get; }
        public CollectorPhone phone { get; }
        public CollectorStatus status { get; }
        public Collector(CollectorWorkerCode _collectorWorkerCode, CollectorId _collectorId, CollectorFirstName _collectorFirstName,
                        CollectorLastName _collectorLastName, CollectorPhone _collectorPhone, CollectorStatus _collectorStatus)
        {
            this.workerCode = _collectorWorkerCode; // atributos
            this.id = _collectorId;
            this.firstName = _collectorFirstName;
            this.lastName = _collectorLastName;
            this.phone = _collectorPhone;
            this.status = _collectorStatus;
        }
    }
}
