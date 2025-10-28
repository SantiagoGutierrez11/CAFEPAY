using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorPhone
    {
        public string collectorPhone { get; }

        // Constructor para string
        public CollectorPhone(string _collectorPhoneValue)
        {
            if (string.IsNullOrWhiteSpace(_collectorPhoneValue))
            {
                throw new ArgumentException("El número de teléfono no puede estar vacío");
            }

            ValidateFormat(_collectorPhoneValue);
            this.collectorPhone = _collectorPhoneValue.Trim();
        }

        // Constructor sobrecargado para long
        public CollectorPhone(long _collectorPhoneValue)
        {
            string phoneString = _collectorPhoneValue.ToString();
            ValidateFormat(phoneString);
            this.collectorPhone = phoneString;
        }

        private void ValidateFormat(string phoneValue)
        {
            string cleanPhone = phoneValue.Trim();

            // Validar longitud exacta de 10 dígitos
            if (cleanPhone.Length != 10)
            {
                 throw new ArgumentException("El número de teléfono debe tener exactamente 10 dígitos");
            }

            // Validar que solo contenga dígitos
            if (!cleanPhone.All(char.IsDigit))
            {
                throw new ArgumentException("El número de teléfono solo puede contener dígitos");
            }

            // Validar que no empiece con 0
            if (cleanPhone.StartsWith("0"))
            {
                throw new ArgumentException("El número de teléfono no puede empezar con 0");
            }

            // Validar que no sean todos los números iguales
            if (AreAllDigitsSame(cleanPhone))
            {
                throw new ArgumentException("El número de teléfono no puede tener todos los dígitos iguales");
            }

            //NUEVA VALIDACIÓN: Debe empezar con 3
            if (!cleanPhone.StartsWith("3"))
            {
                throw new ArgumentException("Teléfono con formato inválido");
            }

            //NUEVA VALIDACIÓN: Segundo dígito debe ser 0, 1 o 2
            char secondDigit = cleanPhone[1];
            if (secondDigit != '0' && secondDigit != '1' && secondDigit != '2')
            {
                throw new ArgumentException("Teléfono con formato inválido");
            }

        }

        private bool AreAllDigitsSame(string phone)
        {
            // Verificar si todos los dígitos son iguales
            return phone.Length > 0 && phone.All(c => c == phone[0]);
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            return obj is CollectorPhone other && collectorPhone == other.collectorPhone;
        }

        public override int GetHashCode()
        {
            return collectorPhone.GetHashCode();
        }

        public override string ToString()
        {
            return collectorPhone;
        }

        // Método para acceder al valor
        public string GetValue()
        {
            return collectorPhone;
        }
    }
}