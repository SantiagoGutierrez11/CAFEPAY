using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.domain
{
    public class PaymentsDetailsIdPayment
    {
        public long idPaymentValue { get; }
        public PaymentsDetailsIdPayment(long _idPaymentValue)
        {
            this.idPaymentValue = _idPaymentValue;
            ValidateFormat();
        }
        public void ValidateFormat()
        {
            if (this.idPaymentValue <= 0)
            {
                throw new ArgumentException("Payment detail id payment must be greater than zero");
            }
        }
    }
}
