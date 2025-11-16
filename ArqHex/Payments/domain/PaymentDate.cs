using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Payments.domain
{
    public class PaymentDate
    {
        public DateTime paymentDate { get; }
            public PaymentDate(DateTime _paymentDateValue)
            {
                ValidateFormat(_paymentDateValue);
                this.paymentDate = _paymentDateValue;
            }
    
            private void ValidateFormat(DateTime dateValue)
            {
                // Validar que no sea una fecha futura
                if (dateValue.Date > DateTime.Now.Date)
                {
                    throw new ArgumentException("La fecha de pago no puede ser futura");
                }
    
                // Validar que no sea la fecha mínima de DateTime (01/01/0001)
                if (dateValue == DateTime.MinValue)
                {
                    throw new ArgumentException("La fecha de pago no es válida");
                }
            }
    
            // Sobrescribir métodos para comparación
            public override bool Equals(object obj)
            {
                if (obj is PaymentDate other)
                {
                    return paymentDate == other.paymentDate;
                }
                return false;
            }
    
            public override int GetHashCode()
            {
                return paymentDate.GetHashCode();
            }
    
            public override string ToString()
            {
                return paymentDate.ToString("dd/MM/yyyy");
            }
    
            // Método para acceder al valor
            public DateTime GetValue()
            {
                return paymentDate;
        }
        public string getValueAsString(string format = "dd/MM/yyyy")
        {
            return paymentDate.ToString(format);
        }
    }
}
