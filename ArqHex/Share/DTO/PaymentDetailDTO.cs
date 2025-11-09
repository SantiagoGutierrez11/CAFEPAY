using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class PaymentsDetailsDTO
    {
        public long AmountToPay { get; set; }
        public long Id { get; set; }
        public long CollectId { get; set; }
        public long HarvestId { get; set; }
        public long PaymentId { get; set; }
        public long PlotId { get; set; }
        public string WorkerCode { get; set; }
    }
}