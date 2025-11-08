using CAFEPAY.ArqHex.Collects.domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.Serializers
{
    public class CollectMaper
    {
        public static List<CollectDTO> ToDTOList(List<Collect> collects)
        {
            if (collects == null) return new List<CollectDTO>();

            return collects.Select(c => new CollectDTO
            {
                collectId = c.id.collectId,
                collectorWorkerCode = c.collectorWorkerCode.collectorWorkerCode,
                plotId = c.plotId.collectIdPlot,
                harvestId = c.harvestId.collectIdHarvest,
                collectDate = c.date.collectDate,
                collectedKilos = c.kilos.collectedKilos,
                status = c.status.collectStatus,
                statusText = c.status.collectStatus == 0 ? "Zero" :
                             c.status.collectStatus == 1 ? "Registrado" :
                             c.status.collectStatus == 2 ? "Pagado"
                                : "Inactivo",
            }).ToList();
        }
    }
}