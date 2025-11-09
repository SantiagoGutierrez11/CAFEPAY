using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CAFEPAY.ArqHex.Share.AppServices;

namespace CAFEPAY.Views.ViewCollect
{
    public partial class ViewCollect : Form
    {
        private List<Harvest> harvests;
        private List<HarvestDTO> harvestDTO;
        private Plot plot;


        public ViewCollect()
        {
            InitializeComponent();
            loadHarvestComboBox();
        }

        public void loadHarvestComboBox()
        {
            try
            {
                // Usar el nuevo caso de uso que ya filtra por status
                harvests = AppServices.HarvestServices.queryByStatus.execute(1); // 1 = ACTIVO

                if (harvests == null || harvests.Count == 0)
                {
                    return;
                }

                harvestDTO = HarvestMaper.ToDTOList(harvests);

                if (harvestDTO != null && harvestDTO.Count > 0)
                {
                    harvestDTO.Insert(0, new HarvestDTO
                    {
                        id = null,
                        plotName = "-- Seleccione una cosecha --"
                    });

                    cmbHarvest.DataSource = null;
                    cmbHarvest.DataSource = harvestDTO;
                    cmbHarvest.DisplayMember = "plotName";
                    cmbHarvest.ValueMember = "id";
                    cmbHarvest.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cosechas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ViewCollect_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewCollectDetail viewCollectDetail = new ViewCollectDetail();
            viewCollectDetail.Owner = this;
            viewCollectDetail.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void cmbHarvest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}