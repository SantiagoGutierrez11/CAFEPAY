using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share.Serializers;
using System.Linq;
using CAFEPAY.ArqHex.Collects.domain;

namespace CAFEPAY.Views.ViewHarvests
{
    public partial class ViewHarvestAssociateCollector : Form
    {
        private PlotDTO plotDTO;
        private HarvestDTO harvestDTO;
        private List<Collector> listCollector;
        private List<CollectorDTO> listDTOCollector;

        // Colores del diseño
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);
        private Color whiteColor = Color.White;

        // 🔹 Constructor que recibe el PlotDTO y HarvestDTO
        public ViewHarvestAssociateCollector(PlotDTO plotDTO, HarvestDTO harvestDTO)
        {
            InitializeComponent();
            this.plotDTO = plotDTO;
            this.harvestDTO = harvestDTO;
            ConfigureDataGridView();
        }

        // 🔹 Constructor vacío (por compatibilidad con el diseñador)
        public ViewHarvestAssociateCollector()
        {
            InitializeComponent();
            ConfigureDataGridView();
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
                // Nombre del lote
                textBoxPlotName.Text = plotDTO.name;
                // Número de cosecha (ID)
                textBoxIdHarvest.Text = harvestDTO.id.ToString();
                // Precio por kilo
                textBoxPricePerKilo.Text = harvestDTO.pricePerKilo.ToString("C2");
                // Fecha de inicio
                textBoxStartDate.Text = harvestDTO.startDate.ToString("dd/MM/yyyy");
            }
        }

        // 🔹 CONFIGURAR EL DATAGRIDVIEW CON EL ESTILO
        private void ConfigureDataGridView()
        {
            dgCollectors.BorderStyle = BorderStyle.None;
            dgCollectors.BackgroundColor = whiteColor;
            dgCollectors.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgCollectors.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgCollectors.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgCollectors.RowHeadersVisible = false;
            dgCollectors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgCollectors.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgCollectors.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgCollectors.EnableHeadersVisualStyles = false;
            dgCollectors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgCollectors.MultiSelect = true; // Permitir selección múltiple
            dgCollectors.ReadOnly = true;

            // Estilo de encabezados
            dgCollectors.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgCollectors.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgCollectors.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgCollectors.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgCollectors.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgCollectors.ColumnHeadersHeight = 40;

            // Estilo de celdas
            dgCollectors.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgCollectors.DefaultCellStyle.BackColor = whiteColor;
            dgCollectors.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgCollectors.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgCollectors.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgCollectors.RowTemplate.Height = 40;
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

                // 5️⃣ ASIGNAR LOS DATOS AL DATAGRIDVIEW ✅
                dgCollectors.DataSource = listDTOCollector;

                // Limpiar selección inicial
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
                // Recuperar el worker code de los recolectores seleccionados usando DataBoundItem
                List<CollectDTO> collectsZero = new List<CollectDTO>();

                foreach (DataGridViewRow row in dgCollectors.SelectedRows)
                {
                    // Obtener el CollectorDTO directamente del item enlazado
                    if (row.DataBoundItem is CollectorDTO collector)
                    {
                        if (!string.IsNullOrWhiteSpace(collector.workerCode))
                        {
                            CollectDTO collect = new CollectDTO
                            {
                                collectId = null,
                                plotId = plotDTO.idPlot,
                                harvestId = harvestDTO.id,
                                paymentId = null,
                                collectorWorkerCode = collector.workerCode,
                                collectedKilos = 0,
                                collectDate = DateTime.Today,
                                amountToPaid = 0,
                                isCountable = 0,
                                status =0, 
                                statusText = "ZERO"
                            };
                        collectsZero.Add(collect);
                        }
                    }
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

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void textBoxNombreLote_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNumeroCosecha_TextChanged(object sender, EventArgs e)
        {

        }
    }
}