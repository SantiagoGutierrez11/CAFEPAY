using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.domain
{
    public interface PaymentDetailRepository
    {
        long save(PaymentDetail PaymentDetail); // Save a new payment detail , update or insert if not exists
        void update(PaymentDetail PaymentDetail); // Update an existing payment detail
        List<PaymentDetail> queryAll(); // List all payment
        List<PaymentDetail> queryByPaymentId(long? paymentId);
    }
}
