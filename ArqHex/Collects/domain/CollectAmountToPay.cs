using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectAmountToPaid
    {
        public decimal collectAmountToPaidValue { get; }

        public CollectAmountToPaid(decimal _collectAmountToPaidValueValue)
        {
            this.collectAmountToPaidValue = _collectAmountToPaidValueValue;
        }

    }
}