using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorPhone
    {
        public long collectorPhone { get; }
        public CollectorPhone(long _collectorPhoneValue)
        {
            this.collectorPhone = _collectorPhoneValue;
            validateFormat();
        }
        public void validateFormat()
        {

        }
    }
}