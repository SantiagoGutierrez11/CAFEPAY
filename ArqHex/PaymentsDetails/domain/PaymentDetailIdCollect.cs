using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.domain
{
    public class PaymentsDetailsIdCollect
    {
        public long idCollectValue { get; }
        public PaymentsDetailsIdCollect(long _idCollectValue)
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
