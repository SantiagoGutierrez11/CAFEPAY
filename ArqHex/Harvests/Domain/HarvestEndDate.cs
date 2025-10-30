using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class HarvestEndDate
    {
        public DateTime? endDateValue { get; }

        public HarvestEndDate(DateTime? _endDateValue)
        {
            this.endDateValue = _endDateValue;
        }
        public void validateFormat()
        {

        }


    }
}
