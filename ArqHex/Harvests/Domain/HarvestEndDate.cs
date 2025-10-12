using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class HarvestEndDate
    {
        private DateTime HarvestEndDateValue { get; }

        public HarvestEndDate(DateTime value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Harvest end date cannot be null or default");

            if (value > DateTime.Now)
                throw new ArgumentException("Harvest end date cannot be in the future");

            HarvestEndDateValue = value;
        }

        public DateTime getValue()
        {
            return this.HarvestEndDateValue;
        }
    }
}
