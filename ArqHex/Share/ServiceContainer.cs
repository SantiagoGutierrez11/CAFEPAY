using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Collectors.infrastructure;
using CAFEPAY.ArqHex.Collectors.application.CollectorSave;
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
        public static class Collector
        {
            public static CollectorSave save = new CollectorSave(collectorRepository);
        }
    }
}
