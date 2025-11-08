using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Collects.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                listCollector = AppServices.CollectorServices.queryByStatus.execute(1); // 1 = Activo

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
                // Recuperar el worker code de los recolectores seleccionados usando DataBoundItem
                List<string> collectorWorkerCodes = new List<string>();

                foreach (DataGridViewRow row in dgCollectors.SelectedRows)
                {
                    if (row.DataBoundItem is CollectorDTO collector)
                    {
                        if (!string.IsNullOrWhiteSpace(collector.workerCode))
                        {
                            CollectDTO collect = new CollectDTO
                            {
                                collectId = null,
                                plotId = plotDTO.idPlot,
                                harvestId = harvestDTO.id,
                                collectorWorkerCode = collector.workerCode,
                                collectedKilos = 0,
                                collectDate = DateTime.Today,
                                amountToPaid = 0,
                                isCountable = 0,
                                status = 0,
                                statusText = "ZERO"
                            };
                            collectsZero.Add(collect);
                        }
                    }
                }

                // Validar que se hayan recuperado códigos
                if (collectorWorkerCodes.Count == 0)
                {
                    MessageBox.Show("No se encontraron recolectores válidos para asociar.",
                                  "Advertencia",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Warning);
                    return;
                }

                // Listas para clasificar los resultados
                List<string> exitosos = new List<string>();
                List<string> fallidos = new List<string>();

                // PROCESAR TODAS LAS ASOCIACIONES
                foreach (CollectDTO collectFor in collectsZero)
                {
                    try
                    {
                        AppServices.CollectServices.save.execute(
                            collectFor.collectId,
                            collectFor.collectorWorkerCode,
                            collectFor.collectDate,
                            collectFor.collectedKilos,
                            collectFor.harvestId,
                            collectFor.status,
                            collectFor.amountToPaid,
                            collectFor.plotId,
                            collectFor.isCountable
                        );

                        exitosos.Add($"✓ Recolector {collectFor.collectorWorkerCode} se asoció exitosamente");
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Capturar el mensaje específico del error
                        string errorMsg = ex.Message;

                        // Identificar si es un error de duplicado ZERO
                        if (errorMsg.Contains("Ya existe un registro ZERO") ||
                            errorMsg.Contains("ya está asociado a esta cosecha"))
                        {
                            fallidos.Add($"✗ Recolector {collectFor.collectorWorkerCode} no se pudo asociar porque ya existe una asociación");
                        }
                        else
                        {
                            fallidos.Add($"✗ Recolector {collectFor.collectorWorkerCode} falló: {errorMsg}");
                        }
                    }
                    catch (Exception ex)
                    {
                        fallidos.Add($"✗ Recolector {collectFor.collectorWorkerCode} falló: {ex.Message}");
                    }
                }

                // CONSTRUIR MENSAJE DETALLADO CON RESULTADOS INDIVIDUALES
                StringBuilder mensaje = new StringBuilder();
                mensaje.AppendLine("Resultado de la asociación de recolectores:");
                mensaje.AppendLine();

                // Agregar exitosos
                if (exitosos.Count > 0)
                {
                    mensaje.AppendLine("EXITOSOS:");
                    foreach (string msg in exitosos)
                    {
                        mensaje.AppendLine(msg);
                    }
                    mensaje.AppendLine();
                }

                // Agregar fallidos
                if (fallidos.Count > 0)
                {
                    mensaje.AppendLine("FALLIDOS:");
                    foreach (string msg in fallidos)
                    {
                        mensaje.AppendLine(msg);
                    }
                }

                // DETERMINAR ÍCONO Y TÍTULO SEGÚN RESULTADO
                MessageBoxIcon icono;
                string titulo;

                if (fallidos.Count == 0)
                {
                    // Todos exitosos
                    icono = MessageBoxIcon.Information;
                    titulo = "Éxito";
                }
                else if (exitosos.Count == 0)
                {
                    // Todos fallaron
                    icono = MessageBoxIcon.Error;
                    titulo = "Error";
                }
                else
                {
                    // Resultado mixto
                    icono = MessageBoxIcon.Warning;
                    titulo = "Resultado Parcial";
                }

                MessageBox.Show(
                    mensaje.ToString(),
                    titulo,
                    MessageBoxButtons.OK,
                    icono
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al procesar los recolectores: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void textBoxNombreLote_TextChanged(object sender, EventArgs e) { }

        private void textBoxNumeroCosecha_TextChanged(object sender, EventArgs e) { }
    }
}
