using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.ArqHex.PaymentDetail.domain
{
    public class PaymentDetailAmontToPay
    {
        public long paymentDetailAmountToPayValue { get; }
        public PaymentDetailAmontToPay(long _paymentDetailAmountToPayValue)
        {
            ValidateFormat(_paymentDetailAmountToPayValue);
            this.paymentDetailAmountToPayValue = _paymentDetailAmountToPayValue;
        }
        private void ValidateFormat(long amountToPayValue)
        {
            // Validar que no sea negativo
            if (amountToPayValue < 0)
            {
                throw new ArgumentException("El monto a pagar no puede ser negativo");
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is PaymentDetailAmontToPay other)
            {
                return paymentDetailAmountToPayValue == other.paymentDetailAmountToPayValue;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return paymentDetailAmountToPayValue.GetHashCode();
        }
        public override string ToString()
        {
            return paymentDetailAmountToPayValue.ToString();
        }
        public long GetValue()
        {
            return paymentDetailAmountToPayValue;
        }
        public string GetValueAsString()
        {
            return paymentDetailAmountToPayValue.ToString();
        }
    }
}
