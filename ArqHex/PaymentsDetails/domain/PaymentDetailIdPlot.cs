using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.domain
{
    public class PaymentsDetailsIdPlot
    {
        public long idPlotValue { get; }
        public PaymentsDetailsIdPlot(long _idPlotValue)
        {
            this.idPlotValue = _idPlotValue;
            ValidateFormat();
        }
        public void ValidateFormat()
        {
            if (this.idPlotValue <= 0)
            {
                throw new ArgumentException("Payment detail id plot must be greater than zero");
            }
        }
    }
}
