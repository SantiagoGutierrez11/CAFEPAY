using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectIdPayment
    {
        public long? collectIdPayment { get; }

        public CollectIdPayment(long? _collectIdPaymentValue)
        {
            this.collectIdPayment = _collectIdPaymentValue;
        }
    }
}