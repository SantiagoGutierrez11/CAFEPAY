using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.infrastructure
{
    public class PaymentController
    {
        public void savePayment(long _oldId, long _paymentId, DateTime _paymentDate, String _paymentWorkerCode)
        {
            AppServices.PaymentServices.save.execute(_oldId, _paymentId, _paymentDate, _paymentWorkerCode);
        }
        public void updatePayment(long _oldId, long _paymentId, DateTime _paymentDate, String _paymentWorkerCode)
        {
            AppServices.PaymentServices.update.execute(_oldId, _paymentId, _paymentDate, _paymentWorkerCode);
        }
        public List<Payment> listPayments()
        {
            return AppServices.PaymentServices.query.execute();
        }
    }
}
