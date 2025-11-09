using CAFEPAY.ArqHex.PaymentsDetails.domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.Serializers
{
    public class PaymentsDetailsMapper
    {
        public static List<PaymentsDetailsDTO> ToDTOList(List<PaymentsDetailsEntity> PaymentsDetailss)
        {
            if (PaymentsDetailss == null) return new List<PaymentsDetailsDTO>();

            return PaymentsDetailss.Select(p => new PaymentsDetailsDTO
            {
                AmountToPay = p.amountToPay.PaymentsDetailsAmountToPayValue,
                Id = p.id.idValue ?? 0,
                CollectId = p.collectId.idCollectValue,
                HarvestId = p.harvestId.idHarvestValue,
                PaymentId = p.paymentId.idPaymentValue,
                PlotId = p.plotId.idPlotValue,
                WorkerCode = p.workerCode.Value 
            }).ToList();
        }
    }
}