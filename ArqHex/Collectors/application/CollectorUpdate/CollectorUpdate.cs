using CAFEPAY.ArqHex.Collectors.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.application.CollectorUpdate
{
    public class CollectorUpdate
    {
        private readonly CollectorRepository collectorRepository;
        public CollectorUpdate(CollectorRepository _collectorRepository)
        {
            this.collectorRepository = _collectorRepository;
        }
        public void execute(long _oldId, string _collectorWorkerCode, long _collectorId, string _collectorFirstName,
                            string _collectorLastName, long _collectorPhone, int _collectorStatus)
        {
            CollectorWorkerCode collectorWorkerCode = new CollectorWorkerCode(_collectorWorkerCode);
            CollectorId id = new CollectorId(_collectorId);
            CollectorFirstName firstName = new CollectorFirstName(_collectorFirstName);
            CollectorLastName lastName = new CollectorLastName(_collectorLastName);
            CollectorPhone phone = new CollectorPhone(_collectorPhone);
            CollectorStatus status = new CollectorStatus(_collectorStatus);
            Collector collector = new Collector(collectorWorkerCode, id, firstName, lastName, phone, status);
            collectorRepository.update(collector,_oldId);
        }

    }
}
