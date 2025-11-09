using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public interface CollectorRepository
    {
        void save(Collector collector); // Save a new collector , update or insert if not exists
        void update(Collector collector, long oldId); // Update an existing collector
        List<Collector> queryAll(); // List all collectors
        List<Collector> queryByStatus(int status); // List collectors by status
    }
}
