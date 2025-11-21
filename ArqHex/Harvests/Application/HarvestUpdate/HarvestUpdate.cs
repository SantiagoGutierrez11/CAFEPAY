using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Application.HarvestUpdate
{
    public class HarvestUpdate
    {
        private readonly HarvestRepository harvestRepository;
        public HarvestUpdate(HarvestRepository _harvestRepository)
        {
            this.harvestRepository = _harvestRepository;
        }
        public void execute(long? _idHarvest, long _idPlot, DateTime _startDate, DateTime? _endDate, decimal _pricePerKilo, int _status)
        {
            HarvestId idHarvest = new HarvestId(_idHarvest);
            HarvestIdPlot idPlot = new HarvestIdPlot(_idPlot);
            HarvestStartDate startDate = new HarvestStartDate(_startDate);
            HarvestPricePerKilo pricePerKilo = new HarvestPricePerKilo(_pricePerKilo);
            HarvestStatus status = new HarvestStatus(_status);
            HarvestEndDate endDate = new HarvestEndDate(_endDate);
            Harvest harvest = new Harvest(idHarvest, idPlot, startDate, pricePerKilo, status, endDate);
            harvestRepository.update(harvest);
        }

    }
}
