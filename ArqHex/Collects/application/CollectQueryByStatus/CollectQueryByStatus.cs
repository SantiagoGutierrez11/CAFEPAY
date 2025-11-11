using CAFEPAY.ArqHex.Collects.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.application.CollectQueryByStatus
{
    public class CollectQueryByStatus
    {
        private readonly CollectRepository collectRepository;
        public CollectQueryByStatus(CollectRepository collectRepository)
        {
            this.collectRepository = collectRepository;
        }
        public List<Collect> execute (int isCountable, int status, long idPlot, long idHarvest)
        {
            return collectRepository.queryByStatus(isCountable, status, idPlot, idHarvest);   
        }
    }
}
