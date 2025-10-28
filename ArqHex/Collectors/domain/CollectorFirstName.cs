using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorFirstName
    {
        public string collectorFirstName { get; }  

        public CollectorFirstName(string _collectorFirstName)
        {
            if (string.IsNullOrWhiteSpace(_collectorFirstName))
            {
                throw new ArgumentException("El nombre no puede estar vacío o ser nulo");
            }

            ValidateFormat(_collectorFirstName);
            this.collectorFirstName = _collectorFirstName.Trim();
        }

        private void ValidateFormat(string firstName)
        {
            // Longitud mínima y máxima
            if (firstName.Length < 3)
            {
                throw new ArgumentException("El nombre debe tener al menos 3 caracteres");
            }

            if (firstName.Length > 30)
            {
                throw new ArgumentException("El nombre no puede tener más de 30 caracteres");
            }

            // Solo letras, espacios y algunos caracteres especiales comunes en nombres
            if (!Regex.IsMatch(firstName, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s\'-]+$"))
            {
                throw new ArgumentException("El nombre solo puede contener letras, espacios, apóstrofes y guiones");
            }

            // No puede empezar o terminar con espacios, puntos, guiones
            if (firstName.StartsWith(" ") || firstName.EndsWith(" ") ||
                firstName.StartsWith(".") || firstName.EndsWith(".") ||
                firstName.StartsWith("-") || firstName.EndsWith("-") ||
                firstName.StartsWith("'") || firstName.EndsWith("'"))
            {
                throw new ArgumentException("El nombre no puede empezar o terminar con espacios, puntos, guiones o apóstrofes");
            }

            // No puede tener espacios múltiples consecutivos
            if (Regex.IsMatch(firstName, @"\s{2,}"))
            {
                throw new ArgumentException("El nombre no puede tener espacios múltiples consecutivos");
            }

            // Validar que tenga al menos una letra (no solo espacios o caracteres especiales)
            if (!Regex.IsMatch(firstName, @"[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]"))
            {
                throw new ArgumentException("El nombre debe contener al menos una letra");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectorFirstName other)
            {
                return collectorFirstName == other.collectorFirstName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectorFirstName?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return collectorFirstName;
        }

        // Método para acceder al valor (opcional, por si necesitas una propiedad con otro nombre)
        public string GetValue()
        {
            return collectorFirstName;
        }
    }
}

