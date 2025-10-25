using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorWorkerCode
    {
        public string collectorWorkerCode { get; } // Hace que el atributo sea de solo lectura y no se pueda modificar
        public CollectorWorkerCode(string _collectorWorkerCode)
        {
            this.collectorWorkerCode = _collectorWorkerCode;
            validateFormat();
        }
        private void validateFormat()
        {
     
        }
    }
}
