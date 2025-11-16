using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.domain
{
    public interface PaymentRepository
    {
        void save(Payment payment); // Save a new payment , update or insert if not exists
        void update(Payment payment, long oldId); // Update an existing payment
        List<Payment> queryAll(); // List all payments
    }
}
