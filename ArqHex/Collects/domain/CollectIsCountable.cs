using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectIsCountable
    {
        public int isCountableValue { get; }

        public CollectIsCountable(int _isCountable) 
        {
            this.isCountableValue = _isCountable;
            validateFormat();
        }

        public void validateFormat() { }
    }
}
