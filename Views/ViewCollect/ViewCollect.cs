using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewCollect
{
    public partial class ViewCollect : Form
    {
        public ViewCollect()
        {
            InitializeComponent();
        }

<<<<<<< HEAD
=======
        public void loadLastDataGridView()
        {
            collects = AppServices.CollectServices.queryByWorkerCode.execute(1, collectorRegister.workerCode, harvestRegister.idPlot, harvestRegister.id);
            collectsDTO = CollectMaper.ToDTOList(collects);
            dgvCollects.DataSource = collectsDTO;
        }

        public void loadHarvestComboBox()
        {
            try
            {
                harvests = AppServices.HarvestServices.queryByStatus.execute(1);
                if (harvests == null || harvests.Count == 0)
                {
                    return;
                }
                harvestDTO = HarvestMaper.ToDTOList(harvests);
                if (harvestDTO != null && harvestDTO.Count > 0)
                {
                    harvestDTO.Insert(0, new HarvestDTO
                    {
                        harvestName = "-- Seleccione una cosecha --"
                    });
                    cmbHarvest.DataSource = null;
                    cmbHarvest.DataSource = harvestDTO;
                    cmbHarvest.DisplayMember = "harvestName";
                    cmbHarvest.ValueMember = null;
                    cmbHarvest.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cosechas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadDgvCollects()
        {
            try
            {
                dgvCollects.Columns.Clear();
                dgvCollects.AutoGenerateColumns = false;
                dgvCollects.AllowUserToAddRows = false;
                dgvCollects.ReadOnly = true;
                dgvCollects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCollects.MultiSelect = false;

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Numero de recolecta",
                    DataPropertyName = "collectId",
                    Width = 90
                });

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Fecha Recolecta",
                    DataPropertyName = "collectDate",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" }
                });

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Kilos Recolectados",
                    DataPropertyName = "collectedKilos",
                    Width = 130,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" }
                });

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Monto a Pagar",
                    DataPropertyName = "amountToPaid",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "C2" }
                });

                dgvCollects.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Estado",
                    DataPropertyName = "statusText",
                    Width = 100
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar columnas del DataGridView: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

>>>>>>> Santiago
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ViewCollect_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD

=======
            if (harvestRegister == null)
            {
                MessageBox.Show("Debe seleccionar una cosecha antes de registrar una recolecta.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (collectorRegister == null)
            {
                MessageBox.Show("Debe seleccionar un recolector antes de registrar una recolecta.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            ViewCollectRegister viewCollectRegister = new ViewCollectRegister(harvestRegister, collectorRegister);
            viewCollectRegister.Owner = this;
            viewCollectRegister.Show();
            this.Hide();
>>>>>>> Santiago
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

<<<<<<< HEAD
        }
    }
}
=======
        private void cmbHarvest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cmbHarvest.SelectedItem is HarvestDTO selectedHarvest) ||
                selectedHarvest.harvestName == "-- Seleccione una cosecha --" ||
                selectedHarvest.id == null)
            {
                cmbCollector.DataSource = null;
                cmbCollector.Items.Clear();
                cmbCollector.Text = string.Empty;
                dgvCollects.DataSource = null;
                dgvCollects.Refresh();
                harvestRegister = null;
                collectorRegister = null;
                return;
            }

            loadCollectors(selectedHarvest.idPlot, selectedHarvest.id.Value);
            harvestRegister = selectedHarvest;

            dgvCollects.DataSource = null;
            dgvCollects.Refresh();
            collectorRegister = null;
        }

        public void loadCollectors(long idPlot, long idHarvest)
        {
            try
            {
                var collectsZero = AppServices.CollectServices.queryByStatus.execute(0, 0, idPlot, idHarvest);

                if (collectsZero == null || collectsZero.Count == 0)
                {
                    cmbCollector.DataSource = null;
                    cmbCollector.Items.Clear();
                    cmbCollector.Text = string.Empty;
                    dgvCollects.DataSource = null;
                    MessageBox.Show("No hay recolectores asociados a esta cosecha.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                List<string> workerCodes = new List<string>();
                foreach (var collect in collectsZero)
                {
                    if (!string.IsNullOrEmpty(collect.collectorWorkerCode.collectorWorkerCode))
                    {
                        workerCodes.Add(collect.collectorWorkerCode.collectorWorkerCode);
                    }
                }

                if (workerCodes.Count == 0)
                {
                    cmbCollector.DataSource = null;
                    cmbCollector.Items.Clear();
                    cmbCollector.Text = string.Empty;
                    dgvCollects.DataSource = null;
                    MessageBox.Show("No se encontraron códigos de trabajadores válidos.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string workerCodesString = string.Join(",", workerCodes.Select(code => $"'{code}'"));

                var collectors = AppServices.CollectorServices.queryByIn.execute(workerCodesString);

                if (collectors == null || collectors.Count == 0)
                {
                    cmbCollector.DataSource = null;
                    cmbCollector.Items.Clear();
                    cmbCollector.Text = string.Empty;
                    dgvCollects.DataSource = null;
                    MessageBox.Show("No se encontraron recolectores.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var collectorsDTO = CollectorMaper.ToDTOList(collectors);

                collectorsDTO.Insert(0, new CollectorDTO
                {
                    displayName = "-- Seleccione un recolector --"
                });

                cmbCollector.DataSource = null;
                cmbCollector.DataSource = collectorsDTO;
                cmbCollector.DisplayMember = "displayName";
                cmbCollector.ValueMember = null;
                cmbCollector.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar recolectores: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCollector.DataSource = null;
                cmbCollector.Items.Clear();
                cmbCollector.Text = string.Empty;
                dgvCollects.DataSource = null;
            }
        }

        private void cmbCollector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (collects != null)
            {
                collects.Clear();
            }
            if (collectsDTO != null)
            {
                collectsDTO.Clear();
            }

            if (!(cmbHarvest.SelectedItem is HarvestDTO selectedHarvest) ||
                selectedHarvest.harvestName == "-- Seleccione una cosecha --")
            {
                dgvCollects.DataSource = null;
                dgvCollects.Refresh();
                collectorRegister = null;
                return;
            }

            if (!(cmbCollector.SelectedItem is CollectorDTO selectedCollector) ||
                selectedCollector.displayName == "-- Seleccione un recolector --")
            {
                dgvCollects.DataSource = null;
                dgvCollects.Refresh();
                collectorRegister = null;
                return;
            }

            try
            {
                collects = AppServices.CollectServices.queryByWorkerCode.execute(
                    1,
                    selectedCollector.workerCode,
                    selectedHarvest.idPlot,
                    selectedHarvest.id);

                if (collects == null || collects.Count == 0)
                {
                    dgvCollects.DataSource = null;
                    dgvCollects.Refresh();
                    collectorRegister = selectedCollector;
                    MessageBox.Show($"El recolector {selectedCollector.displayName} aún no tiene recolectas registradas.",
                                   "Información",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                    return;
                }

                collectsDTO = CollectMaper.ToDTOList(collects);
                dgvCollects.DataSource = collectsDTO;
                collectorRegister = selectedCollector;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las recolectas: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                dgvCollects.DataSource = null;
                collectorRegister = null;
            }
        }
    }
}
>>>>>>> Santiago
