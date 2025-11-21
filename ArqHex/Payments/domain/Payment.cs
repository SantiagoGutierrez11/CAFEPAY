using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.domain
{
    public class Payment
    {
        public PaymentId id { get; }
        public PaymentDate date { get; }    
        public PaymentWorkerCode workerCode { get; }
        public Payment(PaymentId id, PaymentDate date, PaymentWorkerCode workerCode)
        {
            this.id = id;
            this.date = date;
            this.workerCode = workerCode;
        }
    }
}
