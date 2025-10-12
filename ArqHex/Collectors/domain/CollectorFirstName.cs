using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorFirstName
    {
        private String collectorFirstName;
        public CollectorFirstName(String _collectorFirstName)
        {
            this.collectorFirstName = _collectorFirstName;
        }
        public String getValue()
        {
            return this.collectorFirstName;
        }
    }
}

