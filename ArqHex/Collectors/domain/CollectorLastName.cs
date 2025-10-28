using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorLastName
    {
        public string collectorLastName { get; }

        public CollectorLastName(string _collectorLastName)
        {
            if (string.IsNullOrWhiteSpace(_collectorLastName))
            {
                throw new ArgumentException("El apellido no puede estar vacío o ser nulo");
            }

            ValidateFormat(_collectorLastName);
            this.collectorLastName = _collectorLastName.Trim();
        }

        private void ValidateFormat(string lastName)
        {
            // Longitud mínima y máxima
            if (lastName.Length < 3)
            {
                throw new ArgumentException("El apellido debe tener al menos 3 caracteres");
            }

            if (lastName.Length > 30)
            {
                throw new ArgumentException("El apellido no puede tener más de 30 caracteres");
            }

            // Solo letras, espacios y algunos caracteres especiales comunes en apellidos
            if (!Regex.IsMatch(lastName, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s\'-]+$"))
            {
                throw new ArgumentException("El apellido solo puede contener letras, espacios, apóstrofes y guiones");
            }

            // No puede empezar o terminar con espacios, puntos, guiones
            if (lastName.StartsWith(" ") || lastName.EndsWith(" ") ||
                lastName.StartsWith(".") || lastName.EndsWith(".") ||
                lastName.StartsWith("-") || lastName.EndsWith("-") ||
                lastName.StartsWith("'") || lastName.EndsWith("'"))
            {
                throw new ArgumentException("El apellido no puede empezar o terminar con espacios, puntos, guiones o apóstrofes");
            }

            // No puede tener espacios múltiples consecutivos
            if (Regex.IsMatch(lastName, @"\s{2,}"))
            {
                throw new ArgumentException("El apellido no puede tener espacios múltiples consecutivos");
            }

            // Validar que tenga al menos una letra (no solo espacios o caracteres especiales)
            if (!Regex.IsMatch(lastName, @"[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]"))
            {
                throw new ArgumentException("El apellido debe contener al menos una letra");
            }

        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectorLastName other)
            {
                return collectorLastName == other.collectorLastName;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectorLastName?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return collectorLastName;
        }

        // Método para acceder al valor
        public string GetValue()
        {
            return collectorLastName;
        }
    }
}