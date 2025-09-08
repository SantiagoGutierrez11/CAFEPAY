using CAFEPAY.ArqHex.Collectors.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.application.CollectorSave
{
    public class CollectorSave
    {
        private readonly CollectorRepository collectorRepository;
        public CollectorSave(CollectorRepository _collectorRepository) { 
            this.collectorRepository = _collectorRepository;
        }
        public void execute(decimal collectorId, string collectorName, Decimal collectorPhone, Boolean collectorStatus) { 
            CollectorId id = new CollectorId(collectorId);
            CollectorName name = new CollectorName(collectorName);
            CollectorPhone phone = new CollectorPhone(collectorPhone);
            CollectorStatus status = new CollectorStatus(collectorStatus);
            Collector collector = new Collector(id, name, phone, status);
            collectorRepository.save(collector);
        }
        
    }
}
