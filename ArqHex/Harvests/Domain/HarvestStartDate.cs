using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class HarvestStartDate
    {
        public DateTime startDateValue { get; }

        public HarvestStartDate(DateTime _startDateValue)
        {
            this.startDateValue = _startDateValue;
            // validateFormat(); // ← COMENTA O ELIMINA ESTA LLAMADA
        }

        // public void validateFormat() // ← COMENTA O ELIMINA ESTE MÉTODO COMPLETO
        // {
        //     DateTime currentDate = DateTime.Now;
        //     if (this.startDateValue > currentDate)
        //     {
        //         throw new ArgumentException("Harvest start date cannot be in the future");
        //     }
        // }
    }
}