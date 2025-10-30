using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.Serializers
{
    public class PlotMapper
    {
        public static List<PlotDTO> ToDTOList(List<Plot> plots)
        {
            if(plots == null)
            {
                return new List<PlotDTO>();
            }
            return plots.Select(plot => new PlotDTO
            {
                idPlot = plot.idPlot.idPlotValue,
                idOwner = plot.idOwner.idPlotOwnerValue,
                name = plot.name.plotNameValue,
                status = plot.status.statusValue,
                statusText = plot.status.statusValue == 1 ? "Activo" :
                             plot.status.statusValue == 2 ? "Inactivo" :
                             "Desconocido"



            }).ToList();
        }
    }
}
