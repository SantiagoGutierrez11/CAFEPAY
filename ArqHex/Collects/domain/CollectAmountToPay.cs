using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectorAmountToPaid
    {
        public long collectAmountToPaidValue { get; }

        public CollectorAmountToPaid(long _collectAmountToPaidValueValue)
        {
            ValidateFormat(_collectAmountToPaidValueValue);
            this.collectAmountToPaidValue = _collectAmountToPaidValueValue;
        }

        private void ValidateFormat(long amountToPaidValue)
        {
            // Validar que no sea negativo
            if (amountToPaidValue < 0)
            {
                throw new ArgumentException("El monto pagado no puede ser negativo");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectorAmountToPaid other)
            {
                return collectAmountToPaidValue == other.collectAmountToPaidValue;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectAmountToPaidValue.GetHashCode();
        }

        public override string ToString()
        {
            return collectAmountToPaidValue.ToString();
        }

        // Método para acceder al valor
        public long GetValue()
        {
            return collectAmountToPaidValue;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectAmountToPaidValue.ToString();
        }
    }
}