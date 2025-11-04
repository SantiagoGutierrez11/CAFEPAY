using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class CollectDTO
    {
        public long collectId { get; set; }
        public long collectorId { get; set; }
        public long harvestId { get; set; }
        public long paymentId { get; set; }
        public DateTime collectDate { get; set; }
        public decimal collectedKilos { get; set; }
        public long paid { get; set; }  
        public int status { get; set; }         // 1 = activo , 2 = inactivo
        public string statustext { get; set; }

    }
}