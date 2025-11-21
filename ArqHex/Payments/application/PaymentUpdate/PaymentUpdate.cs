using CAFEPAY.ArqHex.Payments.domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.application.PaymentUpdate
{
    public class PaymentUpdate
    {
        private readonly PaymentRepository paymentRepository;
        public PaymentUpdate(PaymentRepository _paymentRepository)
        {
            this.paymentRepository = _paymentRepository;
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
