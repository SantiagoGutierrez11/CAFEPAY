using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorWorkerCode
    {
        public string collectorWorkerCode { get; }

        public CollectorWorkerCode(string _collectorWorkerCode)
        {
            ValidateFormat(_collectorWorkerCode);
            this.collectorWorkerCode = _collectorWorkerCode.Trim().ToUpper();
        }

        private void ValidateFormat(string workerCode)
        {
            if (string.IsNullOrWhiteSpace(workerCode))
            {
                throw new ArgumentException("El código de trabajador no puede estar vacío");
            }

            string cleanCode = workerCode.Trim().ToUpper();

            // Longitud exacta de 6 caracteres (W + 5 dígitos)
            if (cleanCode.Length != 6)
            {
                throw new ArgumentException("El código de trabajador debe tener exactamente 6 caracteres (ej: W00001)");
            }

            // Validar que empiece con 'W'
            if (!cleanCode.StartsWith("W"))
            {
                throw new ArgumentException("El código de trabajador debe empezar con 'W'");
            }

            // Validar que los últimos 5 caracteres sean dígitos
            string digitsPart = cleanCode.Substring(1);
            if (!digitsPart.All(char.IsDigit))
            {
                throw new ArgumentException("Los últimos 5 caracteres del código deben ser dígitos");
            }

            // Validar que no sea "W00000"
            if (digitsPart == "00000")
            {
                throw new ArgumentException("El código de trabajador no puede ser W00000");
            }

            // Validar que los dígitos no sean todos cero (opcional)
            if (digitsPart.All(c => c == '0'))
            {
                throw new ArgumentException("El código de trabajador no puede tener todos los dígitos en cero");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectorWorkerCode other)
            {
                return collectorWorkerCode == other.collectorWorkerCode;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectorWorkerCode?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return collectorWorkerCode;
        }

        // Método para acceder al valor
        public string GetValue()
        {
            return collectorWorkerCode;
        }

        // Método para obtener solo la parte numérica
        public int GetNumericPart()
        {
            string digitsPart = collectorWorkerCode.Substring(1);
            return int.Parse(digitsPart);
        }
    }
}