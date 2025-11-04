using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public interface CollectRepository
    {
        void save(Collect collect); // Save a new collect, update or insert if not exists
        void update(Collect collect, long oldId); // Update an existing collect
        List<Collect> queryAll(); // List all collects
    }
}