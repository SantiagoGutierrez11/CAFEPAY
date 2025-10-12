using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorWorkerId
    {
        private String collectorWorkerId;
        public CollectorWorkerId(String _collectorWorkerId)
        {
            this.collectorWorkerId = _collectorWorkerId;
            validateFormat();
        }
        public String getValue()
        {
            return this.collectorWorkerId;
        }
        private void validateFormat()
        {
            if (this.collectorWorkerId.Length != 8)
            {
                throw new ArgumentException("El formato del WorkerId es incorrecto");
            }
        }
    }
}
