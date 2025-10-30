using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAFEPAY.ArqHex.Share.DTO
{
    public class PlotDTO
    {
        public long idPlot { get; set; }
        public long idOwner { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public string statusText { get; set; }
    }
}
