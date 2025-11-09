using CAFEPAY.ArqHex.Payments.application.PaymentQueryAll;
using CAFEPAY.ArqHex.Payments.application.PaymentUpdate;
using CAFEPAY.ArqHex.Payments.application.PaymentSave;
using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Payments.infrastructure;
using CAFEPAY.ArqHex.PaymentsDetails.application.PaymentsDetailsQueryAll;
using CAFEPAY.ArqHex.PaymentsDetails.application.PaymentsDetailsSave;
using CAFEPAY.ArqHex.PaymentsDetails.application.PaymentsDetailsUpdate;
using CAFEPAY.ArqHex.PaymentsDetails.application.PaymentsDetailsQueryById;
using CAFEPAY.ArqHex.PaymentsDetails.domain;
using CAFEPAY.ArqHex.PaymentsDetails.infrastructure;
using CAFEPAY.ArqHex.Collectors.application.CollectorQueryAll;
using CAFEPAY.ArqHex.Collectors.application.CollectorQueryByStatus;
using CAFEPAY.ArqHex.Collectors.application.CollectorSave;
using CAFEPAY.ArqHex.Collectors.application.CollectorUpdate;
using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Collectors.infrastructure;
using CAFEPAY.ArqHex.Collects.application.CollectQueryAll;
using CAFEPAY.ArqHex.Collects.application.CollectSave;
using CAFEPAY.ArqHex.Collects.application.CollectUpdate;
using CAFEPAY.ArqHex.Collects.domain;
using CAFEPAY.ArqHex.Collects.infrastructure;
using CAFEPAY.ArqHex.Harvests.Application.HarvestQueryAll;
using CAFEPAY.ArqHex.Harvests.Application.HarvestSave;
using CAFEPAY.ArqHex.Harvests.Application.HarvestUpdate;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Harvests.Infrastructure;
using CAFEPAY.ArqHex.Plots.Application.PlotQueryAll;
using CAFEPAY.ArqHex.Plots.Application.PlotQueyById;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Plots.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share
{
    public class AppServices
    {
        private static readonly string connectionstring = "User Id=adminCAFEPAY;Password=adminCAFEPAY;Data Source=localhost:1521/xe;";
        private static readonly CollectorRepository collectorRepository = new OracleCollectorRepository(connectionstring);
        private static readonly HarvestRepository harvestRepository = new OracleHarvestRepository(connectionstring);
        private static readonly PlotRepository plotRepository = new OraclePlotRepository(connectionstring);
        private static readonly CollectRepository collectRepository = new OracleCollectRepository(connectionstring);
        private static readonly PaymentsDetailsRepository PaymentsDetailsRepository = new OraclePaymentsDetailsRepository(connectionstring);
        private static readonly PaymentRepository paymentRepository = new OraclePaymentRepository(connectionstring);

        public static class CollectorServices
        {
            public static CollectorUpdate update = new CollectorUpdate(collectorRepository);
            public static CollectorSave save = new CollectorSave(collectorRepository);
            public static CollectorQueryAll query = new CollectorQueryAll(collectorRepository);
            public static CollectorQueryByStatus queryByStatus = new CollectorQueryByStatus(collectorRepository);
        }

        public static class HarvestServices
        {
            public static HarvestSave save = new HarvestSave(harvestRepository);
            public static HarvestQueryAll query = new HarvestQueryAll(harvestRepository);
            public static HarvestUpdate update = new HarvestUpdate(harvestRepository);
        }

        public static class PlotServices
        {
            public static PlotQueryAll query = new PlotQueryAll(plotRepository);
            public static PlotQueryById queryById = new PlotQueryById(plotRepository);
        }

        public static class CollectServices
        {
            public static CollectSave save = new CollectSave(collectRepository);
            public static CollectQueryAll query = new CollectQueryAll(collectRepository);
            public static CollectUpdate update = new CollectUpdate(collectRepository);
        }

        public static class PaymentsDetailsServices
        {
            public static PaymentsDetailsSave save = new PaymentsDetailsSave(PaymentsDetailsRepository);
            public static PaymentsDetailsQueryAll query = new PaymentsDetailsQueryAll(PaymentsDetailsRepository);
            public static PaymentsDetailsUpdate update = new PaymentsDetailsUpdate(PaymentsDetailsRepository);
            public static PaymentsDetailsQueryById queryById = new PaymentsDetailsQueryById(PaymentsDetailsRepository);
        }

        public static class PaymentServices
        {
            public static PaymentQueryAll query = new PaymentQueryAll(paymentRepository);
            public static PaymentSave save = new PaymentSave(paymentRepository);
            public static PaymentUpdate update = new PaymentUpdate(paymentRepository);
        }
    }
}