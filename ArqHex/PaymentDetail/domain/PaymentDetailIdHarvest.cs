using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetail.domain
{
    public class PaymentDetailIdHarvest
    {
        public long idHarvestValue { get; }
        public PaymentDetailIdHarvest(long _idHarvestValue)
        {
            this.idHarvestValue = _idHarvestValue;
            ValidateFormat();
        }
        public void ValidateFormat()
        {
            if (this.idHarvestValue <= 0)
            {
                throw new ArgumentException("Payment detail id harvest must be greater than zero");
            }
        }
    }
}
