using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewPayment

{
    public partial class ViewPaymentDelete : Form
    {
        // Colores profesionales (manteniendo los del original)
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);
        private Color whiteColor = Color.White;
        private Color redColor = Color.FromArgb(183, 32, 46);     // #B7202E
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);

        private List<PaymentDetailDTO> paymentDetailDTOs;
        private CollectorDTO collectorDTO;
        private PaymentDTO payment;

        public ViewPaymentDelete(CollectorDTO _collector, PaymentDTO _payment, List<PaymentDetailDTO> _paymentDetailsDTOs)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.paymentDetailDTOs = _paymentDetailsDTOs;
            this.collectorDTO = _collector;
            this.payment = _payment;

            // Aplicar mejoras estéticas manteniendo el layout original
            ApplyVisualImprovements();

            ConfigureDataGridView();
        }

        private void ApplyVisualImprovements()
        {
            try
            {
                // 1. Estilo del formulario (manteniendo el original)
                this.BackColor = whiteColor;
                this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Text = "CAFICAUCA - Eliminar Pago";

                // 2. Estilo de botones (mejorados)
                StyleButtons();

                // 3. Mejorar campos de texto (con la nueva fuente)
                StyleTextBoxes();

                // 4. Mejorar etiquetas (con la nueva fuente y colores)
                StyleLabels();

                // 5. Mejorar DataGridView (manteniendo la configuración original pero con mejoras)
                StyleDataGridView();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error aplicando mejoras visuales: " + ex.Message);
            }
        }

        private void StyleButtons()
        {
            // Botón "Eliminar Pago" (button1)
            if (btnDeletPayment != null)
            {
                btnDeletPayment.BackColor = redColor;
                btnDeletPayment.ForeColor = whiteColor;
                btnDeletPayment.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                btnDeletPayment.FlatStyle = FlatStyle.Flat;
                btnDeletPayment.FlatAppearance.BorderSize = 0;
                btnDeletPayment.FlatAppearance.MouseOverBackColor = Color.FromArgb(163, 22, 36);
                btnDeletPayment.Cursor = Cursors.Hand;
                ApplyRoundedCorners(btnDeletPayment, 6);
            }

            // Botón "Eliminar Detalle" (btnDeleteDetailPayment)
            if (btnDeleteDetailPayment != null)
            {
                btnDeleteDetailPayment.BackColor = redColor;
                btnDeleteDetailPayment.ForeColor = whiteColor;
                btnDeleteDetailPayment.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                btnDeleteDetailPayment.FlatStyle = FlatStyle.Flat;
                btnDeleteDetailPayment.FlatAppearance.BorderSize = 0;
                btnDeleteDetailPayment.FlatAppearance.MouseOverBackColor = Color.FromArgb(163, 22, 36);
                btnDeleteDetailPayment.Cursor = Cursors.Hand;
                ApplyRoundedCorners(btnDeleteDetailPayment, 6);
            }

            // Botón "Regresar" (btnBack)
            if (btnBack != null)
            {
                btnBack.BackColor = darkGrayColor;
                btnBack.ForeColor = whiteColor;
                btnBack.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                btnBack.FlatStyle = FlatStyle.Flat;
                btnBack.FlatAppearance.BorderSize = 0;
                btnBack.FlatAppearance.MouseOverBackColor = Color.FromArgb(84, 84, 84);
                btnBack.Cursor = Cursors.Hand;
                ApplyRoundedCorners(btnBack, 6);
            }
        }

        private void StyleTextBoxes()
        {
            // Campos de información del recolector (solo lectura) - CON NUEVA FUENTE
            var collectorTextBoxes = new[] {
                textBoxIdWorker, textBoxWorkerCode, textBoxWorkerName,
                textBoxPhone, textBoxStatus
            };

            foreach (var textBox in collectorTextBoxes)
            {
                if (textBox != null)
                {
                    // Nueva fuente: Segoe UI Semibold para mejor legibilidad
                    textBox.Font = new Font("Segoe UI Semibold", 9.5f, FontStyle.Regular);
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.FromArgb(250, 250, 250);
                    textBox.ForeColor = Color.FromArgb(50, 50, 50);
                    textBox.ReadOnly = true;
                }
            }

            // Campos de información del pago - MISMA FUENTE que los del recolector
            var paymentTextBoxes = new[] {
                textBoxIdPayment, textBoxPaymentDate, textBoxPaymentAmount
            };

            foreach (var textBox in paymentTextBoxes)
            {
                if (textBox != null)
                {
                    // Misma fuente que los campos del recolector
                    textBox.Font = new Font("Segoe UI Semibold", 9.5f, FontStyle.Regular);
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.FromArgb(250, 250, 250);
                    textBox.ForeColor = Color.FromArgb(50, 50, 50);
                    textBox.ReadOnly = true;
                }
            }

            // Campo de razón (editable) - CON NUEVA FUENTE
            if (textBoxReason != null)
            {
                textBoxReason.Font = new Font("Segoe UI", 10);
                textBoxReason.BorderStyle = BorderStyle.FixedSingle;
                textBoxReason.BackColor = Color.FromArgb(255, 245, 245);
                textBoxReason.ForeColor = Color.FromArgb(64, 64, 64);
            }
        }

        private void StyleLabels()
        {
            // ========== TÍTULOS PRINCIPALES ==========

            // "Datos del recolector" (label4) - CON NUEVA FUENTE Y ESTILO
            if (label4 != null)
            {
                label4.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                label4.ForeColor = darkBlueColor;
            }

            // "Pago Realizados" (label3) - MISMO ESTILO que "Datos del recolector"
            if (label3 != null)
            {
                label3.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                label3.ForeColor = darkBlueColor;
            }

            // ========== LABELS DE CAMPOS DEL RECOLECTOR ==========

            // Labels de campos del recolector (Código trabajador, Número de cédula, etc.)
            var collectorFieldLabels = new[] { label7, label8, label9, label1, label2 };
            foreach (var label in collectorFieldLabels)
            {
                if (label != null)
                {
                    // Nueva fuente para mejor legibilidad
                    label.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                    label.ForeColor = darkBlueColor;
                }
            }

            // ========== LABELS DE CAMPOS DE PAGO (crearlos si no existen) ==========

            // Buscar o crear label para ID Pago
            var lblIdPayment = this.Controls.Find("lblIdPayment", true).FirstOrDefault() as Label;
            if (lblIdPayment == null && textBoxIdPayment != null)
            {
                lblIdPayment = new Label();
                lblIdPayment.Text = "ID Pago:";
                lblIdPayment.Name = "lblIdPayment";
                lblIdPayment.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                lblIdPayment.ForeColor = darkBlueColor;
                lblIdPayment.Location = textBoxIdPayment.Location;
                lblIdPayment.Location = new Point(lblIdPayment.Location.X - 100, lblIdPayment.Location.Y);
                lblIdPayment.AutoSize = true;
                this.Controls.Add(lblIdPayment);
            }
            else if (lblIdPayment != null)
            {
                // Aplicar el mismo estilo que los labels del recolector
                lblIdPayment.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                lblIdPayment.ForeColor = darkBlueColor;
            }

            // Buscar o crear label para Fecha
            var lblPaymentDate = this.Controls.Find("lblPaymentDate", true).FirstOrDefault() as Label;
            if (lblPaymentDate == null && textBoxPaymentDate != null)
            {
                lblPaymentDate = new Label();
                lblPaymentDate.Text = "Fecha:";
                lblPaymentDate.Name = "lblPaymentDate";
                lblPaymentDate.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                lblPaymentDate.ForeColor = darkBlueColor;
                lblPaymentDate.Location = textBoxPaymentDate.Location;
                lblPaymentDate.Location = new Point(lblPaymentDate.Location.X - 100, lblPaymentDate.Location.Y);
                lblPaymentDate.AutoSize = true;
                this.Controls.Add(lblPaymentDate);
            }
            else if (lblPaymentDate != null)
            {
                // Aplicar el mismo estilo que los labels del recolector
                lblPaymentDate.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                lblPaymentDate.ForeColor = darkBlueColor;
            }

            // Buscar o crear label para Monto Total
            var lblPaymentAmount = this.Controls.Find("lblPaymentAmount", true).FirstOrDefault() as Label;
            if (lblPaymentAmount == null && textBoxPaymentAmount != null)
            {
                lblPaymentAmount = new Label();
                lblPaymentAmount.Text = "Monto Total:";
                lblPaymentAmount.Name = "lblPaymentAmount";
                lblPaymentAmount.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                lblPaymentAmount.ForeColor = darkBlueColor;
                lblPaymentAmount.Location = textBoxPaymentAmount.Location;
                lblPaymentAmount.Location = new Point(lblPaymentAmount.Location.X - 100, lblPaymentAmount.Location.Y);
                lblPaymentAmount.AutoSize = true;
                this.Controls.Add(lblPaymentAmount);
            }
            else if (lblPaymentAmount != null)
            {
                // Aplicar el mismo estilo que los labels del recolector
                lblPaymentAmount.Font = new Font("Segoe UI Semibold", 10, FontStyle.Regular);
                lblPaymentAmount.ForeColor = darkBlueColor;
            }
        }

        private void StyleDataGridView()
        {
            // Mantener la configuración original pero mejorar la fuente
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 10.5f, FontStyle.Regular);
            dgPaymentDetails.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            try
            {
                GraphicsPath path = new GraphicsPath();
                Rectangle rect = new Rectangle(0, 0, control.Width, control.Height);

                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();

                control.Region = new Region(path);
            }
            catch
            {
                // Si falla, continuar sin esquinas redondeadas
            }
        }

        // ========== MÉTODOS ORIGINALES (SIN CAMBIOS DE FUNCIONALIDAD) ==========

        private void ViewPaymentDelete_Load(object sender, EventArgs e)
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

            // Formato de moneda mejorado
            var culture = new System.Globalization.CultureInfo("es-CO");
            culture.NumberFormat.CurrencySymbol = "$";
            culture.NumberFormat.CurrencyPositivePattern = 2;
            textBoxPaymentAmount.Text = payment.TotalAmount.ToString("C2", culture);
        }

        private void ConfigureDataGridView()
        {
            // Configuración visual del DataGridView (ORIGINAL)
            dgPaymentDetails.BorderStyle = BorderStyle.None;
            dgPaymentDetails.BackgroundColor = whiteColor;
            dgPaymentDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgPaymentDetails.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgPaymentDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPaymentDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPaymentDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPaymentDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPaymentDetails.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgPaymentDetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgPaymentDetails.EnableHeadersVisualStyles = false;
            dgPaymentDetails.MultiSelect = true;
            dgPaymentDetails.ReadOnly = true;

            // Estilo de encabezados (CON NUEVA FUENTE)
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 11, FontStyle.Regular);
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 0, 0);
            dgPaymentDetails.ColumnHeadersHeight = 40;

            // Estilo de celdas (CON NUEVA FUENTE)
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
            // Columna ID Lote
            AddColumn("PlotName", "NOMBRE DE LOTE", 100, DataGridViewContentAlignment.MiddleCenter);

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
                List<Plot> plots;

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

        // eliminar pago
        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Obtener y normalizar
            var reason = (textBoxReason.Text ?? string.Empty).Trim();

            // 2. Validar vacío
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Debe ingresar una razón para eliminar el pago.",
                                "Validación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 3. Validar longitud máxima
            const int maxReasonLength = 1000;
            if (reason.Length > maxReasonLength)
            {
                MessageBox.Show($"La razón no puede superar {maxReasonLength} caracteres. " +
                                $"Actualmente tiene {reason.Length} caracteres.",
                                "Validación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 4. Confirmación del usuario
            var confirm = MessageBox.Show("¿Está seguro que desea eliminar este pago? Esta acción no se puede deshacer.",
                                          "Confirmar eliminación",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                // 6. Usuario canceló
                return;
            }

            try
            {
                foreach (var detail in paymentDetailDTOs)
                {
                    AppServices.PaymentDetailServices.deleteByPaymentDetailId.execute(detail.Id, reason);
                }
                AppServices.PaymentServices.deleteByPaymentId.execute(payment.Id, reason);
                string joinedIds = string.Join(",", paymentDetailDTOs.Select(d => d.Id.ToString()));

                // Aquí notificamos qué pago se eliminó y cuántos detalles se eliminaron
                MessageBox.Show($"Pago eliminado correctamente. ID del pago: {payment.Id}. \n Numero de detalles de pago eliminados: {paymentDetailDTOs.Count}. \n Con IDs: {joinedIds}.",
                                "Operación exitosa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                // Volver a la ventana padre y cerrar
                // Volver a la ventana padre y cerrar
                if (this.Owner is ViewPaymentConsultDeleteWorkerPayments viewOwner)
                {
                    viewOwner.LoadPayments();
                    viewOwner.Show();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el pago: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnDeleteDetailPayment_Click(object sender, EventArgs e)
        {
            // 1. Obtener y normalizar
            var reason = (textBoxReason.Text ?? string.Empty).Trim();

            // 2. Validar vacío
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Debe ingresar una razón para eliminar el pago.",
                                "Validación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 3. Validar longitud máxima
            const int maxReasonLength = 1000;
            if (reason.Length > maxReasonLength)
            {
                MessageBox.Show($"La razón no puede superar {maxReasonLength} caracteres. " +
                                $"Actualmente tiene {reason.Length} caracteres.",
                                "Validación",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // 4. Confirmación del usuario
            var confirm = MessageBox.Show("¿Está seguro que desea eliminar este pago? Esta acción no se puede deshacer.",
                                          "Confirmar eliminación",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                // 6. Usuario canceló
                return;
            }

            try
            {
                List<string> iDsPaymentDetail = new List<string>();
                foreach (DataGridViewRow row in dgPaymentDetails.SelectedRows)
                {
                    if (row.DataBoundItem is PaymentDetailDTO paymentDetail)
                    {
                        AppServices.PaymentDetailServices.deleteByPaymentDetailId.execute(paymentDetail.Id, reason);
                        iDsPaymentDetail.Add(paymentDetail.Id.ToString());
                    }
                }
                string joinedIds = string.Join(",", iDsPaymentDetail);
                // Aquí notificamos qué pago se eliminó y cuántos detalles se eliminaron
                MessageBox.Show($"Numero de detalles de pago eliminados: {iDsPaymentDetail.Count}. \n Con IDs: {joinedIds}",
                                "Operación exitosa",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                // Volver a la ventana padre y cerrar
                if (this.Owner is ViewPaymentConsultDeleteWorkerPayments viewOwner)
                {
                    viewOwner.LoadPayments();
                    viewOwner.Show();
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar el pago: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click_2(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}