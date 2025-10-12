using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorLastName
    {
        private String collectorLastName;
        public CollectorLastName(String _collectorLastName)
        {
            this.collectorLastName = _collectorLastName;
        }
        public String getValue()
        {
            return this.collectorLastName;
        }
    }
}