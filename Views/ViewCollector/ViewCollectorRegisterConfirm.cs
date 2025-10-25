using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollectorRegisterConfirm : Form
    {
        private CollectorDTO collectorDTO;
        public ViewCollectorRegisterConfirm(CollectorDTO _collectorDTO)
        {
            this.collectorDTO = _collectorDTO;
            InitializeComponent();
            loadLabel();
        }

        public void loadLabel()
        {
            lbWorkerCode.Text = collectorDTO.workerCode;
            lbId.Text = collectorDTO.id.ToString();
            lbFirstName.Text = collectorDTO.firstName;
            lbLastName.Text = collectorDTO.lastName;
            lbPhone.Text = collectorDTO.phone.ToString();
            if(collectorDTO.status == 1)
            {
                lbStatus.Text = "Activo";
            }
            else
            {
                lbStatus.Text = "Inactivo";
            }
        }
        private void ViewCollectorModifyConfirm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (this.Owner is ViewCollector parent)
            {
                parent.loadCollectors();
                this.Owner?.Show();
                this.Close();
            }
            else
            {
                this.Owner?.Show();
            }
            
        }

        private void lbId_Click(object sender, EventArgs e)
        {

        }
    }
}
