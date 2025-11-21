using CAFEPAY.ArqHex.PaymentDetails.domain;
using CAFEPAY.ArqHex.Payments.domain;
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
    public partial class ViewPaymentConsultWorkerPayments : Form
    {
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);
        private Color whiteColor = Color.White;
        private CollectorDTO collectorDTO;
        private List<PaymentDTO> listPaymentDTOs;
        private List<PaymentDetail> listPaymentDetails;
        private List<PaymentDetailDTO> listPaymentDetailsDTO;

        public ViewPaymentConsultWorkerPayments(CollectorDTO _collectorDTO, List<PaymentDTO> _listPaymentDTOs)
        {
            InitializeComponent();
            this.listPaymentDTOs = _listPaymentDTOs;
            this.collectorDTO = _collectorDTO;
            loadDataCollector();
            ConfigureDataGridView();
        }
        public void loadDataCollector()
        {
            textBoxIdWorker.Text = collectorDTO.id.ToString();
            textBoxWorkerCode.Text = collectorDTO.workerCode;
            textBoxWorkerName.Text = collectorDTO.firstName + " " + collectorDTO.lastName;
            textBoxPhone.Text = collectorDTO.phone;
            textBoxStatus.Text = collectorDTO.statusText;
        }
        private void ViewPaymentConsultWorkerPayments_Load(object sender, EventArgs e)
        {
            LoadPayments();
            LoadCollectorInfo();
        }

        private void ConfigureDataGridView()
        {
            // Configuración visual del DataGridView
            dgPayments.BorderStyle = BorderStyle.None;
            dgPayments.BackgroundColor = whiteColor;
            dgPayments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgPayments.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPayments.RowHeadersVisible = false;
            dgPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPayments.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgPayments.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgPayments.EnableHeadersVisualStyles = false;
            dgPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPayments.MultiSelect = false;
            dgPayments.ReadOnly = true;

            // Estilo de encabezados
            dgPayments.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgPayments.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgPayments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgPayments.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPayments.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgPayments.ColumnHeadersHeight = 40;

            // Estilo de celdas
            dgPayments.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgPayments.DefaultCellStyle.BackColor = whiteColor;
            dgPayments.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgPayments.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgPayments.DefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            dgPayments.RowTemplate.Height = 40;

            // Configurar columnas manualmente
            dgPayments.AutoGenerateColumns = false;
            dgPayments.Columns.Clear();

            // Columna ID
            AddColumn("Id", "ID PAGO", 100, DataGridViewContentAlignment.MiddleCenter);

            // Columna Fecha
            var colDate = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Date",
                HeaderText = "FECHA",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            dgPayments.Columns.Add(colDate);

            // Columna Worker Code
            AddColumn("WorkerCode", "CÓDIGO TRABAJADOR", 150, DataGridViewContentAlignment.MiddleCenter);

            // Columna Total Amount (Monto)
            var colAmount = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                HeaderText = "MONTO TOTAL",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2", // Formato moneda con 2 decimales
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            };
            dgPayments.Columns.Add(colAmount);
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
            dgPayments.Columns.Add(column);
        }

        private void LoadPayments()
        {
            try
            {
                if (listPaymentDTOs == null || listPaymentDTOs.Count == 0)
                {
                    MessageBox.Show("No hay pagos para mostrar.",
                                  "Sin datos",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Information);
                    return;
                }

                // Asignar los datos al DataGridView
                dgPayments.DataSource = listPaymentDTOs;

                // Limpiar selección inicial
                dgPayments.ClearSelection();

                // Calcular y mostrar el total
                decimal totalAmount = listPaymentDTOs.Sum(p => p.TotalAmount);

                // Si tienes un label para mostrar el total
                // lblTotalAmount.Text = $"Total: {totalAmount:C2}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pagos: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void LoadCollectorInfo()
        {
            try
            {
                if (collectorDTO != null)
                {
                    // Mostrar información del recolector en labels o en el título
                    this.Text = $"Pagos de {collectorDTO.firstName} {collectorDTO.lastName}";

                    // Si tienes labels para mostrar la información
                    // lblCollectorName.Text = $"{collectorDTO.firstName} {collectorDTO.lastName}";
                    // lblWorkerCode.Text = collectorDTO.workerCode?.ToString();
                    // lblCollectorId.Text = collectorDTO.id?.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar información del recolector: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void dgPayments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (dgPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un pago para consultar.",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // Obtener el PaymentDTO seleccionado
                DataGridViewRow selectedRow = dgPayments.SelectedRows[0];

                if (selectedRow.DataBoundItem is PaymentDTO selectedPayment)
                {
                    
      
                    // Consultar los detalles de pago del pago
                    listPaymentDetails = AppServices.PaymentDetailServices.queryByPaymentId.execute(selectedPayment.Id ?? 0);

                    listPaymentDetailsDTO = PaymentDetailMaper.ToDTOList(listPaymentDetails);

                    // Validar si hay pagos
                    if (listPaymentDetailsDTO == null || listPaymentDetailsDTO.Count == 0)
                    {
                        MessageBox.Show($"No se encontraron pagos para el recolector {collectorDTO.firstName} {collectorDTO.lastName}.",
                                      "Sin resultados",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        ViewPaymentConsultWorkerPaymentsDetails viewPaymentConsultWorkerPaymentsDetails = new ViewPaymentConsultWorkerPaymentsDetails(collectorDTO, selectedPayment, listPaymentDetailsDTO);
                        viewPaymentConsultWorkerPaymentsDetails.Owner = this;
                        this.Hide();
                        viewPaymentConsultWorkerPaymentsDetails.Show();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la información del recolector seleccionado.",
                                  "Error de datos",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los pagos: {ex.Message}\n\nDetalles: {ex.StackTrace}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }
    }
}