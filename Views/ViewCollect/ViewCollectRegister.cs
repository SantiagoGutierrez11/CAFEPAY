using CAFEPAY.ArqHex.Share;
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

namespace CAFEPAY.Views.ViewCollect
{
    public partial class ViewCollectRegister : Form
    {
        private CollectorDTO collectorRegister;
        private HarvestDTO harvestRegister;
        public ViewCollectRegister(HarvestDTO _harvestRegister, CollectorDTO _collectorRegister)
        {
            InitializeComponent();
            this.harvestRegister = _harvestRegister;
            this.collectorRegister = _collectorRegister;
            loadData();
        }

        public void loadData()
        {
            textBoxIdHarvest.Text = harvestRegister.id.ToString();
            textBoxPlotName.Text = harvestRegister.plotName;
            textBoxWorkerName.Text = collectorRegister.firstName + " " + collectorRegister.lastName;
            textBoxWorkerCode.Text = collectorRegister.workerCode;
            textBoxIdWorker.Text = collectorRegister.id.ToString();
            textBoxIdPlot.Text = harvestRegister.idPlot.ToString();

            var today = DateTime.Today;
            dtpCollectDate.MaxDate = today;

            dtpCollectDate.MinDate = today;

            dtpCollectDate.Value = today;
        }

        private void ViewCollectDetail_Load(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxKilos.Text)){
               
                CollectDTO collectRegister = new CollectDTO()
                {
                    collectId = null,
                    collectorWorkerCode = collectorRegister.workerCode,
                    plotId = harvestRegister.idPlot,
                    harvestId = harvestRegister.id,
                    collectDate = DateTime.Now,
                    collectedKilos = decimal.Parse(textBoxKilos.Text),
                    amountToPaid = null,
                    status = 1,
                    isCountable = 1,
                    statusText = "Registrado"
                };
                ViewCollectRegisterConfirm viewCollectRegisterConfirm = new ViewCollectRegisterConfirm(collectorRegister, harvestRegister, collectRegister, this.Owner);
                viewCollectRegisterConfirm.Owner = this;
                this.Hide();
                viewCollectRegisterConfirm.Show();
            }
            else
            {
                MessageBox.Show("El campo 'Kilos Recogidos' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
