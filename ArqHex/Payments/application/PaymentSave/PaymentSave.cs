using CAFEPAY.ArqHex.Payments.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.application.PaymentSave
{
    public class PaymentSave
    {
        private readonly PaymentRepository paymentRepository;
        public PaymentSave(PaymentRepository _paymentRepository)
        {
            paymentRepository = _paymentRepository;
        }

        public void execute(long _oldId, long _paymentId, DateTime _paymentDate, String _paymentWorkerCode)
        {
            PaymentId paymentId = new PaymentId(_paymentId);
            PaymentDate paymentDate = new PaymentDate(_paymentDate);
            PaymentWorkerCode paymentWorkerCode = new PaymentWorkerCode(_paymentWorkerCode);
            Payment payment = new Payment(paymentId, paymentDate, paymentWorkerCode);
            paymentRepository.update(payment, _oldId);
        }
    }
}
