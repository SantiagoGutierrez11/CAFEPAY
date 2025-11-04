using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectIsContable
    {
        public int isContableValue { get; }

        public CollectIsContable(int _isContable) 
        {
            this.isContableValue = _isContable;
            validateFormat();
        }

        public void validateFormat() { }
    }
}
