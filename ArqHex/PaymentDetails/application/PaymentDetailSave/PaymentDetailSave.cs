using CAFEPAY.ArqHex.PaymentDetails.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.PaymentDetails.application.PaymentDetailSave
{
    public class PaymentDetailSave
    {
        private readonly PaymentDetailRepository PaymentDetailRepository;

        public PaymentDetailSave(PaymentDetailRepository _PaymentDetailRepository)
        {
            PaymentDetailRepository = _PaymentDetailRepository;
        }

        public long execute(decimal _PaymentDetailAmountToPay, long? _PaymentDetailId, long? _paymentDatilIdCollect,
                            long? _PaymentDetailIdHarvest, long? _PaymentDetailIdPayment, long _PaymentDetailIdPlot,
                            string _PaymentDetailWorkerCode)
        {
            PaymentDetailAmountToPay PaymentDetailAmountToPay = new PaymentDetailAmountToPay(_PaymentDetailAmountToPay);
            PaymentDetailId PaymentDetailId = new PaymentDetailId(_PaymentDetailId);
            PaymentDetailIdCollect PaymentDetailIdCollect = new PaymentDetailIdCollect(_paymentDatilIdCollect);
            PaymentDetailIdHarvest PaymentDetailIdHarvest = new PaymentDetailIdHarvest(_PaymentDetailIdHarvest);
            PaymentDetailIdPayment PaymentDetailIdPayment = new PaymentDetailIdPayment(_PaymentDetailIdPayment);
            PaymentDetailIdPlot PaymentDetailIdPlot = new PaymentDetailIdPlot(_PaymentDetailIdPlot);
            PaymentDetailWorkerCode PaymentDetailWorkerCode = new PaymentDetailWorkerCode(_PaymentDetailWorkerCode);

            domain.PaymentDetail PaymentDetail = new domain.PaymentDetail(PaymentDetailAmountToPay, PaymentDetailId,
                                                                         PaymentDetailIdCollect, PaymentDetailIdHarvest,
                                                                         PaymentDetailIdPayment, PaymentDetailIdPlot,
                                                                         PaymentDetailWorkerCode);

            return PaymentDetailRepository.save(PaymentDetail);
        }
    }
}