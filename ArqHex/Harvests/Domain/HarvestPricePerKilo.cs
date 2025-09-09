using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    internal class HarvestPricePerKilo
    {
        public decimal pricePerKiloValue;

        public HarvestPricePerKilo(decimal _pricePerKiloValue)
        {
            this.pricePerKiloValue = _pricePerKiloValue;
            ValidateFormat();
        }

        private void ValidateFormat()
        {
            if (this.pricePerKiloValue <= 0)
            {
                throw new ArgumentException("Harvest price per kilo must be greater than zero");
            }

            if (this.pricePerKiloValue > 999999.99m)
            {
                throw new ArgumentException("Harvest price per kilo is unrealistically high");
            }
        }

        public decimal GetValue()
        {
            return this.pricePerKiloValue;
        }
    }
}
