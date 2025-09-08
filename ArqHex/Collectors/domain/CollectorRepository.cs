using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    internal interface CollectorRepository
    {
        void save(Collector collector); // Save a new collector , update or insert if not exists
    }
}
