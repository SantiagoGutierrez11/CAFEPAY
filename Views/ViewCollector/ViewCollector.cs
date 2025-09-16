using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollector : Form
    {
        public ViewCollector()
        {
            InitializeComponent();
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ViewCollectorDetail viewCollectorDetail = new ViewCollectorDetail();
            viewCollectorDetail.Owner = this;
            viewCollectorDetail.Show();
            this.Hide();
        }

        private void dgCollector_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewCollector_Load(object sender, EventArgs e)
        {

        }
    }
}
