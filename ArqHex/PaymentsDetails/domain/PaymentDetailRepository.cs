using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.domain
{
    public interface PaymentsDetailsRepository
    {
        void save(PaymentsDetailsEntity PaymentsDetails); // Save a new payment detail , update or insert if not exists
        void update(PaymentsDetailsEntity PaymentsDetails, long oldId); // Update an existing payment detail
        List<PaymentsDetailsEntity> queryAll(); // List all payment details
        List<PaymentsDetailsEntity> queryByPaymentId(long paymentId); // List payment details by payment id
    }
}
