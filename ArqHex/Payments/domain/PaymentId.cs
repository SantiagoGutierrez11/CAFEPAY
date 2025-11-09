using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.domain
{
    public class PaymentId
    {
        public long? id { get; }
        public PaymentId(long? _idValue)
        {
            ValidateFormat();
            this.id = _idValue;
        }
        private void ValidateFormat()
        {
            // Validar que no sea negativo
            if (id < 0)
            {
                throw new ArgumentException("El ID de pago no puede ser negativo");
            }
            // Validar que no sea cero
            if (id == 0)
            {
                throw new ArgumentException("El ID de pago no puede ser cero");
            }
        }
    }
}
