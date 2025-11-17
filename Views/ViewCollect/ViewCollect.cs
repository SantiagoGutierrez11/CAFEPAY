using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Collects.domain;
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
        private List<Collect> collects;
        private List<CollectDTO> collectsDTO;
        private HarvestDTO harvestRegister;
        private CollectorDTO collectorRegister;

        public ViewCollect()
        {
            InitializeComponent();
            loadHarvestComboBox();
            loadDgvCollects();
        }

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
                        harvestName = "-- Seleccione una cosecha --"
                    });
                    cmbHarvest.DataSource = null;
                    cmbHarvest.DataSource = harvestDTO;
                    cmbHarvest.DisplayMember = "harvestName";
                    cmbHarvest.ValueMember = null; // No se usa ValueMember
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
                // Limpiar configuración previa
                dgvCollects.Columns.Clear();
                dgvCollects.AutoGenerateColumns = false;
                dgvCollects.AllowUserToAddRows = false;
                dgvCollects.ReadOnly = true;
                dgvCollects.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCollects.MultiSelect = false;

                // === Configurar columnas manualmente ===
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
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ViewCollect_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ViewCollectRegister viewCollectRegister = new ViewCollectRegister(harvestRegister, collectorRegister);
            viewCollectRegister.Owner = this;
            viewCollectRegister.Show();
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
            // Obtener la cosecha seleccionada
            if (cmbHarvest.SelectedItem is HarvestDTO selectedHarvest && selectedHarvest.id != null)
            {
                loadCollectors(selectedHarvest.idPlot, selectedHarvest.id.Value);
                harvestRegister = selectedHarvest;
            }
            else
            {
                // Si seleccionó "-- Seleccione una cosecha --", limpiar el combo de recolectores
                cmbCollector.DataSource = null;
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
                    cmbCollector.DataSource = null;
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
                    cmbCollector.DataSource = null;
                    MessageBox.Show("No se encontraron códigos de trabajadores válidos.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 3. Crear string con los workerCodes (formato depende de tu queryIn)
                // Asumiendo que necesitas algo como: "'CODE1','CODE2','CODE3'"
                string workerCodesString = string.Join(",", workerCodes.Select(code => $"'{code}'"));

                // 4. Consultar los recolectores
                var collectors = AppServices.CollectorServices.queryByIn.execute(workerCodesString);

                if (collectors == null || collectors.Count == 0)
                {
                    cmbCollector.DataSource = null;
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
            }
        }

        private void cmbCollector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(collects != null)
            {
                collects.Clear();
            }
            if(collectsDTO != null)
            {
                collectsDTO.Clear();
            }

            // agrega logica para cargar el dataGridView
            if(cmbHarvest.SelectedItem is HarvestDTO selectedHarvest && selectedHarvest.harvestName != "-- Seleccione una cosecha --")
            {
                if(cmbCollector.SelectedItem is CollectorDTO selectedCollector && selectedCollector.displayName != "-- Seleccione un recolector --")
                {
                    collects = AppServices.CollectServices.queryByWorkerCode.execute(1, selectedCollector.workerCode, selectedHarvest.idPlot, selectedHarvest.id);
                    collectsDTO = CollectMaper.ToDTOList(collects);
                    dgvCollects.DataSource = collectsDTO;
                    collectorRegister = selectedCollector;
                }
            }
        }
    }

}
    
