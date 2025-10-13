using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorFirstName
    {
        public string collectorFirstName;
        public CollectorFirstName(string _collectorFirstName)
        {
            this.collectorFirstName = _collectorFirstName;
            validateFormat();
        }
        public void validateFormat()
        {
        }

    }
}

