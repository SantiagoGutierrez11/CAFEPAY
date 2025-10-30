using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Domain
{
    public class HarvestStatus
    {
        public int statusValue { get; }
        public HarvestStatus(int _statusValue)
        {
            this.statusValue = _statusValue;
            ValidateFormat();
        }
        public void ValidateFormat()
        {
            if (this.statusValue < 0 || this.statusValue > 2)
            {
                throw new ArgumentException("Harvest status must be between 0 and 2");
            }
        }
    }
}
