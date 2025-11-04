using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectorAmountToPaid
    {
        public long collectorAmountToPaid { get; }

        public CollectorAmountToPaid(long _collectorAmountToPaidValue)
        {
            ValidateFormat(_collectorAmountToPaidValue);
            this.collectorAmountToPaid = _collectorAmountToPaidValue;
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
                return collectorAmountToPaid == other.collectorAmountToPaid;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectorAmountToPaid.GetHashCode();
        }

        public override string ToString()
        {
            return collectorAmountToPaid.ToString();
        }

        // Método para acceder al valor
        public long GetValue()
        {
            return collectorAmountToPaid;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectorAmountToPaid.ToString();
        }
    }
}