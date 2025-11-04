using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectorAmountToPaid
    {
        public long collectPaid { get; }

        public CollectorAmountToPaid(long _collectPaidValue)
        {
            ValidateFormat(_collectPaidValue);
            this.collectPaid = _collectPaidValue;
        }

        private void ValidateFormat(long paidValue)
        {
            // Validar que no sea negativo
            if (paidValue < 0)
            {
                throw new ArgumentException("El monto pagado no puede ser negativo");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectorAmountToPaid other)
            {
                return collectPaid == other.collectPaid;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectPaid.GetHashCode();
        }

        public override string ToString()
        {
            return collectPaid.ToString();
        }

        // Método para acceder al valor
        public long GetValue()
        {
            return collectPaid;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectPaid.ToString();
        }
    }
}