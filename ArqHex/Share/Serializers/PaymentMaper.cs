using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAFEPAY.ArqHex.Share.Serializers
{
    public class PaymentMapper
    {
        public static List<PaymentDTO> ToDTOList(List<Payment> Payments)
        {
            if (Payments == null) return new List<PaymentDTO>();
            return Payments.Select(p => new PaymentDTO
            {
                Id = p.id.id ?? 0,
                Date = p.date.paymentDate,
                WorkerCode = p.workerCode.workerCodeValue
            }).ToList();
        }
    }
}