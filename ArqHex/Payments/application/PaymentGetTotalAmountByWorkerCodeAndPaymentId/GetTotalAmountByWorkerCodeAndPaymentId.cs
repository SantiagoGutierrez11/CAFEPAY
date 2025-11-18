using CAFEPAY.ArqHex.Payments.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.application.PaymentGetTotalAmountByWorkerCodeAndPaymentId
{
    public class GetTotalAmountByWorkerCodeAndPaymentId
    {
        private readonly PaymentRepository paymentRepository;
        public GetTotalAmountByWorkerCodeAndPaymentId(domain.PaymentRepository _paymentRepository)
        {
            paymentRepository = _paymentRepository;
        }
        public decimal execute(string workerCode, long? paymentId)
        {
            return paymentRepository.getTotalAmountByWorkerCodeAndPaymentId(workerCode, paymentId);
        }
    }
}
