using CAFEPAY.ArqHex.Payments.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.application.PaymentQueryByWorkerCode
{
    public class QueryByWorkerCode
    {
        private PaymentRepository paymentRepository;
        public QueryByWorkerCode(PaymentRepository _paymentRepository)
        {
            this.paymentRepository = _paymentRepository;
        }
        public List<Payment> execute(string workerCode)
        {
            return paymentRepository.queryByWorkerCode(workerCode);
        }
    }
}
