using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class HarvestPricePerKilo
    {
        public decimal pricePerKiloValue;

        public HarvestPricePerKilo(decimal _pricePerKilo)
        {
            this.pricePerKiloValue = _pricePerKilo;
            ValidateFormat();
        }

        private void ValidateFormat()
        {
            if (this.pricePerKiloValue <= 0)
            {
                throw new ArgumentException("Harvest price per kilo must be greater than zero");
            }

            if (this.pricePerKiloValue > 3000 || this.pricePerKiloValue < 700)
            {
                throw new ArgumentException("El precio debe estar entre los 700 - 3000 COP");
            }
        }

    }
}
