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
        public CollectWorkerCode collectorWorkerCode { get; }
        public CollectIdHarvest harvestId { get; }
        public CollectDate date { get; }
        public CollectedKilos kilos { get; }
        public CollectStatus status { get; }
        public CollectorAmountToPaid amountToPaid { get; } 
        public CollectIsCountable isCountable { get; }
        public CollectIdPlot plotId { get; }

        public Collect(CollectId _collectId, CollectWorkerCode _collectWorkerCode,
            CollectIdHarvest _collecharvestId, CollectDate _collectDate, CollectedKilos _collectedKilos,
            CollectStatus collectStatus, CollectorAmountToPaid collectAmountToPaidValue, CollectIdPlot collectIdPlot, CollectIsCountable collectIscountable) 
        {
            this.id = _collectId;
            this.collectorWorkerCode = _collectWorkerCode;
            this.harvestId = _collecharvestId;
            this.date = _collectDate;
            this.kilos = _collectedKilos;
            this.status = collectStatus;
            this.amountToPaid = collectAmountToPaidValue; 
            this.plotId = collectIdPlot;
            this.isCountable = collectIscountable;
        }
    }
}