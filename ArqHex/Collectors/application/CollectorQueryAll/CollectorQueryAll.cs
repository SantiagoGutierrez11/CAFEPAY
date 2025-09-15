using CAFEPAY.ArqHex.Collectors.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.application.CollectorQueryAll
{
    public class CollectorQueryAll
    {
        private readonly CollectorRepository collectorRepository;
        public CollectorQueryAll(CollectorRepository _collectorRepository) { 
            this.collectorRepository = _collectorRepository;
        }
        public Dictionary<CollectorId, Collector> execute() { 
            return this.collectorRepository.queryAll();
        }
    }
}
