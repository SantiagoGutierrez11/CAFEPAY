using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.domain
{
    public class PaymentsDetailsEntity
    {
        public PaymentsDetailsAmountToPay amountToPay { get; }
        public PaymentsDetailsId id { get; }
        public PaymentsDetailsIdCollect collectId { get; }
        public PaymentsDetailsIdHarvest harvestId { get; }
        public PaymentsDetailsIdPayment paymentId { get; }
        public PaymentsDetailsIdPlot plotId { get; }
        public PaymentsDetailsWorkerCode workerCode { get; }

        public PaymentsDetailsEntity(PaymentsDetailsAmountToPay _amountToPay, PaymentsDetailsId _id,
            PaymentsDetailsIdCollect _collectId, PaymentsDetailsIdHarvest _harvestId,
            PaymentsDetailsIdPayment _paymentId, PaymentsDetailsIdPlot _plotId,
            PaymentsDetailsWorkerCode _workerCode)
        {
            this.amountToPay = _amountToPay;
            this.id = _id;
            this.collectId = _collectId;
            this.harvestId = _harvestId;
            this.paymentId = _paymentId;
            this.plotId = _plotId;
            this.workerCode = _workerCode;
        }

    }
}
