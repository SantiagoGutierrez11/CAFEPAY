using CAFEPAY.ArqHex.Payments.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.application.checkIfPaymentCanBeDeleted
{
    public class checkIfPaymentCanBeDeleted
    {
        private PaymentRepository paymentRepository;   
        long paymentId;
        string details;
        public checkIfPaymentCanBeDeleted(PaymentRepository _paymentRepository)
        {
            this.paymentRepository = _paymentRepository;
        }
        public bool execute(long? _paymentId)
        {
           return paymentRepository.checkIfPaymentCanBeDeleted(_paymentId);
        }
    }
}
