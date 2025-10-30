using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Plots.Domain
{
    public class PlotStatus
    {
        public int statusValue { get; }
        public PlotStatus(int status)
        {
            statusValue = status;
        }
    }
}
