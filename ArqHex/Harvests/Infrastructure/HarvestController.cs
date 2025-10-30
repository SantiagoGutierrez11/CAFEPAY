using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Harvests.Infrastucture
{
    public class HarvestController
    {
        public long saveHarvest(long _idPlot, DateTime _startDate, decimal _pricePerKilo, int _status, DateTime? _endDate = null)
        {
            return AppServices.Harvest.save.execute( _idPlot, _startDate, _endDate, _pricePerKilo, _status);
        }
        public List<Harvest>listHarvests(){
            return AppServices.Harvest.query.execute();
        }
        public void updateHarvest(long _idHarvest, long _idPlot, DateTime _startDate, decimal _pricePerKilo, int _status, DateTime? _endDate = null)
        {
            AppServices.Harvest.update.execute(_idHarvest, _idPlot, _startDate, _endDate, _pricePerKilo, _status);
        }
    }
}
