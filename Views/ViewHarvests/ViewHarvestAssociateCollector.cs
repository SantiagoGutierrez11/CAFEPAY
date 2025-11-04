using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share.Serializers;
using System.Linq;

namespace CAFEPAY.Views.ViewHarvests
{
    public partial class ViewHarvestAssociateCollector : Form
    {
        private PlotDTO plotDTO;
        private HarvestDTO harvestDTO;
        private List<Collector> listCollector;
        private List<CollectorDTO> listDTOCollector;

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
            loadCollectors();
        }

        private void loadComponents()
        {
            if (plotDTO != null && harvestDTO != null)
            {
                textBoxPlotName.Text = plotDTO.name;
                textBoxIdHarvest.Text = harvestDTO.id.ToString();
                textBoxPricePerKilo.Text = harvestDTO.pricePerKilo.ToString("C2");
                textBoxStartDate.Text = harvestDTO.startDate.ToString("dd/MM/yyyy");
            }
        }

        // 🔹 CARGAR LOS RECOLECTORES EN EL DATAGRIDVIEW
        public void loadCollectors()
        {
            try
            {
                // 1️⃣ Obtener los recolectores desde el servicio
                listCollector = AppServices.CollectorServices.query.execute();

                // 2️⃣ Convertir a DTOs
                listDTOCollector = CollectorMaper.ToDTOList(listCollector);

                // 3️⃣ Configurar columnas manualmente
                dgCollectors.AutoGenerateColumns = false;
                dgCollectors.Columns.Clear();

                // 4️⃣ Agregar columnas
                AddColumn("workerCode", "ID TRABAJADOR", 120);
                AddColumn("id", "CÉDULA", 120);
                AddColumn("firstName", "NOMBRES", 150);
                AddColumn("lastName", "APELLIDOS", 150);
                AddColumn("phone", "TELÉFONO", 120);

                // Columna de estado
                var statusItems = new[]
                {
                    new { Value = 1, Text = "Activo" },
                    new { Value = 2, Text = "Inactivo" }
                };

                var colStatus = new DataGridViewComboBoxColumn
                {
                    DataPropertyName = "status",
                    HeaderText = "ESTADO",
                    DataSource = statusItems,
                    DisplayMember = "Text",
                    ValueMember = "Value",
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing,
                    FlatStyle = FlatStyle.Flat,
                    Width = 100
                };
                dgCollectors.Columns.Add(colStatus);

                // 5️⃣ Asignar los datos al DataGridView
                dgCollectors.DataSource = listDTOCollector;

                dgCollectors.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los recolectores: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        // 🔹 MÉTODO AUXILIAR PARA AGREGAR COLUMNAS
        private void AddColumn(string dataProperty, string headerText, int width)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width
            };
            dgCollectors.Columns.Add(column);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // VALIDAR QUE HAYA RECOLECTORES SELECCIONADOS
            if (dgCollectors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos un recolector para asociar.",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

            // VALIDAR QUE EXISTA LA INFORMACIÓN DE LA COSECHA
            if (harvestDTO == null)
            {
                MessageBox.Show("No se ha cargado la información de la cosecha.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            try
            {
                List<string> collectorWorkerCodes = new List<string>();

                foreach (DataGridViewRow row in dgCollectors.SelectedRows)
                {
                    if (row.DataBoundItem is CollectorDTO collector)
                    {
                        if (!string.IsNullOrWhiteSpace(collector.workerCode))
                        {
                            collectorWorkerCodes.Add(collector.workerCode.Trim());
                        }
                    }
                }

                if (collectorWorkerCodes.Count == 0)
                {
                    MessageBox.Show("No se pudieron obtener los códigos de los recolectores seleccionados.",
                                  "Error de datos",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar los recolectores: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void textBoxNombreLote_TextChanged(object sender, EventArgs e) { }

        private void textBoxNumeroCosecha_TextChanged(object sender, EventArgs e) { }
    }
}
