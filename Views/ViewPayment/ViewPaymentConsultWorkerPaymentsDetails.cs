using CAFEPAY.ArqHex.PaymentDetails.domain;
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
    public partial class ViewPaymentConsultWorkerPaymentsDetails : Form
    {
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);
        private Color whiteColor = Color.White;
        private List<PaymentDetailDTO> paymentDetailDTOs;
        private CollectorDTO collectorDTO;
        private PaymentDTO payment;

        public ViewPaymentConsultWorkerPaymentsDetails(CollectorDTO _collector, PaymentDTO _payment, List<PaymentDetailDTO> _paymentDetailsDTOs)
        {
            InitializeComponent();
            this.paymentDetailDTOs = _paymentDetailsDTOs;
            this.collectorDTO = _collector;
            this.payment = _payment;

            ConfigureDataGridView();
        }

        private void ViewPaymentConsultWorkerPaymentsDetails_Load(object sender, EventArgs e)
        {
            LoadPaymentDetails();
            loadDataCollector();
            loadDataPayment();
        }
        public void loadDataCollector()
        {
            textBoxIdWorker.Text = collectorDTO.id.ToString();
            textBoxWorkerCode.Text = collectorDTO.workerCode;
            textBoxWorkerName.Text = collectorDTO.firstName + " " + collectorDTO.lastName;
            textBoxPhone.Text = collectorDTO.phone;
            textBoxStatus.Text = collectorDTO.statusText;
        }
        public void loadDataPayment()
        {
            textBoxIdPayment.Text = payment.Id.ToString();
            textBoxPaymentDate.Text = payment.Date.ToString("dd/MM/yyyy");
            textBoxPaymentAmount.Text = payment.TotalAmount.ToString("C2");
        }
        private void ConfigureDataGridView()
        {
            // Configuración visual del DataGridView
            dgPaymentDetails.BorderStyle = BorderStyle.None;
            dgPaymentDetails.BackgroundColor = whiteColor;
            dgPaymentDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgPaymentDetails.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgPaymentDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPaymentDetails.RowHeadersVisible = false;
            dgPaymentDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPaymentDetails.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgPaymentDetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgPaymentDetails.EnableHeadersVisualStyles = false;
            dgPaymentDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPaymentDetails.MultiSelect = false;
            dgPaymentDetails.ReadOnly = true;

            // Estilo de encabezados
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgPaymentDetails.ColumnHeadersHeight = 40;

            // Estilo de celdas
            dgPaymentDetails.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgPaymentDetails.DefaultCellStyle.BackColor = whiteColor;
            dgPaymentDetails.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgPaymentDetails.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgPaymentDetails.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgPaymentDetails.RowTemplate.Height = 40;

            // Configurar columnas manualmente
            dgPaymentDetails.AutoGenerateColumns = false;
            dgPaymentDetails.Columns.Clear();

            // Columna ID
            AddColumn("Id", "ID DETALLE", 100, DataGridViewContentAlignment.MiddleCenter);

            // Columna Código Trabajador
            AddColumn("WorkerCode", "CÓDIGO TRABAJADOR", 140, DataGridViewContentAlignment.MiddleCenter);

            // Columna ID Lote
            AddColumn("PlotId", "ID LOTE", 100, DataGridViewContentAlignment.MiddleCenter);

            // Columna ID Recolección
            AddColumn("CollectId", "ID RECOLECCIÓN", 130, DataGridViewContentAlignment.MiddleCenter);

            // Columna ID Cosecha
            AddColumn("HarvestId", "ID COSECHA", 120, DataGridViewContentAlignment.MiddleCenter);

            // Columna ID Pago
            AddColumn("PaymentId", "ID PAGO", 100, DataGridViewContentAlignment.MiddleCenter);

            // Columna Monto a Pagar
            var colAmount = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AmountToPay",
                HeaderText = "MONTO A PAGAR",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Formato moneda con 2 decimales
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            };
            dgPaymentDetails.Columns.Add(colAmount);
        }

        private void AddColumn(string dataProperty, string headerText, int width, DataGridViewContentAlignment alignment)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = alignment
                }
            };
            dgPaymentDetails.Columns.Add(column);
        }

        private void LoadPaymentDetails()
        {
            try
            {
                if (paymentDetailDTOs == null || paymentDetailDTOs.Count == 0)
                {
                    MessageBox.Show("No hay detalles de pago para mostrar.",
                                  "Sin datos",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;
                }
 
                // Asignar los datos al DataGridView
                dgPaymentDetails.DataSource = paymentDetailDTOs;

                // Limpiar selección inicial
                dgPaymentDetails.ClearSelection();

    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los detalles de pago: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}