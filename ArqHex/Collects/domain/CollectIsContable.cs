using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectIsContable
    {
        private int isContable { get; }

        public CollectIsContable(int _isContable) 
        {
            this.isContable = _isContable;
            validateFormat();
        }

        public void validateFormat() { }
    }
}
