using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectDate
    {
        public DateTime collectDate { get; }

        public CollectDate(DateTime _collectDateValue)
        {
            ValidateFormat(_collectDateValue);
            this.collectDate = _collectDateValue;
        }

        private void ValidateFormat(DateTime dateValue)
        {
            // Validar que no sea una fecha futura
            if (dateValue.Date > DateTime.Now.Date)
            {
                throw new ArgumentException("La fecha de recolección no puede ser futura");
            }

            // Validar que no sea la fecha mínima de DateTime (01/01/0001)
            if (dateValue == DateTime.MinValue)
            {
                throw new ArgumentException("La fecha de recolección no es válida");
            }
        }

        // Sobrescribir métodos para comparación
        public override bool Equals(object obj)
        {
            if (obj is CollectDate other)
            {
                return collectDate == other.collectDate;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectDate.GetHashCode();
        }

        public override string ToString()
        {
            return collectDate.ToString("dd/MM/yyyy");
        }

        // Método para acceder al valor
        public DateTime GetValue()
        {
            return collectDate;
        }

        // Método para obtener como string en formato específico
        public string GetValueAsString(string format = "dd/MM/yyyy")
        {
            return collectDate.ToString(format);
        }
    }
}