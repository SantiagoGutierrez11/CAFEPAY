using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.Serializers
{
    public class CollectorMaper
    {
        public static List<CollectorDTO> ToDTOList(List<Collector> collectors)
        {
            if (collectors == null) return new List<CollectorDTO>();

            return collectors.Select(c => new CollectorDTO
            {
                workerCode = c.workerCode.collectorWorkerCode,                  
                id = c.id.collectorId,
                firstName = c.firstName.collectorFirstName,
                lastName = c.lastName.collectorLastName,
                phone = c.phone.collectorPhone,
                status = (c.status.collectorStatus == 1) ? "Activo" 
                : (c.status.collectorStatus == 2) ? "Inactivo"
                : "Desconosido"// 1=activo, 2=inactivo
            }).ToList();
        }
    }
}
