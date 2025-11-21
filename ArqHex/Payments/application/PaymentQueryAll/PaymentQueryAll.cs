using CAFEPAY.ArqHex.Payments.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.application.PaymentQueryAll
{
    public class PaymentQueryAll
    {
        private readonly PaymentRepository paymentRepository;
        public PaymentQueryAll(PaymentRepository _paymentRepository)
        {
            paymentRepository = _paymentRepository;
        }
        public List<Payment> execute()
        {
            return this.paymentRepository.queryAll();
        }
    }
}
