using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.domain
{
    public class PaymentWorkerCode
    {
        public string workerCodeValue { get; }
        public PaymentWorkerCode(string _workerCodeValue)
        {
            this.workerCodeValue = _workerCodeValue;
        }
    }
}
