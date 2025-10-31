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
        public void saveCollect(long _collectId, long _collectCollectorId, DateTime _collectDate,
                               decimal _collectedKilos, long _collectIdHarvest, long _collectIdPayment,
                               int _collectStatus, long _collectPaid)
        {
            AppServices.CollectServices.save.execute(_collectId, _collectCollectorId, _collectDate,
                                                     _collectedKilos, _collectIdHarvest, _collectIdPayment,
                                                     _collectStatus, _collectPaid);
        }

        public void updateCollect(long _oldId, long _collectId, long _collectCollectorId, DateTime _collectDate,
                                 decimal _collectedKilos, long _collectIdHarvest, long _collectIdPayment,
                                 int _collectStatus, long _collectPaid)
        {
            AppServices.CollectServices.update.execute(_oldId, _collectId, _collectCollectorId, _collectDate,
                                                       _collectedKilos, _collectIdHarvest, _collectIdPayment,
                                                       _collectStatus, _collectPaid);
        }

        public List<Collect> listCollects()
        {
            return AppServices.CollectServices.query.execute();
        }
    }
}