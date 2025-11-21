using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.domain
{
    public class PaymentDetailIdPayment
    {
        public long? idPaymentValue { get; }
        public PaymentDetailIdPayment(long? _idPaymentValue)
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
