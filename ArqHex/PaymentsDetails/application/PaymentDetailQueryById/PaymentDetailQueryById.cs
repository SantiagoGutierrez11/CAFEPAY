using CAFEPAY.ArqHex.PaymentsDetails.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.application.PaymentsDetailsQueryById
{
    public class PaymentsDetailsQueryById
    {
        private readonly PaymentsDetailsRepository PaymentsDetailsRepository;

        public PaymentsDetailsQueryById(PaymentsDetailsRepository _PaymentsDetailsRepository)
        {
            PaymentsDetailsRepository = _PaymentsDetailsRepository;
        }

        public List<PaymentsDetailsEntity> execute(long paymentId)
        {
            return PaymentsDetailsRepository.queryByPaymentId(paymentId);
        }
    }
}