using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorName
    {
        private String collectorNameValue;
        public CollectorName(String _collectorNameValue) { 
            this.collectorNameValue = _collectorNameValue;
        }
        public String getValue() { 
            return this.collectorNameValue;
        }
    }
}
