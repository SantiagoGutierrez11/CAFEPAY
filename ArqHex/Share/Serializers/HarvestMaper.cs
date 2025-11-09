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

            var result = new List<HarvestDTO>();

            foreach (var h in harvests)
            {
                try
                {
                    var plot = AppServices.PlotServices.queryById.execute(h.idPlot.idPlotValue);
                    var plotName = plot?.name?.plotNameValue ?? $"Lote {h.idPlot.idPlotValue}";
                    var harvestNumber = h.id?.idValue ?? 0;

                    result.Add(new HarvestDTO
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
                        plotName = $"{plotName} - Cosecha {harvestNumber}"
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error mapeando cosecha ID {h.id?.idValue}: {ex.Message}");
                }
            }

            return result;
        }
    }
}