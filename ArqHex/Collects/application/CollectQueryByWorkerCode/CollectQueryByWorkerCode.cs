using CAFEPAY.ArqHex.Collects.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.application.CollectQueryByWorkerCode
{
    public class CollectQueryByWorkerCode
    {
        private CollectRepository collectRepository;
        public CollectQueryByWorkerCode(CollectRepository _collectRepository)
        {
            collectRepository = _collectRepository;
        }
        public List<Collect> execute(int isCountable, string workerCode, long idPlot, long? idHarvest)
        {
            return collectRepository.queryByWorkerCode(isCountable, workerCode, idPlot, idHarvest);
        }
    }
}
