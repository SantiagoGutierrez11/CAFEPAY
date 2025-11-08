using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Collects.domain
{
    public class CollectId
    {
        public long? collectId { get; }

        public CollectId(long? _collectIdValue)
        {
            ValidateFormat();
            this.collectId = _collectIdValue;
        }

        private void ValidateFormat()
        {
            // Validar que no sea negativo
            if (collectId < 0)
            {
                throw new ArgumentException("El ID de recolección no puede ser negativo");
            }

            // Validar que no sea cero
            if (collectId == 0)
            {
                throw new ArgumentException("El ID de recolección no puede ser cero");
            }
        }
    }
}