using CAFEPAY.ArqHex.Collectors.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class Collect
    {
        public CollectId id { get; }
        public CollectIdCollector collectorId { get; }
        public CollectIdPayment paymentId { get; }
        public CollectIdHarvest tHarvestId { get; }
        public CollectDate date { get; }
        public CollectedKilos kilos { get; }
        public CollectStatus status { get; }
        public CollectPaid paid { get; } 
        public CollectIsContable isContable { get; }
        public CollectIdPlot plotId { get; }

        public Collect(CollectId _collectId, CollectIdCollector _collectCollectorId, CollectIdPayment _collectPaymentId,
            CollectIdHarvest _collectHarvestId, CollectDate _collectDate, CollectedKilos _collectedKilos,
            CollectStatus collectStatus, CollectPaid collectPaid, CollectIdPlot collectIdPlot, CollectIsContable collectIsContable) 
        {
            this.id = _collectId;
            this.collectorId = _collectCollectorId;
            this.paymentId = _collectPaymentId;
            this.tHarvestId = _collectHarvestId;
            this.date = _collectDate;
            this.kilos = _collectedKilos;
            this.status = collectStatus;
            this.paid = collectPaid; 
            this.plotId = collectIdPlot;
            this.isContable = collectIsContable;
        }
    }
}