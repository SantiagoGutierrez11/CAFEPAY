using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Plots.Domain
{
    public class PlotOwnerId
    {
        public long idPlotOwnerValue;
        public PlotOwnerId(long _idPlotOwnerValue)
        {
            idPlotOwnerValue = _idPlotOwnerValue;
            validateFormt();
        }
        public void validateFormt()
        {

        }
    }
}
