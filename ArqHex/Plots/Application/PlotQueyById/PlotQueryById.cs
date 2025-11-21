using CAFEPAY.ArqHex.Plots.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Plots.Application.PlotQueyById
{
    public class PlotQueryById
    {
        private readonly PlotRepository _plotRepository;
        public PlotQueryById(PlotRepository plotRepository)
        {
            _plotRepository = plotRepository;
        }
        public Plot execute(long idPlot)
        {
            // Lógica para consultar el Plot por su ID
            var plot = _plotRepository.queryById(idPlot);
            return plot;
        }
    }
}
