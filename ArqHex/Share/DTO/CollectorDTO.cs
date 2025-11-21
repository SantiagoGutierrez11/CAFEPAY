using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class CollectorDTO
    {
        public string workerCode { get; set; }
        public long id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phone { get; set; }
        public int status { get; set; }         // 1 = activo , 2 = inactivo
        public string statusText { get; set; }

        private string _displayName;

        public string displayName
        {
            get
            {
                // Si tiene un valor manual (como "-- Seleccione un recolector --"), usarlo directamente
                if (!string.IsNullOrEmpty(_displayName))
                    return _displayName;

                // Si no, construirlo normalmente con los datos
                return $"{id} - {firstName} {lastName}";
            }
            set
            {
                _displayName = value;
            }
        }
    }
}
