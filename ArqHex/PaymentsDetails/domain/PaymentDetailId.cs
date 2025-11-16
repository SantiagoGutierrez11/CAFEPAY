using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.domain
{
    public class PaymentsDetailsId
    {
        public long? idValue { get; }
        public PaymentsDetailsId(long? _idValue)
        {
            this.idValue = _idValue;
            validateFormat();
        }
        public void validateFormat()
        {
        }
    }
}
