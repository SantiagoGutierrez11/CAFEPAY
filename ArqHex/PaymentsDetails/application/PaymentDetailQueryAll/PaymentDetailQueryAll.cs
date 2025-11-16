using CAFEPAY.ArqHex.PaymentsDetails.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.application.PaymentsDetailsQueryAll
{
    public class PaymentsDetailsQueryAll
    {
        private readonly PaymentsDetailsRepository PaymentsDetailsRepository;
        public PaymentsDetailsQueryAll(PaymentsDetailsRepository _PaymentsDetailsRepository)
        {
            PaymentsDetailsRepository = _PaymentsDetailsRepository;
        }
        public List<PaymentsDetailsEntity> execute()
        {
            return this.PaymentsDetailsRepository.queryAll();
        }
    }
}
