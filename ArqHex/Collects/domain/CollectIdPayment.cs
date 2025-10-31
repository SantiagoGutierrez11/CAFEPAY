using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectIdPayment
    {
        public long collectIdPayment { get; }

        public CollectIdPayment(long _collectIdPaymentValue)
        {
            ValidateFormat(_collectIdPaymentValue);
            this.collectIdPayment = _collectIdPaymentValue;
        }

        private void ValidateFormat(long idValue)
        {
            // Validar que no sea negativo
            if (idValue < 0)
            {
                throw new ArgumentException("El ID de pago no puede ser negativo");
            }

            // Validar que no sea cero
            if (idValue == 0)
            {
                throw new ArgumentException("El ID de pago no puede ser cero");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectIdPayment other)
            {
                return collectIdPayment == other.collectIdPayment;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectIdPayment.GetHashCode();
        }

        public override string ToString()
        {
            return collectIdPayment.ToString();
        }

        // Método para acceder al valor
        public long GetValue()
        {
            return collectIdPayment;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectIdPayment.ToString();
        }
    }
}