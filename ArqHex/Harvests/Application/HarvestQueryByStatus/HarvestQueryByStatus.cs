using CAFEPAY.ArqHex.Harvests.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Application.HarvestQueryByStatus
{
    public class HarvestQueryByStatus
    {
        private readonly HarvestRepository _harvestRepository;

        public HarvestQueryByStatus(HarvestRepository harvestRepository)
        {
            _harvestRepository = harvestRepository;
        }

        public List<Harvest> execute(int status)
        {
            // Obtener todas las cosechas
            var allHarvests = _harvestRepository.queryAll();

            // Filtrar por status
            var harvestsByStatus = allHarvests
                .Where(h => h.status.statusValue == status)
                .ToList();

            return harvestsByStatus;
        }
    }
}