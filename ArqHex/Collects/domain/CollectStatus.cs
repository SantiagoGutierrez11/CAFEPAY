using System;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectStatus
    {
        public int collectStatus { get; }

        public CollectStatus(int _collectorStatusValue)
        {
            this.collectStatus = _collectorStatusValue;
        }
    }
}