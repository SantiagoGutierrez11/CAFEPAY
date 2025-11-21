using CAFEPAY.ArqHex.Collectors.domain;
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
    public partial class ViewPaymentConsult : Form
    {
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);
        private Color whiteColor = Color.White;
        private List<Collector> listCollector;
        private List<CollectorDTO> listDTOCollector;
        private List<Payment> listPayments;
        private List<PaymentDTO> listPaymentsDTO;
        public ViewPaymentConsult()
        {
            InitializeComponent();
            ConfigureDataGridView();
            loadCollectors();
        }
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
        public void loadCollectors()
        {
            try
            {
                // 1️⃣ Obtener los recolectores desde el servicio
                listCollector = AppServices.CollectorServices.query.execute(); ;

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
        private void ViewPaymentConsult_Load(object sender, EventArgs e)
        {

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void textBoxConsult_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchText = textBoxConsult.Text.Trim().ToLower();

                // Si el texto está vacío, mostrar todos los registros
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    dgCollectors.DataSource = listDTOCollector;
                    dgCollectors.ClearSelection();
                    return;
                }

                // Filtrar por ID TRABAJADOR o CÉDULA
                var filteredList = listDTOCollector.Where(c =>
                    (c.workerCode != null && c.workerCode.ToString().ToLower().Contains(searchText)) ||
                    (c.id != null && c.id.ToString().ToLower().Contains(searchText))
                ).ToList();

                // Actualizar el DataGridView con los resultados filtrados
                dgCollectors.DataSource = filteredList;
                dgCollectors.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        private void btnConsult_Click(object sender, EventArgs e)
        {
            if (dgCollectors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos un recolector para consultar.",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Obtener el CollectorDTO de la fila seleccionada
                DataGridViewRow selectedRow = dgCollectors.SelectedRows[0];

                if (selectedRow.DataBoundItem is CollectorDTO collector)
                {
                    // Validar que el workerCode no sea nulo
                    if (collector.workerCode == null)
                    {
                        MessageBox.Show("El recolector seleccionado no tiene un código de trabajador válido.",
                                      "Datos incompletos",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                        return;
                    }

                    // Consultar los pagos del recolector
                    listPayments = AppServices.PaymentServices.queryByWorkerCode.execute(collector.workerCode);
                    listPaymentsDTO = PaymentMaper.ToDTOList(listPayments);

                    // Validar si hay pagos
                    if (listPayments == null || listPayments.Count == 0)
                    {
                        MessageBox.Show($"No se encontraron pagos para el recolector {collector.firstName} {collector.lastName}.",
                                      "Sin resultados",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        ViewPaymentConsultWorkerPayments viewPaymentConsultWorkerPayments = new ViewPaymentConsultWorkerPayments(collector, listPaymentsDTO);
                        viewPaymentConsultWorkerPayments.Owner = this;
                        this.Hide();
                        viewPaymentConsultWorkerPayments.Show();
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