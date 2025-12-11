using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.PaymentDetails.domain;
using CAFEPAY.ArqHex.Payments.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewPayment
{
    public partial class ViewPaymentConsultDeleteWorkerPayments : Form
    {
        // Colores exactos del diseño CAFICAUCA
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(34, 139, 34);    // #228B22 - Verde para botón Consultar
        private Color redColor = Color.FromArgb(183, 32, 46);      // #B7202E - Rojo del botón Eliminar
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);  // Gris oscuro para el botón home/regresar
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);

        // Controles
        private DataGridView dgPayments;
        private Button btnConsultDetails;
        private Button btnDelete;
        private Button btnBack;

        // Campos de información del recolector
        private Label lblWorkerCode = new Label();
        private Label lblWorkerId = new Label();
        private Label lblWorkerName = new Label();
        private Label lblWorkerPhone = new Label();
        private Label lblWorkerStatus = new Label();

        private CollectorDTO collectorDTO;
        private List<Payment> listPayments;
        private List<PaymentDTO> listPaymentDTOs;
        private List<PaymentDetail> listPaymentDetails;
        private List<PaymentDetailDTO> listPaymentDetailsDTO;
        private bool? canBeDeleted;

        public ViewPaymentConsultDeleteWorkerPayments(CollectorDTO _collectorDTO, List<PaymentDTO> _listPaymentDTOs)
        {
            this.collectorDTO = _collectorDTO;
            this.listPaymentDTOs = _listPaymentDTOs;

            // Inicializar controles
            dgPayments = new DataGridView();
            btnConsultDetails = new Button();
            btnDelete = new Button();
            btnBack = new Button();

            ApplyProfessionalDesign();
            LoadPayments();
            LoadCollectorInfo();

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
            this.MinimumSize = new Size(1200, 800);
            this.Text = "CAFICAUCA - Consultar/Eliminar Pagos de Recolector";
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

            // 🏠 BOTÓN HOME (esquina superior derecha)
            var homeButton = new Button
            {
                Size = new Size(40, 40),
                Location = new Point(topHeaderPanel.Width - 60, 25),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = darkGrayColor,
                ForeColor = whiteColor,
                Text = "🏠",
                Font = new Font("Segoe UI", 14),
                Cursor = Cursors.Hand
            };
            homeButton.FlatAppearance.BorderSize = 0;

            // Hacer botón home con esquinas redondeadas
            GraphicsPath homePath = new GraphicsPath();
            homePath.AddRectangle(new Rectangle(0, 0, 40, 40));
            homeButton.Region = new Region(homePath);

            homeButton.Click += (s, e) => {
                var viewMain = new ViewOrigin.ViewMain();
                viewMain.Show();
                this.Close();
            };

            topHeaderPanel.Controls.Add(homeButton);
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
                Text = "CONSULTAR/ELIMINAR PAGOS DEL RECOLECTOR",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 📋 PANEL DE INFORMACIÓN DEL RECOLECTOR
            var collectorInfoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 160, // 🔥 AUMENTADO de 130 a 150 para incluir Estado
                BackColor = whiteColor,
                Padding = new Padding(40, 15, 40, 10)
            };

            // Crear tabla de información del recolector
            CreateCollectorInfoTable(collectorInfoPanel);

            // 📊 PANEL PRINCIPAL DE CONTENIDO (DataGridView de Pagos)
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
            ConfigureDataGridFigmaStyle();
            dataGridContainerPanel.Controls.Add(dgPayments);
            mainContentPanel.Controls.Add(dataGridContainerPanel);

            // 🟦 PANEL DE BOTONES DE ACCIÓN
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // Contenedor para los 3 botones (centrados)
            var buttonContainer = new Panel
            {
                Location = new Point((buttonPanel.Width - 700) / 2, 20),
                Size = new Size(700, 50),
                BackColor = Color.Transparent
            };

            ConfigureButtonsDesign(buttonContainer);

            buttonPanel.Controls.Add(buttonContainer);

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
                Text = "inicio / pagos / consultar / ver detalles-eliminar pagos",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };
            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES EN ORDEN CORRECTO
            this.Controls.Add(mainContentPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(collectorInfoPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Ajustar redimensionamiento
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                buttonContainer.Location = new Point((buttonPanel.Width - buttonContainer.Width) / 2, 20);

                // Redistribuir la información al redimensionar
                if (collectorInfoPanel != null)
                {
                    collectorInfoPanel.Controls.Clear();
                    CreateCollectorInfoTable(collectorInfoPanel);
                    LoadCollectorInfo();
                }
            };
        }

        private void CreateCollectorInfoTable(Panel container)
        {
            int containerWidth = container.Width - 80;
            int sectionWidth = containerWidth;

            // Crear sección única para la información del recolector
            var collectorSection = CreateSectionPanel("DATOS DEL RECOLECTOR", 40, 10, sectionWidth, 135);

            // 🔥 AHORA TENEMOS 3 FILAS: Estado ocupa toda la tercera fila
            int col1X = 20;
            int col2X = 550;
            int fieldWidth = (sectionWidth / 2) - 40;
            int row1Y = 40;   // Fila 1: Código y Cédula
            int row2Y = 75;   // Fila 2: Nombre y Celular
            int row3Y = 115;  // 🔥 Fila 3: Estado (ocupa toda la fila)

            // Fila 1 - Columna 1: Código de trabajador
            CreateInfoField(collectorSection, "Código del Recolector:", lblWorkerCode, col1X, row1Y, fieldWidth - 30);

            // Fila 1 - Columna 2: Número de cédula
            CreateInfoField(collectorSection, "Número de Cédula:", lblWorkerId, col2X, row1Y, fieldWidth - 40);

            // Fila 2 - Columna 1: Nombre
            CreateInfoField(collectorSection, "Nombre del Recolector:", lblWorkerName, col1X, row2Y, fieldWidth - 30);

            // Fila 2 - Columna 2: Celular
            CreateInfoField(collectorSection, "Número de Celular:", lblWorkerPhone, col2X, row2Y, fieldWidth - 40);

            // Fila 3 - Estado
            CreateInfoField(collectorSection, "Estado del Recolector:", lblWorkerStatus, col1X, row3Y, (sectionWidth - 60) - 180);

            container.Controls.Add(collectorSection);
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

        private void CreateInfoField(Panel container, string labelText, Label valueLabel, int x, int y, int valueWidth)
        {
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(x, y),
                AutoSize = true, // 🔥 ESTO ES LA CLAVE
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Valor con ancho fijo generoso
            valueLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            valueLabel.Location = new Point(x + 200, y); // 200px después de la etiqueta
            valueLabel.Size = new Size(300, 26); // Ancho generoso
            valueLabel.ForeColor = Color.FromArgb(60, 60, 60);
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;

            container.Controls.Add(label);
            container.Controls.Add(valueLabel);
        }

        private void LoadCollectorInfo()
        {
            if (collectorDTO != null)
            {
                lblWorkerCode.Text = collectorDTO.workerCode ?? "N/A";
                lblWorkerId.Text = collectorDTO.id.ToString();
                lblWorkerName.Text = $"{collectorDTO.firstName} {collectorDTO.lastName}";
                lblWorkerPhone.Text = collectorDTO.phone ?? "N/A";
                lblWorkerStatus.Text = collectorDTO.statusText ?? "N/A";
            }
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
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nCONSULTAR/ELIMINAR PAGOS",
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
                BackColor = redColor
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

        private void ConfigureDataGridFigmaStyle()
        {
            dgPayments.BorderStyle = BorderStyle.None;
            dgPayments.BackgroundColor = whiteColor;
            dgPayments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgPayments.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgPayments.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgPayments.RowHeadersVisible = false;

            dgPayments.AllowUserToAddRows = false;
            dgPayments.AllowUserToDeleteRows = false;
            dgPayments.AllowUserToResizeRows = false;

            dgPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgPayments.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgPayments.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgPayments.EnableHeadersVisualStyles = false;
            dgPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPayments.MultiSelect = false;
            dgPayments.ReadOnly = true;
            dgPayments.Dock = DockStyle.Fill;

            // Estilo de encabezados
            dgPayments.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgPayments.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgPayments.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgPayments.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPayments.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 15, 0);
            dgPayments.ColumnHeadersHeight = 45;

            // Estilo de celdas
            dgPayments.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgPayments.DefaultCellStyle.BackColor = whiteColor;
            dgPayments.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgPayments.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPayments.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgPayments.RowTemplate.Height = 45;
        }

        private void ConfigureButtonsDesign(Panel buttonContainer)
        {
            int buttonWidth = 200;
            int buttonHeight = 45;
            int spacing = 30;

            // Calcular posiciones para 3 botones
            int totalWidth = (buttonWidth * 3) + (spacing * 2);
            int startX = (buttonContainer.Width - totalWidth) / 2;

            // Botón CONSULTAR DETALLES (Verde)
            btnConsultDetails.FlatStyle = FlatStyle.Flat;
            btnConsultDetails.BackColor = greenColor;
            btnConsultDetails.ForeColor = whiteColor;
            btnConsultDetails.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnConsultDetails.Text = "CONSULTAR DETALLES";
            btnConsultDetails.Size = new Size(buttonWidth, buttonHeight);
            btnConsultDetails.Location = new Point(startX, 0);
            btnConsultDetails.Cursor = Cursors.Hand;
            btnConsultDetails.FlatAppearance.BorderSize = 0;
            btnConsultDetails.Click += btnConsultDetails_Click;
            ApplyRoundedCorners(btnConsultDetails, 8);

            // Botón ELIMINAR (Rojo)
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.BackColor = redColor;
            btnDelete.ForeColor = whiteColor;
            btnDelete.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnDelete.Text = "ELIMINAR";
            btnDelete.Size = new Size(buttonWidth, buttonHeight);
            btnDelete.Location = new Point(startX + buttonWidth + spacing, 0);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += btnDelete_Click;
            ApplyRoundedCorners(btnDelete, 8);

            // Botón REGRESAR (Gris oscuro)
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.BackColor = darkGrayColor;
            btnBack.ForeColor = whiteColor;
            btnBack.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnBack.Text = "← REGRESAR";
            btnBack.Size = new Size(buttonWidth, buttonHeight);
            btnBack.Location = new Point(startX + (buttonWidth * 2) + (spacing * 2), 0);
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += btnBack_Click;
            ApplyRoundedCorners(btnBack, 8);

            buttonContainer.Controls.Add(btnConsultDetails);
            buttonContainer.Controls.Add(btnDelete);
            buttonContainer.Controls.Add(btnBack);
        }

        public void LoadPayments()
        {
            try
            {
                // Limpiar configuración previa
                dgPayments.Columns.Clear();
                dgPayments.AutoGenerateColumns = false;
                dgPayments.AllowUserToAddRows = false;
                dgPayments.ReadOnly = true;
                dgPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgPayments.MultiSelect = false;
                dgPayments.RowHeadersVisible = false;

                // Hacer la selección normal (visible)
                dgPayments.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                dgPayments.DefaultCellStyle.SelectionForeColor = Color.Black;

                // Configurar columnas según la imagen
                AddColumn("Id", "ID PAGO", 120);
                AddColumn("Date", "FECHA", 130);
                AddColumn("WorkerCode", "CÓDIGO TRABAJADOR", 150);
                AddColumn("TotalAmount", "MONTO TOTAL", 140);

                // 🔥 CENTRAR TODAS LAS COLUMNAS
                foreach (DataGridViewColumn column in dgPayments.Columns)
                {
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Aplicar formatos ESPECÍFICOS (manteniendo centrado)
                if (dgPayments.Columns["Date"] != null)
                {
                    dgPayments.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    // Ya está centrado por el foreach
                }

                if (dgPayments.Columns["TotalAmount"] != null)
                {
                    var culture = new System.Globalization.CultureInfo("es-CO");
                    culture.NumberFormat.CurrencySymbol = "$";
                    culture.NumberFormat.CurrencyPositivePattern = 2;
                    dgPayments.Columns["TotalAmount"].DefaultCellStyle.FormatProvider = culture;
                    dgPayments.Columns["TotalAmount"].DefaultCellStyle.Format = "C2";
                    // El monto también estará centrado
                }

                // Aplicar formatos
                if (dgPayments.Columns["Date"] != null)
                {
                    dgPayments.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgPayments.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgPayments.Columns["Id"] != null)
                {
                    dgPayments.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgPayments.Columns["WorkerCode"] != null)
                {
                    dgPayments.Columns["WorkerCode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgPayments.Columns["TotalAmount"] != null)
                {
                    dgPayments.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    var culture = new System.Globalization.CultureInfo("es-CO");
                    culture.NumberFormat.CurrencySymbol = "$";
                    culture.NumberFormat.CurrencyPositivePattern = 2;
                    dgPayments.Columns["TotalAmount"].DefaultCellStyle.FormatProvider = culture;
                    dgPayments.Columns["TotalAmount"].DefaultCellStyle.Format = "C2";
                }

                // Consultar los pagos del recolector si no vienen en el constructor
                if (listPaymentDTOs == null || listPaymentDTOs.Count == 0)
                {
                    listPayments = AppServices.PaymentServices.queryByWorkerCode.execute(collectorDTO.workerCode);
                    listPaymentDTOs = PaymentMaper.ToDTOList(listPayments);
                }

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
                dgPayments.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los pagos: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        // Método auxiliar para agregar columnas
        private void AddColumn(string dataProperty, string headerText, int width)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width
            };
            dgPayments.Columns.Add(column);
        }

        // Eventos de los botones
        private void btnConsultDetails_Click(object sender, EventArgs e)
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
                    if (selectedPayment.Id == null || selectedPayment.Id == 0)
                    {
                        MessageBox.Show("El pago seleccionado no tiene un ID válido.",
                                      "Error de datos",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                        return;
                    }

                    listPaymentDetails = AppServices.PaymentDetailServices.queryByPaymentId.execute(selectedPayment.Id.Value);
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
                    MessageBox.Show("No se pudo obtener la información del pago seleccionado.",
                                  "Error de datos",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al consultar los pagos: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione un pago para eliminar.",
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
                    // 🔥 CORRECCIÓN: Verificar si el ID es válido
                    if (selectedPayment.Id == null || selectedPayment.Id == 0)
                    {
                        MessageBox.Show("El pago seleccionado no tiene un ID válido.",
                                      "Error de datos",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error);
                        return;
                    }

                    canBeDeleted = AppServices.PaymentServices.checkIfPaymentCanBeDeleted.execute(selectedPayment.Id.Value);

                    if (canBeDeleted != null && canBeDeleted == false)
                    {
                        MessageBox.Show("El pago seleccionado no puede ser eliminado porque está asociado a otros registros.",
                                      "Eliminación no permitida",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Warning);
                        return;
                    }

                    // Consultar los detalles de pago del pago
                    listPaymentDetails = AppServices.PaymentDetailServices.queryByPaymentId.execute(selectedPayment.Id.Value);
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
                        ViewPaymentDelete viewPaymentDelete = new ViewPaymentDelete(collectorDTO, selectedPayment, listPaymentDetailsDTO);
                        viewPaymentDelete.Owner = this;
                        this.Hide();
                        viewPaymentDelete.Show();
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la información del pago seleccionado.",
                                  "Error de datos",
                                  MessageBoxButtons.OK,
                                  MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar eliminar el pago: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }

        // Método para cargar datos del recolector (mantenido por compatibilidad)
        public void loadDataCollector()
        {
            // Este método ya no es necesario ya que LoadCollectorInfo hace lo mismo
            LoadCollectorInfo();
        }
    }
}