using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorId
    {
        public long collectorId { get; }

        public CollectorId(long _collectorIdValue)
        {
            ValidateFormat(_collectorIdValue);
            this.collectorId = _collectorIdValue;
        }

        private void ValidateFormat(long idValue)
        {
            // Convertir a string para validar longitud
            string idString = idValue.ToString();

            // Validar longitud entre 8 y 10 dígitos
            if (idString.Length < 8 || idString.Length > 10)
            {
                throw new ArgumentException("La cédula debe tener entre 8 y 10 dígitos");
            }

            // Validar que no sea negativo
            if (idValue < 0)
            {
                throw new ArgumentException("La cédula no puede ser negativa");
            }

            // Validar que no empiece con 0
            if (idString.StartsWith("0"))
            {
                throw new ArgumentException("La cédula no puede empezar con 0");
            }

            // Validar que no sean todos los dígitos iguales
            if (AreAllDigitsSame(idString))
            {
                throw new ArgumentException("La cédula no puede tener todos los dígitos iguales");
            }
        }

        private bool AreAllDigitsSame(string id)
        {
            // Verificar si todos los dígitos son iguales
            return id.Length > 0 && id.All(c => c == id[0]);
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectorId other)
            {
                return collectorId == other.collectorId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectorId.GetHashCode();
        }

        public override string ToString()
        {
            return collectorId.ToString();
        }

        // Método para acceder al valor
        public long GetValue()
        {
            return collectorId;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectorId.ToString();
        }
    }
}