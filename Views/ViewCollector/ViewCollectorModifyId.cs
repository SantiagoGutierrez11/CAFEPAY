using CAFEPAY.ArqHex.Share.DTO;
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
    public partial class ViewCollectorModifyId : Form
    {
        private Form viewCollector;
        private CollectorDTO newCollector;
        private CollectorDTO oldCollector;
        public ViewCollectorModifyId(CollectorDTO newCollector, CollectorDTO oldCollector, System.Windows.Forms.Form _viewCollector)
        {
            InitializeComponent();
            this.oldCollector = oldCollector;
            this.newCollector = newCollector;
            this.viewCollector = _viewCollector;
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }

        private void ViewCollectorModifyId_Load(object sender, EventArgs e)
        {

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if(long.Parse(textBoxId.Text) == newCollector.id)
            {
                ViewCollectorModifyConfirm_ viewCollectorModifyConfirm_ = new ViewCollectorModifyConfirm_(newCollector,oldCollector, viewCollector);
                viewCollectorModifyConfirm_.Owner = this.Owner;
                viewCollectorModifyConfirm_.Show();
                this.Close();
                
            }
            else
            {
                MessageBox.Show("El ID ingresado no coincide con el nuevo ID del colector a modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxId_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
