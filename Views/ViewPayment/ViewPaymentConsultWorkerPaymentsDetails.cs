using CAFEPAY.ArqHex.PaymentDetails.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewPayment
{
    public partial class ViewPaymentConsultWorkerPaymentsDetails : Form
    {
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);  // Gris oscuro
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);

        private List<PaymentDetailDTO> paymentDetailDTOs;
        private CollectorDTO collectorDTO;
        private PaymentDTO payment;

        // Controles
        private DataGridView dgPaymentDetails;
        private Button btnBack;
        private Panel mainFormPanel;
        private Label lblTotalAmount;

        // Campos de información
        private Label lblWorkerCode = new Label();
        private Label lblWorkerId = new Label();
        private Label lblWorkerName = new Label();
        private Label lblWorkerPhone = new Label();
        private Label lblWorkerStatus = new Label();
        private Label lblPaymentId = new Label();
        private Label lblPaymentDate = new Label();
        private Label lblPaymentAmount = new Label();

        public ViewPaymentConsultWorkerPaymentsDetails(CollectorDTO _collector, PaymentDTO _payment, List<PaymentDetailDTO> _paymentDetailsDTOs)
        {
            this.paymentDetailDTOs = _paymentDetailsDTOs;
            this.collectorDTO = _collector;
            this.payment = _payment;

            // Inicializar controles
            dgPaymentDetails = new DataGridView();
            btnBack = new Button();
            lblTotalAmount = new Label();
            mainFormPanel = new Panel();

            ApplyProfessionalDesign();
            LoadPaymentDetails();
            LoadDataCollector();
            LoadDataPayment();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
        }

        private void ApplyProfessionalDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 850);
            this.Text = "CAFICAUCA - Detalles de Pago";
            this.WindowState = FormWindowState.Maximized;

            // 🔝 ENCABEZADO SUPERIOR - Logo CAFICAUCA
            var topHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = whiteColor,
                Padding = new Padding(20, 10, 40, 10)
            };

            // Panel del logo
            var logoPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 350,
                BackColor = Color.Transparent,
                Height = 70,
                Padding = new Padding(10, 0, 0, 0)
            };

            // 🖼️ CARGAR IMAGEN DESDE CARPETA RESOURCES
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "LOGO-CAFICAUCA.png");
                if (File.Exists(imagePath))
                {
                    PictureBox logoPicture = new PictureBox();
                    logoPicture.Image = Image.FromFile(imagePath);
                    logoPicture.SizeMode = PictureBoxSizeMode.Zoom;
                    logoPicture.Size = new Size(320, 70);
                    logoPicture.Location = new Point(5, 5);
                    logoPicture.Cursor = Cursors.Hand;

                    ToolTip toolTip = new ToolTip();
                    toolTip.SetToolTip(logoPicture, "CAFICAUCA - Cooperativa de Caficultores del Cauca");
                    logoPanel.Controls.Add(logoPicture);
                }
                else
                {
                    CreateSimulatedLogo(logoPanel);
                }
            }
            catch (Exception)
            {
                CreateSimulatedLogo(logoPanel);
            }

            topHeaderPanel.Controls.Add(logoPanel);

            // 🏷️ TÍTULO PRINCIPAL
            var titleContainerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(0, 30, 0, 0)
            };

            var blueOuterPanel = new Panel
            {
                Size = new Size(600, 70),
                Location = new Point((this.Width - 600) / 2, 0),
                BackColor = darkBlueColor,
                Anchor = AnchorStyles.None
            };

            var whiteInnerPanel = new Panel
            {
                Size = new Size(590, 60),
                Location = new Point(5, 5),
                BackColor = whiteColor
            };

            var mainTitleLabel = new Label
            {
                Text = "DETALLES DE PAGO DEL RECOLECTOR",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 📋 PANEL ÚNICO CON TODA LA INFORMACIÓN (2 SECCIONES)
            var infoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 200, // Más alto para acomodar más información
                BackColor = whiteColor,
                Padding = new Padding(40, 15, 40, 10)
            };

            // Crear tabla de información unificada (2 secciones)
            CreateUnifiedInfoTable(infoPanel);

            // 📊 PANEL PRINCIPAL DE CONTENIDO (DataGridView)
            var mainContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = whiteColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // Panel con borde azul para el DataGridView
            var dataGridContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = darkBlueColor,
                Padding = new Padding(2),
                Margin = new Padding(0, 10, 0, 0)
            };

            // Configurar DataGridView
            ConfigureDataGridView();
            dataGridContainerPanel.Controls.Add(dgPaymentDetails);
            mainContentPanel.Controls.Add(dataGridContainerPanel);

            // 🟦 PANEL DE TOTAL A PAGAR Y BOTÓN
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 15, 40, 15)
            };

            // TOTAL A PAGAR (izquierda)
            var totalContainer = new Panel
            {
                Location = new Point(40, 15),
                Size = new Size(350, 40),
                BackColor = Color.Transparent
            };

            var totalLabel = new Label
            {
                Text = "Total pagado:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(0, 5),
                Size = new Size(140, 30),
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false
            };

            lblTotalAmount.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTotalAmount.ForeColor = darkBlueColor;
            lblTotalAmount.Location = new Point(150, 5);
            lblTotalAmount.Size = new Size(200, 30);
            lblTotalAmount.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalAmount.AutoSize = false;
            lblTotalAmount.Text = payment?.TotalAmount.ToString("C0") ?? "$ 0";

            totalContainer.Controls.Add(totalLabel);
            totalContainer.Controls.Add(lblTotalAmount);

            // BOTÓN REGRESAR (centrado)
            var buttonContainer = new Panel
            {
                Location = new Point((bottomPanel.Width - 200) / 2, 30),
                Size = new Size(200, 60),
                BackColor = Color.Transparent
            };

            ConfigureButtonDesign(buttonContainer);

            bottomPanel.Controls.Add(totalContainer);
            bottomPanel.Controls.Add(buttonContainer);

            // 📋 BREADCRUMB
            var breadcrumbPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 10, 40, 10)
            };

            var breadcrumbLabel = new Label
            {
                Text = "inicio / pagos / consultar / ver detalles-eliminar pago / detalles",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };
            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES EN ORDEN CORRECTO
            this.Controls.Add(mainContentPanel);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(infoPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Ajustar redimensionamiento
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                buttonContainer.Location = new Point((bottomPanel.Width - buttonContainer.Width) / 2, 30);

                // Redistribuir la información al redimensionar
                if (infoPanel != null)
                {
                    infoPanel.Controls.Clear();
                    CreateUnifiedInfoTable(infoPanel);
                    LoadDataCollector();
                    LoadDataPayment();
                }
            };
        }

        private void CreateUnifiedInfoTable(Panel container)
        {
            int containerWidth = container.Width - 80;
            int sectionWidth = (containerWidth - 40) / 2;
            int startY = 10;
            int sectionHeight = 180;

            // SECCIÓN 1: INFORMACIÓN DEL RECOLECTOR
            var workerSection = CreateSectionPanel("Datos del Recolector", 40, startY, sectionWidth, sectionHeight);

            // Organizar en 2 columnas
            int columnWidth = (sectionWidth - 20) / 2;

            // Columna izquierda
            CreateInfoField(workerSection, "Código de trabajador:", lblWorkerCode, 20, 40, 170, columnWidth - 180);
            CreateInfoField(workerSection, "Número de cédula:", lblWorkerId, 20, 75, 170, columnWidth - 180);
            CreateInfoField(workerSection, "Celular:", lblWorkerPhone, 20, 110, 170, columnWidth - 180);

            // Columna derecha
            CreateInfoField(workerSection, "Nombre:", lblWorkerName, 20 + columnWidth, 40, 170, columnWidth - 180);
            CreateInfoField(workerSection, "Estado:", lblWorkerStatus, 20 + columnWidth, 75, 170, columnWidth - 180);

            container.Controls.Add(workerSection);

            // SECCIÓN 2: INFORMACIÓN DEL PAGO
            var paymentSection = CreateSectionPanel("Datos del Pago", 40 + sectionWidth + 40, startY, sectionWidth, sectionHeight);

            // Organizar en 2 columnas
            CreateInfoField(paymentSection, "ID de Pago:", lblPaymentId, 20, 40, 150, columnWidth - 160);
            CreateInfoField(paymentSection, "Fecha:", lblPaymentDate, 20, 75, 150, columnWidth - 160);
            CreateInfoField(paymentSection, "Monto total:", lblPaymentAmount, 20, 110, 150, columnWidth - 160);

            container.Controls.Add(paymentSection);
        }

        private Panel CreateSectionPanel(string title, int x, int y, int width, int height)
        {
            var panel = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };

            // Título de la sección
            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(0, 0),
                Size = new Size(width, 30),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };

            panel.Controls.Add(titleLabel);

            // Línea decorativa debajo del título
            var linePanel = new Panel
            {
                Location = new Point(0, 30),
                Size = new Size(width, 2),
                BackColor = darkBlueColor
            };

            panel.Controls.Add(linePanel);

            return panel;
        }

        private void CreateInfoField(Panel container, string labelText, Label valueLabel, int x, int y, int labelWidth, int valueWidth)
        {
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(x, y),
                Size = new Size(labelWidth, 26),
                TextAlign = ContentAlignment.MiddleLeft
            };

            valueLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            valueLabel.Location = new Point(x + labelWidth + 8, y);
            valueLabel.Size = new Size(valueWidth, 26);
            valueLabel.ForeColor = Color.FromArgb(60, 60, 60);
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            valueLabel.BackColor = Color.Transparent;
            valueLabel.Cursor = Cursors.Default;

            container.Controls.Add(label);
            container.Controls.Add(valueLabel);
        }

        private void CreateSimulatedLogo(Panel logoPanel)
        {
            var simulatedLogoPanel = new Panel
            {
                Size = new Size(320, 70),
                Location = new Point(5, 5),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle
            };

            var logoText = new Label
            {
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nCONSULTAR DETALLES PAGO",
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(15, 8),
                AutoSize = false,
                Size = new Size(290, 55),
                TextAlign = ContentAlignment.MiddleLeft
            };

            var redLine = new Panel
            {
                Size = new Size(4, 40),
                Location = new Point(8, 15),
                BackColor = Color.FromArgb(183, 32, 46) // Rojo CAFICAUCA
            };

            simulatedLogoPanel.Controls.Add(logoText);
            simulatedLogoPanel.Controls.Add(redLine);
            logoPanel.Controls.Add(simulatedLogoPanel);
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private void ConfigureDataGridView()
        {
            dgPaymentDetails.BorderStyle = BorderStyle.None;
            dgPaymentDetails.BackgroundColor = whiteColor;
            dgPaymentDetails.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgPaymentDetails.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgPaymentDetails.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPaymentDetails.RowHeadersVisible = false;

            dgPaymentDetails.AllowUserToAddRows = false;
            dgPaymentDetails.AllowUserToDeleteRows = false;
            dgPaymentDetails.AllowUserToResizeRows = false;

            dgPaymentDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPaymentDetails.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgPaymentDetails.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgPaymentDetails.EnableHeadersVisualStyles = false;
            dgPaymentDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPaymentDetails.MultiSelect = false;
            dgPaymentDetails.ReadOnly = true;
            dgPaymentDetails.Dock = DockStyle.Fill;

            // Estilo de encabezados
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgPaymentDetails.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
            dgPaymentDetails.ColumnHeadersHeight = 45;

            // Estilo de celdas
            dgPaymentDetails.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgPaymentDetails.DefaultCellStyle.BackColor = whiteColor;
            dgPaymentDetails.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgPaymentDetails.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgPaymentDetails.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgPaymentDetails.RowTemplate.Height = 45;

            // Configurar columnas manualmente
            dgPaymentDetails.AutoGenerateColumns = false;
            dgPaymentDetails.Columns.Clear();

            // Columnas según la imagen proporcionada
            AddColumn("Id", "ID DETALLE", 120);
            AddColumn("WorkerCode", "CÓDIGO TRABAJADOR", 160);
            AddColumn("PlotId", "ID LOTE", 100);
            AddColumn("PlotName", "NOMBRE DE LOTE", 180);
            AddColumn("CollectId", "ID RECOLECCIÓN", 140);
            AddColumn("HarvestId", "ID COSECHA", 120);
            AddColumn("PaymentId", "ID PAGO", 100);

            // Columna de monto con formato especial
            var colAmount = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AmountToPay",
                HeaderText = "MONTO A PAGAR",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight,
                    Padding = new Padding(0, 10, 15, 10)
                }
            };
            dgPaymentDetails.Columns.Add(colAmount);
        }

        private void AddColumn(string dataProperty, string headerText, int width)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width
            };
            dgPaymentDetails.Columns.Add(column);
        }

        private void ConfigureButtonDesign(Panel buttonContainer)
        {
            int buttonWidth = 200;
            int buttonHeight = 45;

            // Botón REGRESAR (Azul oscuro)
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.BackColor = darkBlueColor;
            btnBack.ForeColor = whiteColor;
            btnBack.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnBack.Text = "REGRESAR";
            btnBack.Size = new Size(buttonWidth, buttonHeight);
            btnBack.Location = new Point(0, 0);
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += btnBack_Click;

            ApplyRoundedCorners(btnBack, 8);
            buttonContainer.Controls.Add(btnBack);
        }

        public void LoadDataCollector()
        {
            if (collectorDTO != null)
            {
                lblWorkerCode.Text = collectorDTO.workerCode;
                lblWorkerId.Text = collectorDTO.id.ToString();
                lblWorkerName.Text = $"{collectorDTO.firstName} {collectorDTO.lastName}";
                lblWorkerPhone.Text = collectorDTO.phone ?? "No registrado";
                lblWorkerStatus.Text = collectorDTO.statusText ?? "Activo";
            }
        }

        public void LoadDataPayment()
        {
            if (payment != null)
            {
                lblPaymentId.Text = payment.Id.ToString();
                lblPaymentDate.Text = payment.Date.ToString("dd/MM/yyyy");
                lblPaymentAmount.Text = payment.TotalAmount.ToString("C0");
            }
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
                dgPaymentDetails.CurrentCell = null;

                // Calcular total si no viene del pago
                if (payment == null && paymentDetailDTOs.Count > 0)
                {
                    decimal total = 0;
                    foreach (var detail in paymentDetailDTOs)
                    {
                        total += detail.AmountToPay;
                    }
                    lblTotalAmount.Text = total.ToString("C0");
                }
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
            this.Owner?.Show();
            this.Close();
        }
    }
}