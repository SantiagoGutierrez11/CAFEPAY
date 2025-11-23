using CAFEPAY.ArqHex.PaymentDetails.domain;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.Serializers
{
    public class PaymentDetailMaper
    {
        public static List<PaymentDetailDTO> ToDTOList(List<PaymentDetails.domain.PaymentDetail> PaymentDetails)
        {
            if (PaymentDetails == null) return new List<PaymentDetailDTO>();

            Plot plot = AppServices.PlotServices.queryById.execute(PaymentDetails[0].plotId.idPlotValue); // Dummy call to avoid unused variable warning
            return PaymentDetails.Select(p => new PaymentDetailDTO
            {
                AmountToPay = p.amountToPay.amountToPayValue,
                Id = p.id.idValue ?? 0,
                CollectId = p.collectId.idCollectValue,
                HarvestId = p.harvestId.idHarvestValue,
                PaymentId = p.paymentId.idPaymentValue,
                PlotId = p.plotId.idPlotValue,
                WorkerCode = p.workerCode.Value,
                PlotName = plot.name.plotNameValue
            }).ToList();
        }
    }
}