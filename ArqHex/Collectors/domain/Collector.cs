using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class Collector
    {
        private CollectorWorkerId workerId;
        private CollectorId id;
        private CollectorFirstName firstName;
        private CollectorLastName lastName;
        private CollectorPhone phone;
        private CollectorStatus status;
        public Collector(CollectorWorkerId _collectorWorkerId, CollectorId _collectorId, CollectorFirstName _collectorFirstName,
                        CollectorLastName _collectorLastName, CollectorPhone _collectorPhone, CollectorStatus _collectorStatus)
        {
            this.workerId = _collectorWorkerId; // atributos
            this.id = _collectorId;
            this.firstName = _collectorFirstName;
            this.lastName = _collectorLastName;
            this.phone = _collectorPhone;
            this.status = _collectorStatus;
        }

    }
}
