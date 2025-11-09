using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.domain
{
    public class PaymentsDetailsWorkerCode
    {
        public string Value { get; }
        public PaymentsDetailsWorkerCode(string value)
        {
            this.Value = value;
        }
    }
}