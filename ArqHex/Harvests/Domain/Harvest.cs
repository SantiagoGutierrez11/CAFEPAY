using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class Harvest
    {
        public HarvestId id { get; }
        public HarvestIdPlot idPlot { get; }
        public HarvestStartDate startDate { get; }
        public HarvestPricePerKilo pricePerKilo { get; }
        public HarvestStatus status { get; }
        public HarvestEndDate endDate { get; }

        public Harvest(HarvestId _id, HarvestIdPlot _idPlot, HarvestStartDate _startDate, HarvestPricePerKilo _pricePerKilo, HarvestStatus _status, HarvestEndDate _endDate =null)
        {
            this.id = _id;
            this.idPlot = _idPlot;
            this.startDate = _startDate;
            this.pricePerKilo = _pricePerKilo;
            this.status = _status;
            this.endDate = _endDate;
        }
    }
}