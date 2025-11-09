using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetail.domain
{
    public class PaymentDetailIdCollect
    {
        public long idCollectValue { get; }
        public PaymentDetailIdCollect(long _idCollectValue)
        {
            this.idCollectValue = _idCollectValue;
            ValidateFormat();
        }
        public void ValidateFormat()
        {
            if (this.idCollectValue <= 0)
            {
                throw new ArgumentException("Payment detail id collect must be greater than zero");
            }
        }
    }
}
