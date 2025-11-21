using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.ArqHex.PaymentDetails.domain
{
    public class PaymentDetailAmountToPay
    {
        public decimal amountToPayValue { get; }
        public PaymentDetailAmountToPay(decimal _amountToPay)
        {
            ValidateFormat(_amountToPay);
            this.amountToPayValue = _amountToPay;
        }
        private void ValidateFormat(decimal amountToPayValue)
        {
            // Validar que no sea negativo
            if (amountToPayValue < 0)
            {
                throw new ArgumentException("El monto a pagar no puede ser negativo");
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is PaymentDetailAmountToPay other)
            {
                return amountToPayValue == other.amountToPayValue;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return amountToPayValue.GetHashCode();
        }
        public override string ToString()
        {
            return amountToPayValue.ToString();
        }
        public decimal GetValue()
        {
            return amountToPayValue;
        }
        public string GetValueAsString()
        {
            return amountToPayValue.ToString();
        }
    }
}
