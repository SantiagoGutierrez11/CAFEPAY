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
        public long saveHarvest(long _idPlot, DateTime _startDate, decimal _pricePerKilo)
        {
            return AppServices.HarvestServices.save.execute( _idPlot, _startDate, _pricePerKilo);
        }
        public List<Harvest>listHarvests(){
            return AppServices.HarvestServices.query.execute();
        }
        public void updateHarvest(long _idHarvest, long _idPlot, DateTime _startDate, decimal _pricePerKilo, int _status, DateTime? _endDate = null)
        {
            AppServices.HarvestServices.update.execute(_idHarvest, _idPlot, _startDate, _endDate, _pricePerKilo, _status);
        }
        
        // 🔹 Nuevo método para asociar recolector a cosecha
        public long associateCollector(long _idHarvest, long _idCollector)
        {
            return AppServices.HarvestServices.associateCollector.execute(_idHarvest, _idCollector);
        }
    }
}
