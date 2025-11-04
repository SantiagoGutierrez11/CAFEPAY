using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectIdHarvest
    {
        public long collectIdHarvest { get; }

        public CollectIdHarvest(long _collectIdHarvestValue)
        {
            ValidateFormat(_collectIdHarvestValue);
            this.collectIdHarvest = _collectIdHarvestValue;
        }

        private void ValidateFormat(long idValue)
        {
            // Validar que no sea negativo
            if (idValue < 0)
            {
                throw new ArgumentException("El ID de cosecha no puede ser negativo");
            }

            // Validar que no sea cero
            if (idValue == 0)
            {
                throw new ArgumentException("El ID de cosecha no puede ser cero");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectIdHarvest other)
            {
                return collectIdHarvest == other.collectIdHarvest;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectIdHarvest.GetHashCode();
        }

        public override string ToString()
        {
            return collectIdHarvest.ToString();
        }

        // Método para acceder al valor
        public long GetValue()
        {
            return collectIdHarvest;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectIdHarvest.ToString();
        }
    }
}