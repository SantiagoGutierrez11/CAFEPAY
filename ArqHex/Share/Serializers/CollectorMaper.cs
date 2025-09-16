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
        public static List<CollectorDTO> ToDTOList(Dictionary<CollectorId, Collector> collectors)
        {
            if (collectors == null) return new List<CollectorDTO>();
            return collectors.Values.Select(c => new CollectorDTO
            {
                Id = c.Id.getValue(),
                Name = c.Name.getValue(),
                Phone = c.Phone.getValue(),
                Status = c.Status.getValue()
            }).ToList();
        }
    }
}
