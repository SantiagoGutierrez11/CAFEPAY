using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectedKilos
    {
        public decimal collectedKilos { get; }

        public CollectedKilos(decimal _collectedKilosValue)
        {
            ValidateFormat(_collectedKilosValue);
            this.collectedKilos = _collectedKilosValue;
        }

        private void ValidateFormat(decimal kilosValue)
        {
            // Validar que no sea negativo
            if (kilosValue < 0)
            {
                throw new ArgumentException("Los kilos recolectados no pueden ser negativos");
            }

            // Validar que sea un valor razonable (por ejemplo, máximo 10,000 kg por recolección)
            if (kilosValue > 10000)
            {
                throw new ArgumentException("Los kilos recolectados exceden el límite permitido (10,000 kg)");
            }

            // Validar que no tenga más de 2 decimales
            if (decimal.Round(kilosValue, 2) != kilosValue)
            {
                throw new ArgumentException("Los kilos recolectados no pueden tener más de 2 decimales");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectedKilos other)
            {
                return collectedKilos == other.collectedKilos;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectedKilos.GetHashCode();
        }

        public override string ToString()
        {
            return collectedKilos.ToString("F2") + " kg";
        }

        // Método para acceder al valor
        public decimal GetValue()
        {
            return collectedKilos;
        }

        // Método para obtener como string
        public string GetValueAsString()
        {
            return collectedKilos.ToString("F2");
        }

        // Método para obtener el valor redondeado
        public decimal GetRoundedValue(int decimals = 2)
        {
            return decimal.Round(collectedKilos, decimals);
        }
    }
}