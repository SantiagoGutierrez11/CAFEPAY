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
                textBoxNombreLote.Text = plotDTO.name;
                // Número de cosecha (ID)
                textBoxNumeroCosecha.Text = harvestDTO.id.ToString();
                // Precio por kilo
                textBoxPrecioPorKilo.Text = harvestDTO.pricePerKilo.ToString("C2");
                // Fecha de inicio
                textBoxFechaInicio.Text = harvestDTO.startDate.ToString("dd/MM/yyyy");
            }
        }

        // 🔹 CONFIGURAR EL DATAGRIDVIEW CON EL ESTILO
        private void ConfigureDataGridView()
        {
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.BackgroundColor = whiteColor;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = true; // ✅ Permitir selección múltiple
            dataGridView1.ReadOnly = true;

            // Estilo de encabezados
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dataGridView1.ColumnHeadersHeight = 40;

            // Estilo de celdas
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridView1.DefaultCellStyle.BackColor = whiteColor;
            dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridView1.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dataGridView1.RowTemplate.Height = 40;
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
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.Columns.Clear();

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
                dataGridView1.Columns.Add(colStatus);

                // 5️⃣ ASIGNAR LOS DATOS AL DATAGRIDVIEW ✅
                dataGridView1.DataSource = listDTOCollector;

                // Limpiar selección inicial
                dataGridView1.ClearSelection();
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
            dataGridView1.Columns.Add(column);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 🔹 VALIDAR QUE HAYA RECOLECTORES SELECCIONADOS
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos un recolector para asociar.",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

            // 🔹 VALIDAR QUE EXISTA LA INFORMACIÓN DE LA COSECHA
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
                // 🔹 OBTENER TODOS LOS RECOLECTORES SELECCIONADOS
                List<CollectorDTO> selectedCollectors = new List<CollectorDTO>();
                List<string> collectorNames = new List<string>();

                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    if (row.Index >= 0 && row.Index < listDTOCollector.Count)
                    {
                        var collector = listDTOCollector[row.Index];
                        if (collector != null)
                        {
                            selectedCollectors.Add(collector);
                            collectorNames.Add($"{collector.firstName} {collector.lastName}");
                        }
                    }
                }

                // 🔹 CONFIRMAR LA ASOCIACIÓN
                string collectorsText = string.Join("\n", collectorNames);
                DialogResult result = MessageBox.Show(
                    $"¿Está seguro de asociar los siguientes recolectores a la cosecha #{harvestDTO.id}?\n\n" +
                    $"{collectorsText}\n\n" +
                    $"Total: {selectedCollectors.Count} recolector(es)",
                    "Confirmar asociación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                // 🔹 ASOCIAR CADA RECOLECTOR A LA COSECHA
                int successCount = 0;
                int errorCount = 0;
                List<string> errors = new List<string>();

                foreach (var collector in selectedCollectors)
                {
                    try
                    {
                        // 🔹 AQUÍ VA TU LÓGICA DE ASOCIACIÓN
                        // Ejemplo con tu servicio (ajusta según tu arquitectura):
                        // AppServices.HarvestCollectorServices.associate.execute(harvestDTO.id, collector.id);

                        // 🔹 O si usas un objeto HarvestCollector:
                        /*
                        HarvestCollector harvestCollector = new HarvestCollector
                        {
                            harvestId = harvestDTO.id,
                            collectorId = collector.id,
                            // Otros campos necesarios...
                        };
                        AppServices.HarvestCollectorServices.save.execute(harvestCollector);
                        */

                        successCount++;
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        errors.Add($"- {collector.firstName} {collector.lastName}: {ex.Message}");
                    }
                }

                // 🔹 MOSTRAR RESULTADO
                if (errorCount == 0)
                {
                    MessageBox.Show(
                        $"✅ {successCount} recolector(es) asociado(s) exitosamente a la cosecha #{harvestDTO.id}",
                        "Asociación exitosa",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // Limpiar selección
                    dataGridView1.ClearSelection();

                    // Opcional: Cerrar el formulario y volver al anterior
                    // this.Close();
                }
                else
                {
                    string errorMessage = $"Asociación completada con errores:\n\n" +
                                        $"✅ Exitosos: {successCount}\n" +
                                        $"❌ Errores: {errorCount}\n\n" +
                                        $"Detalles de errores:\n" +
                                        string.Join("\n", errors);

                    MessageBox.Show(errorMessage,
                                  "Asociación con errores",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al asociar recolectores: {ex.Message}",
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
    }
}