using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorStatus
    {
        public int collectorStatus { get; } // Hace que el atributo sea de solo lectura y no se pueda modificar
        public CollectorStatus(int _collectorStatusValue)
        {
            this.collectorStatus = _collectorStatusValue;
            validateFormat();
        }
        private void validateFormat()
        {
        }
    }
}