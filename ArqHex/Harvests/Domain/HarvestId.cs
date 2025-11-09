using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class HarvestId
    {
        public long? idValue { get; }
        public HarvestId(long? _idValue) { 
            this.idValue = _idValue;
            validateFormat();
        }
        public void validateFormat()
        {
        }
    }
}
