using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectIdPlot
    {
        public long collectIdPlot { get; }
        public CollectIdPlot(long _collectIdPlotValue)
        {
            ValidateFormat(_collectIdPlotValue);
            this.collectIdPlot = _collectIdPlotValue;
        }
        private void ValidateFormat(long idValue)
        {
            // Validar que no sea negativo
            if (idValue < 0)
            {
                throw new ArgumentException("El ID de parcela no puede ser negativo");
            }
            // Validar que no sea cero
            if (idValue == 0)
            {
                throw new ArgumentException("El ID del lote no puede ser cero");
            }
        }
    }
}
