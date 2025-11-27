using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.domain
{
    public class HarvestActiveExistsException : Exception
    {
        public long IdPlot { get; }
        public HarvestActiveExistsException(long idPlot, Exception inner = null)
            : base($"El lote {idPlot} ya tiene una cosecha activa.", inner) => IdPlot = idPlot;
    }
}
