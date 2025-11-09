using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.ArqHex.PaymentsDetails.domain
{
    public class PaymentsDetailsAmountToPay
    {
        public long PaymentsDetailsAmountToPayValue { get; }
        public PaymentsDetailsAmountToPay(long _PaymentsDetailsAmountToPayValue)
        {
            ValidateFormat(_PaymentsDetailsAmountToPayValue);
            this.PaymentsDetailsAmountToPayValue = _PaymentsDetailsAmountToPayValue;
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
            if (obj is PaymentsDetailsAmountToPay other)
            {
                return PaymentsDetailsAmountToPayValue == other.PaymentsDetailsAmountToPayValue;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return PaymentsDetailsAmountToPayValue.GetHashCode();
        }
        public override string ToString()
        {
            return PaymentsDetailsAmountToPayValue.ToString();
        }
        public long GetValue()
        {
            return PaymentsDetailsAmountToPayValue;
        }
        public string GetValueAsString()
        {
            return PaymentsDetailsAmountToPayValue.ToString();
        }
    }
}
