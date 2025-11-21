using CAFEPAY.ArqHex.Collects.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.application.CollectQueryByStatusAndWorkerCode
{
    public class CollectQueryByStatusAndWorkerCode
    {
        private readonly CollectRepository collectRepository;
        public CollectQueryByStatusAndWorkerCode(CollectRepository _collectRepository)
        {
            this.collectRepository = _collectRepository;
        }
        public List<Collect> execute(int isCountable, string workerCode, int status, long idPlot, long? idHarvest)
        {
            return collectRepository.queryByStatusAndWorkerCode(isCountable, workerCode, status, idPlot, idHarvest);
        }
    }
}
