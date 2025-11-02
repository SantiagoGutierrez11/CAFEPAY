using CAFEPAY.Views.ViewCollector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewOrigin
{
    public partial class ViewMain : Form
    {
        public ViewMain()
        {
            InitializeComponent();
        }

        private void btnCollectors_Click(object sender, EventArgs e)
        {
            ViewCollector.ViewCollector viewCollectors = new ViewCollector.ViewCollector();
            viewCollectors.Owner = this;
            viewCollectors.Show();
            this.Hide();
        }

        private void btnHarvests_Click(object sender, EventArgs e)
        {
            ViewHarvest.ViewHarvest viewHarvest = new ViewHarvest.ViewHarvest();
            viewHarvest.Owner = this;
            viewHarvest.Show();
            this.Hide();
        }
    }
}
