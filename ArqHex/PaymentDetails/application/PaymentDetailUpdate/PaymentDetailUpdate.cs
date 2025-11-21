using CAFEPAY.ArqHex.PaymentDetails.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.application.PaymentDetailUpdate
{
    public class PaymentDetailUpdate
    {
        private readonly PaymentDetailRepository PaymentDetailRepository;
        public PaymentDetailUpdate(PaymentDetailRepository _PaymentDetailRepository)
        {
            PaymentDetailRepository = _PaymentDetailRepository;
        }
        public void execute(long _PaymentDetailAmountToPay, long _PaymentDetailId, long _paymentDatilIdCollect, long _PaymentDetailIdHarvest,
                            long _PaymentDetailIdPayment, long _PaymentDetailIdPlot, long PaymentDetailIdPlot, string _PaymentDetailWorkerCode)
        {
            PaymentDetailAmountToPay PaymentDetailAmountToPay = new PaymentDetailAmountToPay(_PaymentDetailAmountToPay);
            PaymentDetailId PaymentDetailId = new PaymentDetailId(_PaymentDetailId);
            PaymentDetailIdCollect PaymentDetailIdCollect = new PaymentDetailIdCollect(_paymentDatilIdCollect);
            PaymentDetailIdHarvest PaymentDetailIdHarvest = new PaymentDetailIdHarvest(_PaymentDetailIdHarvest);
            PaymentDetailIdPayment PaymentDetailIdPayment = new PaymentDetailIdPayment(_PaymentDetailIdPayment);
            PaymentDetailIdPlot PaymentDetailIdPlotInstance = new PaymentDetailIdPlot(_PaymentDetailIdPlot); 
            PaymentDetailWorkerCode PaymentDetailWorkerCode = new PaymentDetailWorkerCode(_PaymentDetailWorkerCode);
            domain.PaymentDetail PaymentDetail = new domain.PaymentDetail(PaymentDetailAmountToPay, PaymentDetailId, PaymentDetailIdCollect,
                                                            PaymentDetailIdHarvest, PaymentDetailIdPayment, PaymentDetailIdPlotInstance, PaymentDetailWorkerCode);
            PaymentDetailRepository.save(PaymentDetail);
        }
    }
}
