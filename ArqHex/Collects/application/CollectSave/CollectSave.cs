using CAFEPAY.ArqHex.Collects.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.application.CollectSave
{
    public class CollectSave
    {
        private readonly CollectRepository collectRepository;
        public CollectSave(CollectRepository _collectRepository){
            this.collectRepository = _collectRepository;
        }

        public void execute(long? _collectId, string _collectWorkerCode, DateTime _collectDate,
                           decimal _collectedKilos, long? _collectIdHarvest, 
                           int _collectStatus, decimal? _collectAmountToPaidValue, long _collectIdPlot, int _collectIsCountable){
            CollectId collectId = new CollectId(_collectId);
            CollectWorkerCode collectWorkerCode = new CollectWorkerCode(_collectWorkerCode);
            CollectDate collectDate = new CollectDate(_collectDate);
            CollectedKilos collectedKilos = new CollectedKilos(_collectedKilos);
            CollectIdHarvest collectIdHarvest = new CollectIdHarvest(_collectIdHarvest);
            CollectStatus collectStatus = new CollectStatus(_collectStatus);
            CollectAmountToPaid collectAmountToPaidValue = new CollectAmountToPaid(_collectAmountToPaidValue);
            CollectIsCountable collectIscountable = new CollectIsCountable(_collectIsCountable);
            CollectIdPlot collectIdPlot = new CollectIdPlot(_collectIdPlot);

            Collect collect = new Collect(collectId, collectWorkerCode,
                                          collectIdHarvest, collectDate, collectedKilos, collectStatus, collectAmountToPaidValue,
                                          collectIdPlot, collectIscountable);
            collectRepository.save(collect);
        }
    }
}