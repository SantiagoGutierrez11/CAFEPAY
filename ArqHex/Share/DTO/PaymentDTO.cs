using System;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class PaymentDTO
    {
        public long? Id { get; set; }
        public DateTime Date { get; set; }
        public string WorkerCode { get; set; }

        public decimal TotalAmount { get; set; }
    }
}