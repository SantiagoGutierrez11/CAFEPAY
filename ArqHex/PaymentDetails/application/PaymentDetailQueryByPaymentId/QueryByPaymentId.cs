using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.application.PaymentDetailQueryByPaymentId
{
    public class QueryByPaymentId
    {
        private readonly domain.PaymentDetailRepository paymentDetailRepository;
        public QueryByPaymentId(domain.PaymentDetailRepository _paymentDetailRepository)
        {
            paymentDetailRepository = _paymentDetailRepository;
        }
        public List<domain.PaymentDetail> execute(long? paymentId)
        {
            return paymentDetailRepository.queryByPaymentId(paymentId);
        }
    }
}
