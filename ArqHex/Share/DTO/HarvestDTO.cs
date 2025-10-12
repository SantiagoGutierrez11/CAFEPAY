using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class HarvestDTO
    {
        public decimal Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerKilo { get; set; }
        public string Location { get; set; }
    }
}
