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
        public ViewPayment()
        {
<<<<<<< HEAD
            InitializeComponent();
=======
            InitializeComponent(); // Inicializa los componentes del formulario
            loadHarvestComboBox(); // Carga el ComboBox de cosechas
            loadDgvCollects(); // Configura el DataGridView para mostrar las recolectas
            this.viewMenuPayment = _viewMenuPayment;
            textBoxTotalAmounToPaid.Text = "No hay datos";
>>>>>>> Santiago
        }

        private void ViewPayment_Load(object sender, EventArgs e)
        {

        }
<<<<<<< HEAD
=======

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
                textBoxTotalAmounToPaid.Text = "No hay datos";
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
                    collectorPayment = selectedCollector;
                    return;
                }

                collectsDTO = CollectMaper.ToDTOList(collects);
                decimal? totalAmountToPaid = 0;
                foreach (CollectDTO collectSum in collectsDTO)
                {
                    totalAmountToPaid += collectSum.amountToPaid;
                }
                var sortableList = new BindingList<CollectDTO>(collectsDTO);

                dgvCollects.DataSource = sortableList;
                dgvCollects.ClearSelection();
                ReEnableSorting();
                dgvCollects.CurrentCell = null;

                collectorPayment = selectedCollector;
                textBoxTotalAmounToPaid.Text = totalAmountToPaid?.ToString("C2");

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

            if (collectsDTO == null || collectsDTO.Count == 0)
            {
                MessageBox.Show("No hay recolectas para calcular el pago.",
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
            if (collectsDTO == null || collectsDTO.Count == 0)
            {
                MessageBox.Show("No hay recolectas para calcular el pago.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            if (dgvCollects.SelectedRows.Count == 0)
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
>>>>>>> Santiago
    }
}
