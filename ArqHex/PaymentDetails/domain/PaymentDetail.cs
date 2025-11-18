using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.domain
{
    public class PaymentDetail
    {
        public PaymentDetailAmountToPay amountToPay { get; }
        public PaymentDetailId id { get; }
        public PaymentDetailIdCollect collectId { get; }
        public PaymentDetailIdHarvest harvestId { get; }
        public PaymentDetailIdPayment paymentId { get; }
        public PaymentDetailIdPlot plotId { get; }
        public PaymentDetailWorkerCode workerCode { get; }

        public PaymentDetail(PaymentDetailAmountToPay _amountToPay, PaymentDetailId _id,
            PaymentDetailIdCollect _collectId, PaymentDetailIdHarvest _harvestId,
            PaymentDetailIdPayment _paymentId, PaymentDetailIdPlot _plotId,
            PaymentDetailWorkerCode _workerCode)
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
