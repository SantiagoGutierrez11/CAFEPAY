using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetail.domain
{
    public class PaymentDetailId
    {
        public long? idValue { get; }
        public PaymentDetailId(long? _idValue)
        {
            this.idValue = _idValue;
            validateFormat();
        }
        public void validateFormat()
        {
        }
    }
}
