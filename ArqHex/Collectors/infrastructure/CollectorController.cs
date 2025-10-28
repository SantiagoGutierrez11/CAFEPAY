using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CAFEPAY.ArqHex.Collectors.infrastructure
{
    public class CollectorController
    {
       public void saveCollector(string _collectorWorkerCode, long _collectorId, string _collectorFirstName,
                            string _collectorLastName, string _collectorPhone, int _collectorStatus) { 
            AppServices.Collector.save.execute(_collectorWorkerCode, _collectorId, _collectorFirstName, _collectorLastName,
                                                    _collectorPhone, _collectorStatus);
        }
        public void updateCollector(long _oldId, string _collectorWorkerCode, long _collectorId, string _collectorFirstName,
                            string _collectorLastName, string _collectorPhone, int _collectorStatus)
        {
            AppServices.Collector.update.execute(_oldId, _collectorWorkerCode, _collectorId, _collectorFirstName, _collectorLastName,
                                                    _collectorPhone, _collectorStatus);
        }
        public List<Collector> listCollectors() { 
            return AppServices.Collector.query.execute();
        }
    }
}
