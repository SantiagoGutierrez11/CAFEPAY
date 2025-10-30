using CAFEPAY.ArqHex.Harvests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Application.HarvestSave
{
    public class HarvestSave
    {
        private readonly HarvestRepository harvestRepository;
        public HarvestSave(HarvestRepository _harvestRepository)
        {
            this.harvestRepository = _harvestRepository;
        }
        public long execute(long _idPlot, DateTime _startDate, DateTime? _endDate, decimal _pricePerKilo, int _status)
        {
            long idAssigned = -1;
            HarvestIdPlot idPlot = new HarvestIdPlot(_idPlot);
			HarvestStartDate startDate = new HarvestStartDate(_startDate);
            HarvestEndDate endDate = new HarvestEndDate(_endDate);
            HarvestPricePerKilo pricePerKilo = new HarvestPricePerKilo(_pricePerKilo);
            HarvestStatus status = new HarvestStatus(_status);
            Harvest harvest = new Harvest(null, idPlot, startDate, pricePerKilo, status, null);
            return idAssigned = harvestRepository.save(harvest);
        }
    }
}