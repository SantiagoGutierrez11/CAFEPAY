using CAFEPAY.ArqHex.PaymentDetails.domain;
using CAFEPAY.ArqHex.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetail.infrastructure
{
    public class PaymentDetailController
    {
        public void savePaymentDetail(long _PaymentDetailAmountToPay, long _PaymentDetailId, long _paymentDatilIdCollect, long _PaymentDetailIdHarvest,
                                long _PaymentDetailIdPayment, long _PaymentDetailIdPlot, string _PaymentDetailWorkerCode)
        {
            AppServices.PaymentDetailServices.save.execute(_PaymentDetailAmountToPay, _PaymentDetailId, _paymentDatilIdCollect, _PaymentDetailIdHarvest,
                                                    _PaymentDetailIdPayment, _PaymentDetailIdPlot, _PaymentDetailWorkerCode);
        }
        public void updatePaymentDetail(long _oldId, long _PaymentDetailAmountToPay, long _PaymentDetailId, long _paymentDatilIdCollect, long _PaymentDetailIdHarvest,
                                long _PaymentDetailIdPayment, long _PaymentDetailIdPlot, string _PaymentDetailWorkerCode)
        {
            AppServices.PaymentDetailServices.update.execute(_oldId, _PaymentDetailAmountToPay, _PaymentDetailId, _paymentDatilIdCollect, _PaymentDetailIdHarvest,
                                                    _PaymentDetailIdPayment, _PaymentDetailIdPlot, _PaymentDetailWorkerCode);
        }
        public List<PaymentDetails.domain.PaymentDetail> listPaymentDetails()
        {
            return AppServices.PaymentDetailServices.query.execute();
        }
        public List<PaymentDetails.domain.PaymentDetail> queryByPaymentID(long? paymentID)
        {
            return AppServices.PaymentDetailServices.queryByPaymentId.execute(paymentID);
        }
    }
}
