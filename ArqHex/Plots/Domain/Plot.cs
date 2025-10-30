using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Plots.Domain
{
    public class Plot
    {
        public PlotId idPlot { get; }
        public PlotOwnerId idOwner { get; }
        public PlotName name { get; }
        public PlotStatus status { get; }
        public Plot(PlotId _idPlot, PlotOwnerId _idOwner, PlotName _name, PlotStatus _status)
        {
            idPlot = _idPlot;
            idOwner = _idOwner;
            name = _name;
            status = _status;
        }
    }
}
