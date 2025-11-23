using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
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
    public partial class ViewPaymentConfirm : Form
    {
        private HarvestDTO harvestPayment; // Payment for a specific harvest
        private CollectorDTO collectorPayment; // Collector making the payment
        private List<CollectDTO> collectsPayment; // List of collects associated with the payment
        public ViewPaymentConfirm(HarvestDTO _harvestPayment, CollectorDTO _collectorPayment, List<CollectDTO> _collectsPayment)
        {
            InitializeComponent();
            this.harvestPayment = _harvestPayment;
            this.collectorPayment = _collectorPayment;
            this.collectsPayment = _collectsPayment;
            loadDgvCollectsToPayment();
            loadData();
       
        }
        public void loadDgvCollectsToPayment()
        {
            try
            {
                // Limpiar configuración previa
                dgvCollectsToPayment.Columns.Clear();
                dgvCollectsToPayment.AutoGenerateColumns = false;
                dgvCollectsToPayment.AllowUserToAddRows = false;
                dgvCollectsToPayment.ReadOnly = true;
                dgvCollectsToPayment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvCollectsToPayment.MultiSelect = false;
                dgvCollectsToPayment.RowHeadersVisible = false; // Ocultar selector de filas

                // Hacer la selección invisible
                dgvCollectsToPayment.DefaultCellStyle.SelectionBackColor = dgvCollectsToPayment.DefaultCellStyle.BackColor;
                dgvCollectsToPayment.DefaultCellStyle.SelectionForeColor = dgvCollectsToPayment.DefaultCellStyle.ForeColor;


                // evita seleccion automatica
                dgvCollectsToPayment.CurrentCell = null; 
                dgvCollectsToPayment.RowHeadersVisible = false;

                // configuracion de columnas
                dgvCollectsToPayment.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Numero de recolecta",
                    DataPropertyName = "collectId",
                    Width = 90,
                    SortMode = DataGridViewColumnSortMode.Automatic
                });

                dgvCollectsToPayment.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Fecha Recolecta",
                    DataPropertyName = "collectDate",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" },
                    SortMode = DataGridViewColumnSortMode.Automatic
                });

                dgvCollectsToPayment.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Kilos Recolectados",
                    DataPropertyName = "collectedKilos",
                    Width = 130,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N2" },
                    SortMode = DataGridViewColumnSortMode.Automatic
                });

                dgvCollectsToPayment.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Monto a Pagar",
                    DataPropertyName = "amountToPaid",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "C2" },
                    SortMode = DataGridViewColumnSortMode.Automatic
                });

                dgvCollectsToPayment.Columns.Add(new DataGridViewTextBoxColumn
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
        public void loadData()
        {
            textBoxIdHarvest.Text = harvestPayment.id.ToString();
            textBoxPlotName.Text = harvestPayment.plotName;
            textBoxWorkerName.Text = collectorPayment.firstName + " " + collectorPayment.lastName;
            textBoxWorkerCode.Text = collectorPayment.workerCode;
            textBoxIdWorker.Text = collectorPayment.id.ToString();
            textBoxIdPlot.Text = harvestPayment.idPlot.ToString();

            decimal totalAmount = 0;
            foreach (var collect in collectsPayment)
            {
                totalAmount += collect.amountToPaid.Value;
            }
            textBoxTotalAmount.Text = totalAmount.ToString("C2");

            dgvCollectsToPayment.DataSource = collectsPayment;

            dgvCollectsToPayment.ClearSelection();
            dgvCollectsToPayment.CurrentCell = null;
        }
        private void ViewPaymentConfirm_Load(object sender, EventArgs e)
        {

        }
        //
        //
        private void button1_Click(object sender, EventArgs e) //buton confirm
        {
            try
            {
                long paymentID = AppServices.PaymentServices.save.execute(
                    null,
                    DateTime.Today,
                    collectorPayment.workerCode
                );
                List<long> paymentDetailIDS = new List<long>();
                foreach (var collect in collectsPayment)
                {
                    paymentDetailIDS.Add(AppServices.PaymentDetailServices.save.execute(
                        collect.amountToPaid.Value,
                        null,
                        collect.collectId,
                        harvestPayment.id,
                        paymentID,
                        harvestPayment.idPlot,
                        collectorPayment.workerCode
                    ));
                }
                MessageBox.Show("Pago confirmado exitosamente.\n" +
                                $"ID de Pago: {paymentID}\n" +
                                $"Numero Detalles de pagos creados: { paymentDetailIDS.Count}",
                                "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if(this.Owner is ViewPayment viewPayment)
                {
                    viewPayment.loadDataGridView(); // Recargar datos en la vista principal de pagos
                }
                this.Owner.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al confirmar el pago: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
