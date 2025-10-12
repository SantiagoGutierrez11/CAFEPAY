using CAFEPAY.ArqHex.Harvests.Application.HarvestQueryAll;
using CAFEPAY.ArqHex.Harvests.Application.HarvestSave;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Harvests.Infrastructure;
using CAFEPAY.ArqHex.Collectors.application.CollectorQueryAll;
using CAFEPAY.ArqHex.Collectors.application.CollectorSave;
using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Collectors.infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share
{
    public class ServiceContainer
    {
        private static readonly String connectionString = "User Id=your_user;Password=your_password;Data Source=your_data_source";
        private static readonly CollectorRepository collectorRepository = new OracleCollectorRepository(connectionString);

        private static readonly String harvestConnectionString = "User Id=your_user;Password=your_password;Data Source=your_data_source";
        private static readonly HarvestRepository harvestRepository = new OracleHarvestRepository(harvestConnectionString);


        public static class Collector
        {
            public static CollectorSave save = new CollectorSave(collectorRepository);
            public static CollectorQueryAll query = new CollectorQueryAll(collectorRepository);
        }

        public static class Harvest
        {
            public static HarvestSave save = new HarvestSave(harvestRepository);
            public static HarvestQueryAll query = new HarvestQueryAll(harvestRepository);
        }
    }
}
