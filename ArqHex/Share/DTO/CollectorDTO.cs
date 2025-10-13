using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class CollectorDTO
    {
        public string workerCode { get; set; }   
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public string status { get; set; }         // 1 = activo , 2 = inactivo

    }
}
