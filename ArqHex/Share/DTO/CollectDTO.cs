using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class CollectDTO
    {
        public long? collectId { get; set; }
        public string collectorWorkerCode { get; set; }
        public long plotId { get; set; }
        public long? harvestId { get; set; }
        public DateTime collectDate { get; set; }
        public decimal collectedKilos { get; set; }
        public long amountToPaid { get; set; }  
        public int status { get; set; }         // 1 = activo , 2 = inactivo
        public string statusText { get; set; }

        public int isCountable { get; set; }

    }
}