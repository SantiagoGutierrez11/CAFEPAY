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
        public CollectWorkerCode collectorId { get; }
        public CollectIdPayment paymentId { get; }
        public CollectIdHarvest harvestId { get; }
        public CollectDate date { get; }
        public CollectedKilos kilos { get; }
        public CollectStatus status { get; }
        public CollectorAmountToPaid paid { get; } 
        public CollectIsCountable iscountable { get; }
        public CollectIdPlot plotId { get; }

        public Collect(CollectId _collectId, CollectWorkerCode _collectCollectorId, CollectIdPayment _collectPaymentId,
            CollectIdHarvest _collecharvestId, CollectDate _collectDate, CollectedKilos _collectedKilos,
            CollectStatus collectStatus, CollectorAmountToPaid collectPaid, CollectIdPlot collectIdPlot, CollectIsCountable collectIscountable) 
        {
            this.id = _collectId;
            this.collectorId = _collectCollectorId;
            this.paymentId = _collectPaymentId;
            this.harvestId = _collecharvestId;
            this.date = _collectDate;
            this.kilos = _collectedKilos;
            this.status = collectStatus;
            this.paid = collectPaid; 
            this.plotId = collectIdPlot;
            this.iscountable = collectIscountable;
        }
    }
}