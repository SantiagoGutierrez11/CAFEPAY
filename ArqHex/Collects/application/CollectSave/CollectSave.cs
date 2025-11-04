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

        public void execute(long _collectId, long _collectCollectorId, DateTime _collectDate,
                           decimal _collectedKilos, long _collectIdHarvest, long _collectIdPayment,
                           int _collectStatus, long _collectPaid, long _collectIdPlot, int _collectIsContable){
            CollectId collectId = new CollectId(_collectId);
            CollectIdCollector collectCollectorId = new CollectIdCollector(_collectCollectorId);
            CollectDate collectDate = new CollectDate(_collectDate);
            CollectedKilos collectedKilos = new CollectedKilos(_collectedKilos);
            CollectIdHarvest collectIdHarvest = new CollectIdHarvest(_collectIdHarvest);
            CollectIdPayment collectIdPayment = new CollectIdPayment(_collectIdPayment);
            CollectStatus collectStatus = new CollectStatus(_collectStatus);
            CollectPaid collectPaid = new CollectPaid(_collectPaid);
            CollectIsContable collectIsContable = new CollectIsContable(_collectIsContable);
            CollectIdPlot collectIdPlot = new CollectIdPlot(_collectIdPlot);

            Collect collect = new Collect(collectId, collectCollectorId, collectIdPayment,
                                          collectIdHarvest, collectDate, collectedKilos, collectStatus, collectPaid,
                                          collectIdPlot, collectIsContable);
            collectRepository.save(collect);
        }
    }
}