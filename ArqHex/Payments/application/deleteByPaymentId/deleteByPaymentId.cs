using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.application.delete
{
    public class deleteByPaymentId
    {
        private readonly domain.PaymentRepository paymentRepository;
        public deleteByPaymentId(domain.PaymentRepository _paymentRepository)
        {
            paymentRepository = _paymentRepository;
        }
        public void execute(long? paymentId, string reason)
        {
            paymentRepository.deleteByPaymentId(paymentId, reason);
        }
    }
}
