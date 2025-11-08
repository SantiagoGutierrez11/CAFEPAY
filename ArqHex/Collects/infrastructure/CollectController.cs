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
                               decimal _collectedKilos, long _collectIdHarvest,
                               int _collectStatus, long _collectAmountToPaidValue, long _collectIdPlot, int _collectIsCountable)
        {
            AppServices.CollectServices.save.execute(_collectId, _collectWorkerCode, _collectDate,
                                                     _collectedKilos, _collectIdHarvest,
                                                     _collectStatus, _collectAmountToPaidValue, _collectIdPlot, _collectIsCountable);
        }

        public void updateCollect(long _oldId, long _collectId, string _collectWorkerCode, DateTime _collectDate,
                                 decimal _collectedKilos, long _collectIdHarvest,
                                 int _collectStatus, long _collectAmountToPaidValue, long _collectIdPlot, int _collectIsCountable)
        {
            AppServices.CollectServices.update.execute(_oldId, _collectId, _collectWorkerCode, _collectDate,
                                                       _collectedKilos, _collectIdHarvest,
                                                       _collectStatus, _collectAmountToPaidValue, _collectIdPlot, _collectIsCountable);
        }

        public List<Collect> listCollects()
        {
            return AppServices.CollectServices.query.execute();
        }
    }
}