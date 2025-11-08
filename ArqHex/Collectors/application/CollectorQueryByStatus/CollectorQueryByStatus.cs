using CAFEPAY.ArqHex.Collectors.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.application.CollectorQueryByStatus
{
    public class CollectorQueryByStatus
    {
        private readonly domain.CollectorRepository collectorRepository;
        public CollectorQueryByStatus(CollectorRepository _collectorRepository)
        {
            collectorRepository = _collectorRepository;
        }
        public List<Collector> execute(int status)
        {
            return collectorRepository.queryByStatus(status);
        }
    }
}
