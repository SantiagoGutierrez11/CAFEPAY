using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetail.domain
{
    public class PaymentDetailWorkerCode
    {
        public string paymentDetailWorkerCode { get; }
        public PaymentDetailWorkerCode(string _paymentDetailWorkerCode)
        {
            this.paymentDetailWorkerCode = _paymentDetailWorkerCode;
        }
    }
}
