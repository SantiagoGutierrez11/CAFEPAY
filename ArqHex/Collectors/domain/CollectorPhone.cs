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
        public long collectorPhone { get; }

        public CollectorPhone(long _collectorPhoneValue)
        {
            ValidateFormat(_collectorPhoneValue);
            this.collectorPhone = _collectorPhoneValue;
        }

        private void ValidateFormat(long phoneValue)
        {
            // Convertir a string para validaciones de formato
            string phoneString = phoneValue.ToString();

            // Longitud exacta de 10 dígitos
            if (phoneString.Length != 10)
            {
                throw new ArgumentException("El número de teléfono debe tener exactamente 10 dígitos");
            }

            // Validar que no empiece con 0
            if (phoneString.StartsWith("0"))
            {
                throw new ArgumentException("El número de teléfono no puede empezar con 0");
            }

            // Validar que el primer dígito sea válido (2-9 para códigos de área típicos)
            if (!Regex.IsMatch(phoneString.Substring(0, 1), @"[2-9]"))
            {
                throw new ArgumentException("El primer dígito del teléfono debe ser entre 2 y 9");
            }

            // Validar formato general (solo dígitos, ya está garantizado por ser long)
            // Validación adicional: no todos los dígitos iguales (evitar 1111111111, 2222222222, etc.)
            if (phoneString.Distinct().Count() == 1)
            {
                throw new ArgumentException("El número de teléfono no puede tener todos los dígitos iguales");
            }
        }

        // Método para formatear el teléfono
        public string GetFormattedPhone()
        {
            string phoneString = collectorPhone.ToString();
            // Formato: (XXX) XXX-XXXX
            return $"({phoneString.Substring(0, 3)}) {phoneString.Substring(3, 3)}-{phoneString.Substring(6)}";
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectorPhone other)
            {
                return collectorPhone == other.collectorPhone;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectorPhone.GetHashCode();
        }

        public override string ToString()
        {
            return GetFormattedPhone();
        }

        // Método para acceder al valor
        public long GetValue()
        {
            return collectorPhone;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectorPhone.ToString();
        }
    }
}