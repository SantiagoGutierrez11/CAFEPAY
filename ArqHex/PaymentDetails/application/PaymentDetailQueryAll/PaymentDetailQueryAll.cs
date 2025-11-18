using CAFEPAY.ArqHex.PaymentDetails.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.application.PaymentDetailQueryAll
{
    public class PaymentDetailQueryAll
    {
        private readonly PaymentDetailRepository PaymentDetailRepository;
        public PaymentDetailQueryAll(PaymentDetailRepository _PaymentDetailRepository)
        {
            PaymentDetailRepository = _PaymentDetailRepository;
        }
        public List<domain.PaymentDetail> execute()
        {
            return this.PaymentDetailRepository.queryAll();
        }
    }
}
