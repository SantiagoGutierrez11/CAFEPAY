using CAFEPAY.ArqHex.PaymentsDetails.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentsDetails.application.PaymentsDetailsSave
{
    public class PaymentsDetailsSave
    {
        private readonly PaymentsDetailsRepository PaymentsDetailsRepository;

        public PaymentsDetailsSave(PaymentsDetailsRepository _PaymentsDetailsRepository)
        {
            PaymentsDetailsRepository = _PaymentsDetailsRepository;
        }

        public void execute(long _PaymentsDetailsAmountToPay, long _PaymentsDetailsId, long _paymentDatilIdCollect,
                            long _PaymentsDetailsIdHarvest, long _PaymentsDetailsIdPayment, long _PaymentsDetailsIdPlot,
                            string _PaymentsDetailsWorkerCode)
        {
            PaymentsDetailsAmountToPay PaymentsDetailsAmountToPay = new PaymentsDetailsAmountToPay(_PaymentsDetailsAmountToPay);
            PaymentsDetailsId PaymentsDetailsId = new PaymentsDetailsId(_PaymentsDetailsId);
            PaymentsDetailsIdCollect PaymentsDetailsIdCollect = new PaymentsDetailsIdCollect(_paymentDatilIdCollect);
            PaymentsDetailsIdHarvest PaymentsDetailsIdHarvest = new PaymentsDetailsIdHarvest(_PaymentsDetailsIdHarvest);
            PaymentsDetailsIdPayment PaymentsDetailsIdPayment = new PaymentsDetailsIdPayment(_PaymentsDetailsIdPayment);
            PaymentsDetailsIdPlot PaymentsDetailsIdPlot = new PaymentsDetailsIdPlot(_PaymentsDetailsIdPlot);
            PaymentsDetailsWorkerCode PaymentsDetailsWorkerCode = new PaymentsDetailsWorkerCode(_PaymentsDetailsWorkerCode);

            PaymentsDetailsEntity PaymentsDetails = new PaymentsDetailsEntity(PaymentsDetailsAmountToPay, PaymentsDetailsId,
                                                                         PaymentsDetailsIdCollect, PaymentsDetailsIdHarvest,
                                                                         PaymentsDetailsIdPayment, PaymentsDetailsIdPlot,
                                                                         PaymentsDetailsWorkerCode);

            PaymentsDetailsRepository.save(PaymentsDetails);
        }
    }
}