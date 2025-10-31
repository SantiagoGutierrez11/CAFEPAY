using System;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectStatus
    {
        public int collectStatus { get; }

        public CollectStatus(int _collectorStatusValue)
        {
            ValidateFormat(_collectorStatusValue);
            this.collectStatus = _collectorStatusValue;
        }

        private void ValidateFormat(int statusValue)
        {
            //ESTA VALIDACIÓN YA ESTÁ INCLUIDA
            if (statusValue != 1 && statusValue != 2)
            {
                throw new ArgumentException("El estado del collector debe ser 1 (Activo) o 2 (Inactivo)");
            }
        }

        public bool IsActive()
        {
            return collectStatus == 1;
        }

        public bool IsInactive()
        {
            return collectStatus == 2;
        }

        public string GetStatusText()
        {
            return collectStatus == 1 ? "Activo" : "Inactivo";
        }

        public override bool Equals(object obj)
        {
            if (obj is CollectStatus other)
            {
                return collectStatus == other.collectStatus;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return collectStatus.GetHashCode();
        }

        public override string ToString()
        {
            return GetStatusText();
        }

        public int GetValue()
        {
            return collectStatus;
        }

        public static CollectStatus Active()
        {
            return new CollectStatus(1);
        }

        public static CollectStatus Inactive()
        {
            return new CollectStatus(2);
        }
    }
}