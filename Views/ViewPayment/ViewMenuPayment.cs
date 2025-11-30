using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewPayment
{
    public partial class ViewMenuPayment : Form
    {
        public ViewMenuPayment()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            ViewPayment viewPayment = new ViewPayment(this.Owner);
            viewPayment.Owner = this;
            viewPayment.Show();
            this.Hide();
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            ViewPaymentConsultDelete viewPaymentConsult = new ViewPaymentConsultDelete();
            viewPaymentConsult.Owner = this.Owner; // Obtener el formulario propietario original
            this.Hide();
            viewPaymentConsult.Show();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
