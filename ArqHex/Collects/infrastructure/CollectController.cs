using CAFEPAY.ArqHex.Collects.domain;
using CAFEPAY.ArqHex.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.infrastructure
{
    public class CollectController
    {
        public void saveCollect(long _collectId, string _collectCollectorWorkerId, DateTime _collectDate,
                               decimal _collectedKilos, long _collectIdHarvest, long _collectIdPayment,
                               int _collectStatus, long _collectorAmountToPaid, long _collectIdPlot, int _collectIsContable)
        {
            AppServices.CollectServices.save.execute(_collectId, _collectCollectorWorkerId, _collectDate,
                                                     _collectedKilos, _collectIdHarvest, _collectIdPayment,
                                                     _collectStatus, _collectorAmountToPaid, _collectIdPlot, _collectIsContable);
        }

        public void updateCollect(long _oldId, long _collectId, string _collectCollectorWorkerId, DateTime _collectDate,
                                 decimal _collectedKilos, long _collectIdHarvest, long _collectIdPayment,
                                 int _collectStatus, long _collectorAmountToPaid, long _collectIdPlot, int _collectIsContable)
        {
            AppServices.CollectServices.update.execute(_oldId, _collectId, _collectCollectorWorkerId, _collectDate,
                                                       _collectedKilos, _collectIdHarvest, _collectIdPayment,
                                                       _collectStatus, _collectorAmountToPaid, _collectIdPlot, _collectIsContable);
        }

        public List<Collect> listCollects()
        {
            return AppServices.CollectServices.query.execute();
        }
    }
}