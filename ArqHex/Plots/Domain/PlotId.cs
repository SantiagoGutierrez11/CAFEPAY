using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Plots.Domain
{
    public class PlotId
    {
        public long idPlotValue;
        public PlotId(long _idPlotValue)
        {
            idPlotValue = _idPlotValue;
            validateFormt();
        }
        public void validateFormt()
        {
            if (idPlotValue <= 0)
            {
                throw new ArgumentException("El ID del lote debe ser un número positivo.");
            }
        }

    }
}
