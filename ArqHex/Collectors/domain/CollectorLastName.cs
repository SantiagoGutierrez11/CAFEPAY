using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorLastName
    {
        public string collectorLastName { get;}
        public CollectorLastName(string _collectorLastName)
        {
            this.collectorLastName = _collectorLastName;
            validateFormat();
        }
        public void validateFormat()
        {
        }
    }
}