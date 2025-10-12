using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class HarvestStartDate
    {
        private DateTime HarvestStartDateValue { get; }

        public HarvestStartDate(DateTime value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Harvest start date cannot be null or default");

            if (value > DateTime.Now)
                throw new ArgumentException("Harvest start date cannot be in the future");

            HarvestStartDateValue = value;
        }

        public DateTime getValue()
        {
            return this.HarvestStartDateValue;
        }
    }
}