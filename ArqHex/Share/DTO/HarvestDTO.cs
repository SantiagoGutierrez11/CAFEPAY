using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class HarvestDTO
    {
        public long? id { get; set; }
        public long idPlot { get; set; }
        public DateTime startDate { get; set; }
        public DateTime? endDate { get; set; }
        public decimal pricePerKilo { get; set; }
        public int status { get; set; }
        public string statusText { get; set; }
    }
}
