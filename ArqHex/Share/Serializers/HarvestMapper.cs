using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.Serializers
{
    public class HarvestMaper
    {
        public static List<HarvestDTO> ToDTOList(IReadOnlyList<Harvest> harvests)
        {
            if (harvests == null) return new List<HarvestDTO>();
            return harvests.Select(c => new HarvestDTO
            {
                Id = c.getId(),
                StartDate = c.getStartDate(),
                EndDate = c.getEndDate(),
                PricePerKilo = c.getPricePerKilo(),
                Location = c.getLocation()
            }).ToList();
        }
    }
}