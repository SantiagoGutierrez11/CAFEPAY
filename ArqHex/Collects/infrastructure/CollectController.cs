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
        public void saveCollect(long _collectId, string _collectWorkerCode, DateTime _collectDate,
                               decimal _collectedKilos, long _collectIdHarvest, long _collectIdPayment,
                               int _collectStatus, long _collectAmountToPaidValue, long _collectIdPlot, int _collectIscountable)
        {
            AppServices.CollectServices.save.execute(_collectId, _collectWorkerCode, _collectDate,
                                                     _collectedKilos, _collectIdHarvest, _collectIdPayment,
                                                     _collectStatus, _collectAmountToPaidValue, _collectIdPlot, _collectIscountable);
        }

        public void updateCollect(long _oldId, long _collectId, string _collectWorkerCode, DateTime _collectDate,
                                 decimal _collectedKilos, long _collectIdHarvest, long _collectIdPayment,
                                 int _collectStatus, long _collectAmountToPaidValue, long _collectIdPlot, int _collectIscountable)
        {
            AppServices.CollectServices.update.execute(_oldId, _collectId, _collectWorkerCode, _collectDate,
                                                       _collectedKilos, _collectIdHarvest, _collectIdPayment,
                                                       _collectStatus, _collectAmountToPaidValue, _collectIdPlot, _collectIscountable);
        }

        public List<Collect> listCollects()
        {
            return AppServices.CollectServices.query.execute();
        }
    }
}