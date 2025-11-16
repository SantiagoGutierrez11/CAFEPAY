using CAFEPAY.ArqHex.PaymentsDetails.domain;
using CAFEPAY.ArqHex.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.infrastructure
{
    public class PaymentsDetailsController
    {
        public void savePaymentsDetails(long _PaymentsDetailsAmountToPay, long _PaymentsDetailsId, long _paymentDatilIdCollect, long _PaymentsDetailsIdHarvest,
                                long _PaymentsDetailsIdPayment, long _PaymentsDetailsIdPlot, string _PaymentsDetailsWorkerCode)
        {
            AppServices.PaymentsDetailsServices.save.execute(_PaymentsDetailsAmountToPay, _PaymentsDetailsId, _paymentDatilIdCollect, _PaymentsDetailsIdHarvest,
                                                    _PaymentsDetailsIdPayment, _PaymentsDetailsIdPlot, _PaymentsDetailsWorkerCode);
        }
        public void updatePaymentsDetails(long _oldId, long _PaymentsDetailsAmountToPay, long _PaymentsDetailsId, long _paymentDatilIdCollect, long _PaymentsDetailsIdHarvest,
                                long _PaymentsDetailsIdPayment, long _PaymentsDetailsIdPlot, string _PaymentsDetailsWorkerCode)
        {
            AppServices.PaymentsDetailsServices.update.execute(_oldId, _PaymentsDetailsAmountToPay, _PaymentsDetailsId, _paymentDatilIdCollect, _PaymentsDetailsIdHarvest,
                                                    _PaymentsDetailsIdPayment, _PaymentsDetailsIdPlot, _PaymentsDetailsWorkerCode);
        }
        public List<PaymentsDetailsEntity> listPaymentsDetailss()
        {
            return AppServices.PaymentsDetailsServices.query.execute();
        }
    }
}
