using CAFEPAY.ArqHex.Collectors.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.application.CollectorByIn
{
    public class CollectorQueryByIn
    {
        private readonly CollectorRepository collectorRepository;
        public  CollectorQueryByIn(CollectorRepository collectorRepository)
        {
            this.collectorRepository = collectorRepository;
        }
        public List<Collector> execute(string workerCodes)
        {
            return collectorRepository.queryByIn(workerCodes);
        }
    }
}
