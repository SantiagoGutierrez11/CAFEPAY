using CAFEPAY.ArqHex.Payments.application.PaymentQueryAll;
using CAFEPAY.ArqHex.Payments.application.PaymentUpdate;
using CAFEPAY.ArqHex.Payments.application.PaymentSave;
using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Payments.infrastructure;
using CAFEPAY.ArqHex.PaymentDetails.application.PaymentDetailQueryAll;
using CAFEPAY.ArqHex.PaymentDetails.application.PaymentDetailSave;
using CAFEPAY.ArqHex.PaymentDetails.application.PaymentDetailUpdate;
using CAFEPAY.ArqHex.PaymentDetails.application.PaymentQueryByWorkerCode;
using CAFEPAY.ArqHex.PaymentDetails.domain;
using CAFEPAY.ArqHex.PaymentDetails.infrastructure;
using CAFEPAY.ArqHex.Collects.application.CollectQueryByStatus;
using CAFEPAY.ArqHex.Collectors.application.CollectorByIn;
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
using CAFEPAY.ArqHex.Harvests.Application.HarvestQueryByStatus;
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
using CAFEPAY.ArqHex.Collects.application.CollectQueryByWorkerCode;
using CAFEPAY.ArqHex.Collects.application.CollectQueryByStatusAndWorkerCode;
using CAFEPAY.ArqHex.Payments.application.PaymentGetTotalAmountByWorkerCodeAndPaymentId;
using CAFEPAY.ArqHex.PaymentDetails.application.PaymentDetailQueryByPaymentId;


namespace CAFEPAY.ArqHex.Share
{
    public class AppServices
    {
        private static readonly string connectionString = "User Id=adminCAFEPAY;Password=adminCAFEPAY;Data Source=localhost:1521/xe;";
        private static readonly CollectorRepository collectorRepository = new OracleCollectorRepository(connectionString);
        private static readonly HarvestRepository harvestRepository = new OracleHarvestRepository(connectionString);
        private static readonly PlotRepository plotRepository = new OraclePlotRepository(connectionString);
        private static readonly CollectRepository collectRepository = new OracleCollectRepository(connectionString);
        private static readonly PaymentDetailRepository PaymentDetailRepository = new OraclePaymentDetailRepository(connectionString);
        private static readonly PaymentRepository paymentRepository = new OraclePaymentRepository(connectionString);

        public static class CollectorServices
        {
            public static CollectorUpdate update = new CollectorUpdate(collectorRepository);
            public static CollectorSave save = new CollectorSave(collectorRepository);
            public static CollectorQueryAll query = new CollectorQueryAll(collectorRepository);
            public static CollectorQueryByStatus queryByStatus = new CollectorQueryByStatus(collectorRepository);
            public static CollectorQueryByIn queryByIn = new CollectorQueryByIn(collectorRepository);
        }

        public static class HarvestServices
        {
            public static HarvestSave save = new HarvestSave(harvestRepository);
            public static HarvestQueryAll query = new HarvestQueryAll(harvestRepository);
            public static HarvestQueryByStatus queryByStatus = new HarvestQueryByStatus(harvestRepository);
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
            public static CollectQueryByStatus queryByStatus = new CollectQueryByStatus(collectRepository);
            public static CollectQueryByWorkerCode queryByWorkerCode = new CollectQueryByWorkerCode(collectRepository);
            public static CollectQueryByStatusAndWorkerCode queryByStatusAndWorkerCode = new CollectQueryByStatusAndWorkerCode(collectRepository);
        }

        public static class PaymentDetailServices
        {
            public static PaymentDetailSave save = new PaymentDetailSave(PaymentDetailRepository);
            public static PaymentDetailQueryAll query = new PaymentDetailQueryAll(PaymentDetailRepository);
            public static PaymentDetailUpdate update = new PaymentDetailUpdate(PaymentDetailRepository);
            public static QueryByPaymentId queryByPaymentId = new QueryByPaymentId(PaymentDetailRepository);
        }

        public static class PaymentServices
        {
            public static PaymentQueryAll query = new PaymentQueryAll(paymentRepository);
            public static PaymentSave save = new PaymentSave(paymentRepository);
            public static PaymentUpdate update = new PaymentUpdate(paymentRepository);
            public static QueryByWorkerCode queryByWorkerCode = new QueryByWorkerCode(paymentRepository);
            public static GetTotalAmountByWorkerCodeAndPaymentId getTotalAmountByWorkerCodeAndPaymentId = new GetTotalAmountByWorkerCodeAndPaymentId(paymentRepository);
        }
    }
}