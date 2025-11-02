using CAFEPAY.ArqHex.Harvests.Domain;
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

namespace CAFEPAY.Views.ViewHarvest
{
    public partial class ViewHarvestFinishConfirm : Form
    {
        private HarvestDTO harvestDTO;
        private PlotDTO plotOfHarvest;
        public ViewHarvestFinishConfirm(PlotDTO _plotDTO, HarvestDTO _harvestDTO)
        {
            plotOfHarvest = _plotDTO;
            harvestDTO = _harvestDTO;
            InitializeComponent();
            loadCompoents();

        }
        public void loadCompoents()
        {
            textBoxIdPlot.Text = plotOfHarvest.idPlot.ToString();
            textBoxPlotName.Text = plotOfHarvest.name;
            textBoxIdHarvest.Text = harvestDTO.id.ToString();
            textBoxStartDate.Text = harvestDTO.startDate.ToString("dd/MM/yyyy");
            textBoxEndDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            textBoxPricePerKilo.Text = harvestDTO.pricePerKilo.ToString("C2");

        }
        private void ViewHarvestFinishConfirm_Load(object sender, EventArgs e)
        {

        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            if (this.Owner is ViewHarvest parent)
            {
                parent.loadHarvests();
                this.Owner?.Show();
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                AppServices.HarvestServices.update.execute(harvestDTO.id, harvestDTO.idPlot, harvestDTO.startDate, DateTime.Today, harvestDTO.pricePerKilo, 2);
                MessageBox.Show($"Se ha finalizado la cosecha correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(this.Owner is ViewHarvest parent)
                {
                    parent.loadHarvests();
                    this.Owner.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error finishing harvest: " + ex.Message);
                return;
            }
        }
    }
}
