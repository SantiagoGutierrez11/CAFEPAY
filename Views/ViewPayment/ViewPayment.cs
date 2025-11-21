using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Collects.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
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

namespace CAFEPAY.Views.ViewPayment
{
    public partial class ViewPayment : Form
    {
        private List<Harvest> harvests; // Lista de cosechas del dominio
        private List<HarvestDTO> harvestsDTO; // Lista de cosechas en formato DTO
        private List<Collector> collectors; // Lista de recolectores del dominio
        private List<CollectorDTO> collectorsDTO; // Lista de recolectores en formato DTO
        private HarvestDTO harvestPayment; // Cosecha seleccionada para el pago
        private CollectorDTO collectorPayment; // Recolector seleccionado para el pago
        private List<Collect> collects; // Lista de recolectas del dominio
        private List<CollectDTO> collectsDTO; // Lista de recolectas en formato DTO
        private Form viewMenuPayment; // Referencia al formulario del menú de pagos
        public ViewPayment(Form _viewMenuPayment)
        {
            InitializeComponent(); // Inicializa los componentes del formulario
            loadHarvestComboBox(); // Carga el ComboBox de cosechas
            loadDgvCollects(); // Configura el DataGridView para mostrar las recolectas
            this.viewMenuPayment = _viewMenuPayment;
        }
        public void loadDgvCollects()
        {
            try
            {
                // Limpiar configuración previa
                dgvCollects.Columns.Clear();
                dgvCollects.AutoGenerateColumns = false;
                dgvCollects.AllowUserToAddRows = false;
                dgvCollects.ReadOnly = true;
                dgvCollects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
 

                // === Configurar columnas manualmente ===
                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Numero de recolecta",
                    DataPropertyName = "collectId",
                    Width = 90,
                    SortMode = DataGridViewColumnSortMode.Automatic
                });

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Fecha Recolecta",
                    DataPropertyName = "collectDate",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                    SortMode = DataGridViewColumnSortMode.Automatic
                });

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Kilos Recolectados",
                    DataPropertyName = "collectedKilos",
                    Width = 130,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                    SortMode = DataGridViewColumnSortMode.Automatic
                });

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Monto a Pagar",
                    DataPropertyName = "amountToPaid",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "C2" },
                    SortMode = DataGridViewColumnSortMode.Automatic
                });

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Estado",
                    DataPropertyName = "statusText",
                    Width = 100,
                    SortMode = DataGridViewColumnSortMode.Automatic
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar columnas del DataGridView: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void loadCollectors(long idPlot, long idHarvest)
        {

            try
            {
                // 1. Obtener las recolectas zero (asociaciones primarias)
                var collectsZero = AppServices.CollectServices.queryByStatus.execute(0, 0, idPlot, idHarvest);

                if (collectsZero == null || collectsZero.Count == 0)
                {
                    cmbCollectors.DataSource = null;
                    cmbCollectors.Items.Clear(); // Limpiar items
                    cmbCollectors.Text = string.Empty; // Limpiar texto
                    dgvCollects.DataSource = null; // Limpiar DataGridView
                    MessageBox.Show("No hay recolectores asociados a esta cosecha.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. Extraer los workerCode de las recolectas
                List<string> workerCodes = new List<string>();
                foreach (var collect in collectsZero)
                {
                    if (!string.IsNullOrEmpty(collect.collectorWorkerCode.collectorWorkerCode))
                    {
                        workerCodes.Add(collect.collectorWorkerCode.collectorWorkerCode);
                    }
                }

                // Verificar si hay códigos válidos
                if (workerCodes.Count == 0)
                {
                    cmbCollectors.DataSource = null;
                    cmbCollectors.Items.Clear();
                    cmbCollectors.Text = string.Empty;
                    dgvCollects.DataSource = null;
                    MessageBox.Show("No se encontraron códigos de trabajadores válidos.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 3. Crear string con los workerCodes 
                string workerCodesString = string.Join(",", workerCodes.Select(code => $"'{code}'"));

                // 4. Consultar los recolectores
                var collectors = AppServices.CollectorServices.queryByIn.execute(workerCodesString);

                if (collectors == null || collectors.Count == 0)
                {
                    cmbCollectors.DataSource = null;
                    MessageBox.Show("No se encontraron recolectores.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                // 5. Mapear a DTO si es necesario
                var collectorsDTO = CollectorMaper.ToDTOList(collectors);

                // Agregar opción por defecto
                collectorsDTO.Insert(0, new CollectorDTO
                {
                    displayName = "-- Seleccione un recolector --",

                });

                //Mostrar ID + nombre + apellido
                cmbCollectors.DataSource = null;
                cmbCollectors.DataSource = collectorsDTO;
                cmbCollectors.DisplayMember = "displayName";
                cmbCollectors.ValueMember = null;
                cmbCollectors.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar recolectores: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCollectors.DataSource = null;
                cmbCollectors.Items.Clear();
                cmbCollectors.Text = string.Empty;
                dgvCollects.DataSource = null;
            }
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
                harvestsDTO = HarvestMaper.ToDTOList(harvests);
                if (harvestsDTO != null && harvestsDTO.Count > 0)
                {
                    harvestsDTO.Insert(0, new HarvestDTO
                    {
                        harvestName = "-- Seleccione una cosecha --"
                    });
                    cmbHarvests.DataSource = null;
                    cmbHarvests.DataSource = harvestsDTO;
                    cmbHarvests.DisplayMember = "harvestName";
                    cmbHarvests.ValueMember = null; // No se usa ValueMember
                    cmbHarvests.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cosechas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ViewPayment_Load(object sender, EventArgs e)
        {

        }

        private void cmbHarvests_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtener la cosecha seleccionada
            if (cmbHarvests.SelectedItem is HarvestDTO selectedHarvest && selectedHarvest.id != null)
            {
                loadCollectors(selectedHarvest.idPlot, selectedHarvest.id.Value);
                harvestPayment = selectedHarvest;
                dgvCollects.DataSource = null;
            }
            else
            {
                // Si seleccionó "-- Seleccione una cosecha --", limpiar el combo de recolectores
                cmbCollectors.DataSource = null;
            }
        }

        private void cmbCollectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (collects != null)
            {
                collects.Clear();
            }
            if (collectsDTO != null)
            {
                collectsDTO.Clear();
            }

            // Verificar si hay una cosecha seleccionada válida
            if (!(cmbHarvests.SelectedItem is HarvestDTO selectedHarvest) ||
                selectedHarvest.harvestName == "-- Seleccione una cosecha --")
            {
                dgvCollects.DataSource = null;
                return;
            }

            // Verificar si hay un recolector seleccionado válido
            if (!(cmbCollectors.SelectedItem is CollectorDTO selectedCollector) ||
                selectedCollector.displayName == "-- Seleccione un recolector --")
            {
                // Limpiar el DataGridView cuando se selecciona la opción por defecto
                dgvCollects.DataSource = null;
                dgvCollects.Refresh();
                collectorPayment = null; // Limpiar el recolector de pago
                return;
            }

            try
            {
                // Cargar las recolectas del recolector seleccionado
                collects = AppServices.CollectServices.queryByStatusAndWorkerCode.execute(
                    1,
                    selectedCollector.workerCode,
                    1,
                    selectedHarvest.idPlot,
                    selectedHarvest.id.Value);
                if (collects == null || collects.Count == 0)
                {
                    dgvCollects.DataSource = null;
                    MessageBox.Show("No hay recolectas a pagar para este recolector en la cosecha seleccionada.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    collectorPayment = null;
                    return;
                }

                collectsDTO = CollectMaper.ToDTOList(collects);
                var sortableList = new BindingList<CollectDTO>(collectsDTO);

                dgvCollects.DataSource = sortableList;
                dgvCollects.ClearSelection();
                ReEnableSorting();
                dgvCollects.CurrentCell = null;

                collectorPayment = selectedCollector;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las recolectas: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                dgvCollects.DataSource = null;
                collectorPayment = null;
            }
        }

        public void loadDataGridView()
        {
            try
            {
                // Cargar las recolectas del recolector seleccionado
                collects = AppServices.CollectServices.queryByStatusAndWorkerCode.execute(
                    1,
                    collectorPayment.workerCode,
                    1,
                    harvestPayment.idPlot,
                    harvestPayment.id.Value);

                collectsDTO = CollectMaper.ToDTOList(collects);
                var sortableList = new BindingList<CollectDTO>(collectsDTO);

                dgvCollects.DataSource = sortableList;
                dgvCollects.ClearSelection();
                ReEnableSorting();
                dgvCollects.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las recolectas: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                dgvCollects.DataSource = null;
                collectorPayment = null;
            }
        }
        private void btnCalculateTotalPayment_Click(object sender, EventArgs e)
        {
            if (harvestPayment == null)
            {
                MessageBox.Show("Debe seleccionar una cosecha antes de calcular el pago.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            if (collectorPayment == null)
            {
                MessageBox.Show("Debe seleccionar un recolector antes de calcular el pago.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            ViewPaymentConfirm viewPaymentConfirm = new ViewPaymentConfirm(harvestPayment, collectorPayment, collectsDTO);
            viewPaymentConfirm.Owner = this;
            viewPaymentConfirm.Show();
            this.Hide();
        }

        private void btnPaymentPartial_Click(object sender, EventArgs e)
        {
            if (harvestPayment == null)
            {
                MessageBox.Show("Debe seleccionar una cosecha antes de calcular el pago.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            if (collectorPayment == null)
            {
                MessageBox.Show("Debe seleccionar un recolector antes de calcular el pago.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            if(dgvCollects.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una recolecta para el pago parcial.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            List<CollectDTO> collectsSelected = new List<CollectDTO>();
            foreach (DataGridViewRow row in dgvCollects.SelectedRows)
            {
                // Obtener el CollectDTO directamente del item enlazado
                if (row.DataBoundItem is CollectDTO collect)
                {
                    CollectDTO collectItem = new CollectDTO()
                    {
                        collectDate = collect.collectDate,
                        collectedKilos = collect.collectedKilos,
                        collectId = collect.collectId,
                        collectorWorkerCode = collect.collectorWorkerCode,
                        isCountable = collect.isCountable,
                        amountToPaid = collect.amountToPaid,
                        harvestId = collect.harvestId,
                        plotId = collect.plotId,
                        status = collect.status,
                        statusText = collect.statusText
                    };
                    collectsSelected.Add(collectItem);
                }
            }
            ViewPaymentConfirm viewPaymentConfirm = new ViewPaymentConfirm(harvestPayment, collectorPayment, collectsSelected);
            viewPaymentConfirm.Owner = this;
            this.Hide();
            viewPaymentConfirm.Show();
            
        }
        private void ReEnableSorting() // Rehabilita la funcionalidad de ordenamiento en las columnas del DataGridView
        {
            foreach (DataGridViewColumn column in dgvCollects.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }

}

