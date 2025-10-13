
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorId
    {
        public string collectorId { get; } // Hace que el atributo sea de solo lectura y no se pueda modificar
        public CollectorId(string _collectorIdValue)
        {
            this.collectorId = _collectorIdValue;
            validateFormat();
        }
        public void validateFormat()
        {
           
        }
    }
}
