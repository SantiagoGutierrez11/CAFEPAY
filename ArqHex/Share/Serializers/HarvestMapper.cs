using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.Serializers
{
    public class HarvestMaper
    {
        public static List<HarvestDTO> ToDTOList(IReadOnlyList<Harvest> harvests)
        {
            if (harvests == null) return new List<HarvestDTO>();
            return harvests.Select(h => new HarvestDTO
            {
                id = h.id.idValue,
                idPlot = h.idPlot.idPlotValue,
                startDate = h.startDate.startDateValue,
                endDate = h.endDate?.endDateValue,
                pricePerKilo = h.pricePerKilo.pricePerKiloValue,
                status = h.status.statusValue,
                statusText = h.status.statusValue == 1 ? "En Proceso" :
                             h.status.statusValue == 2 ? "Finalizado" :
                             "Desconocido",
                plotName = AppServices.PlotServices.queryById.execute(h.idPlot.idPlotValue).name.plotNameValue

            }).ToList();
        }
    }
}