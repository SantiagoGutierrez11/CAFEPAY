using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collectors.domain
{
    public class CollectorStatus
    {
        public int collectorStatus { get; }

        public CollectorStatus(int _collectorStatusValue)
        {
            ValidateFormat(_collectorStatusValue); 
            this.collectorStatus = _collectorStatusValue;
        }

        private void ValidateFormat(int statusValue)
        {
            //ESTA VALIDACIÓN YA ESTÁ INCLUIDA
            if (statusValue != 1 && statusValue != 2)
            {
                throw new ArgumentException("El estado del collector debe ser 1 (Activo) o 2 (Inactivo)");
            }
        }

        // Los demás métodos que ya te pasé (IsActive, GetStatusText, etc.)
        public bool IsActive()
        {
            return collectorStatus == 1;
        }

        public bool IsInactive()
        {
            return collectorStatus == 2;
        }

        public string GetStatusText()
        {
            return collectorStatus == 1 ? "Activo" : "Inactivo";
        }

        public override bool Equals(object obj)
        {
            if (obj is CollectorStatus other)
            {
                return collectorStatus == other.collectorStatus;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectorStatus.GetHashCode();
        }

        public override string ToString()
        {
            return GetStatusText();
        }

        public int GetValue()
        {
            return collectorStatus;
        }

        public static CollectorStatus Active()
        {
            return new CollectorStatus(1);
        }

        public static CollectorStatus Inactive()
        {
            return new CollectorStatus(2);
        }
    }
}