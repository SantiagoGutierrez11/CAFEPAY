using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share.DTO;

namespace CAFEPAY.Views.ViewHarvests
{
    public partial class ViewHarvestAssociateCollector : Form
    {
        private PlotDTO plotDTO;
        private HarvestDTO harvestDTO;

        // 🔹 Constructor que recibe el PlotDTO y HarvestDTO
        public ViewHarvestAssociateCollector(PlotDTO plotDTO, HarvestDTO harvestDTO)
        {
            InitializeComponent();
            this.plotDTO = plotDTO;
            this.harvestDTO = harvestDTO;
        }

        // 🔹 Constructor vacío (por compatibilidad con el diseñador)
        public ViewHarvestAssociateCollector()
        {
            InitializeComponent();
        }

        private void ViewHarvestAssociateCollector_Load(object sender, EventArgs e)
        {
            loadComponents();
        }

        private void loadComponents()
        {
            if (plotDTO != null && harvestDTO != null)
            {
                // Nombre del lote
                textBoxNombreLote.Text = plotDTO.name;

                // Número de cosecha (ID)
                textBoxNumeroCosecha.Text = harvestDTO.id.ToString();

                // Precio por kilo
                textBoxPrecioPorKilo.Text = harvestDTO.pricePerKilo.ToString("C2");

                // Fecha de inicio
                textBoxFechaInicio.Text = harvestDTO.startDate.ToString("dd/MM/yyyy");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lógica del botón "Asociar"
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}