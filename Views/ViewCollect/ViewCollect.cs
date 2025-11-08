using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }
        public void loadHarvestComboBox()
        {
            harvests = AppServices.HarvestServices.query.execute();
            harvestDTO = HarvestMaper.ToDTOList(harvests); 
            foreach (var harvest in harvestDTO)
            {
                Console.WriteLine("Harvest ID: " + harvest.id + ", Plot Name: " + harvest.plotName);
            }
            cmbHarvest.DisplayMember = "plotName";
            cmbHarvest.ValueMember = "id" + "id lote";
        }

        private void ViewCollect_Load(object sender, EventArgs e)
        {
            loadCollectors();
        }

        // CARGAR LOS RECOLECTORES
        private void loadCollectors()
        {
            try
            {
                listCollector = AppServices.CollectorServices.query.execute();
                listDTOCollector = CollectorMaper.ToDTOList(listCollector);

                dgCollectors.AutoGenerateColumns = false;
                dgCollectors.Columns.Clear();

        private void button1_Click(object sender, EventArgs e)
        {
            ViewCollectDetail viewCollectDetail = new ViewCollectDetail();
            viewCollectDetail.Owner = this;
            viewCollectDetail.Show();
            this.Hide();
        }

        // MÉTODO AUXILIAR PARA AGREGAR COLUMNAS AL DATAGRIDVIEW
        private void AddColumn(string dataProperty, string headerText)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText
            };
            dgCollectors.Columns.Add(column);
        }

        // Botón "Recargar"
        private void button1_Click(object sender, EventArgs e)
        {
            loadCollectors();
        }

        // Botón "Eliminar" (sin lógica aún)
        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void cmbHarvest_SelectedIndexChanged(object sender, EventArgs e)
        {

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            // Vacío
        }
    }
}
