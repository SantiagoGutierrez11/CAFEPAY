using System;
using CAFEPAY.ArqHex.Plots.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Plots.Application.PlotQueryAll
{
    public class PlotQueryAll
    {
        private readonly PlotRepository plotRepository;
        public PlotQueryAll(PlotRepository _plotRepository)
        {
            this.plotRepository = _plotRepository;
        }
        public List<Plot> execute()
        {
            return this.plotRepository.queryAll();
        }
    }
}
