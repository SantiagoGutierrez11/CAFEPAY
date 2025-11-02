using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Plots.Domain
{
    public interface PlotRepository
    {
        List<Plot> queryAll();
        Plot queryById(long idPlot);
    }
}
