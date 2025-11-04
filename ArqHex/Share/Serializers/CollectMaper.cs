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
                collectorId = c.collectorId.collectIdCollector,
                paymentId = c.paymentId.collectIdPayment,
                harvestId = c.tHarvestId.collectIdHarvest,
                collectDate = c.date.collectDate,
                collectedKilos = c.kilos.collectedKilos,
                status = c.status.collectStatus,
                paid = c.paid.collectPaid
            }).ToList();
        }
    }
}