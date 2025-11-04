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

        public void execute(long? _collectId, string _collectCollectorWorkerId, DateTime _collectDate,
                           decimal _collectedKilos, long _collectIdHarvest, long _collectIdPayment,
                           int _collectStatus, long _collectorAmountToPaid, long _collectIdPlot, int _collectIsContable){
            CollectId collectId = new CollectId(_collectId);
            CollectWorkerCode collectCollectorId = new CollectWorkerCode(_collectCollectorWorkerId);
            CollectDate collectDate = new CollectDate(_collectDate);
            CollectedKilos collectedKilos = new CollectedKilos(_collectedKilos);
            CollectIdHarvest collectIdHarvest = new CollectIdHarvest(_collectIdHarvest);
            CollectIdPayment collectIdPayment = new CollectIdPayment(_collectIdPayment);
            CollectStatus collectStatus = new CollectStatus(_collectStatus);
            CollectorAmountToPaid collectorAmountToPaid = new CollectorAmountToPaid(_collectorAmountToPaid);
            CollectIsContable collectIsContable = new CollectIsContable(_collectIsContable);
            CollectIdPlot collectIdPlot = new CollectIdPlot(_collectIdPlot);

            Collect collect = new Collect(collectId, collectCollectorId, collectIdPayment,
                                          collectIdHarvest, collectDate, collectedKilos, collectStatus, collectorAmountToPaid,
                                          collectIdPlot, collectIsContable);
            collectRepository.save(collect);
        }
    }
}