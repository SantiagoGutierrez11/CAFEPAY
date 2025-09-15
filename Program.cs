
using CAFEPAY.Views.ViewCollector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY
{
    public static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewCollector());
            /*
                        ViewCollector viewCollector = new ViewCollector();
            viewCollector.Show();
            */

            //)FJnTmGvIIkti?L]
        }
    }
}
