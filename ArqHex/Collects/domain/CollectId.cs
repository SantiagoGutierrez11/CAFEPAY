using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectId
    {
        public long collectId { get; }

        public CollectId(long _collectIdValue)
        {
            ValidateFormat(_collectIdValue);
            this.collectId = _collectIdValue;
        }

        private void ValidateFormat(long idValue)
        {
            // Validar que no sea negativo
            if (idValue < 0)
            {
                throw new ArgumentException("El ID de recolección no puede ser negativo");
            }

            // Validar que no sea cero
            if (idValue == 0)
            {
                throw new ArgumentException("El ID de recolección no puede ser cero");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectId other)
            {
                return collectId == other.collectId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectId.GetHashCode();
        }

        public override string ToString()
        {
            return collectId.ToString();
        }

        // Método para acceder al valor
        public long GetValue()
        {
            return collectId;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectId.ToString();
        }
    }
}