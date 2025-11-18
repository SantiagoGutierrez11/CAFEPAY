using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.domain
{
    public class PaymentDetailWorkerCode
    {
        public string Value { get; }
        public PaymentDetailWorkerCode(string value)
        {
            this.Value = value;
        }
    }
}