using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class HarvestIdPlot
    {
      public long idPlotValue { get; }
        public HarvestIdPlot(long _idPlotValue)
        {
            this.idPlotValue = _idPlotValue;
            ValidateFormat();
        }
        public void ValidateFormat()
        {
            if (this.idPlotValue <= 0)
            {
                throw new ArgumentException("Harvest id plot must be greater than zero");
            }
        }

    }
}
