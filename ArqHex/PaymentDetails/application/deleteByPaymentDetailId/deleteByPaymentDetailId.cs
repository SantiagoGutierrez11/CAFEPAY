using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.application.deleteByPaymentId
{
    public class deleteByPaymentDetailId
    { 
        private readonly domain.PaymentDetailRepository paymentDetailRepository;
        public deleteByPaymentDetailId(domain.PaymentDetailRepository _paymentDetailRepository)
        {
            paymentDetailRepository = _paymentDetailRepository;
        }
        public void execute(long paymentId, string reason)
        {
            paymentDetailRepository.deleteByPaymentDetailId(paymentId, reason);
        }
    }
}
