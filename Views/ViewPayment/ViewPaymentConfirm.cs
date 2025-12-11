using CAFEPAY.ArqHex.Payments.domain;
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
    public partial class ViewPaymentConfirm : Form
    {
        private HarvestDTO harvestPayment;
        private CollectorDTO collectorPayment;
        private List<CollectDTO> collectsPayment;

        // Colores exactos del diseño CAFICAUCA
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Confirmar
        private Color redColor = Color.FromArgb(183, 32, 46);      // #B7202E - Rojo del botón Cancelar
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);  // Gris oscuro para el botón home
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);

        // Controles
        private DataGridView dgvCollectsToPayment;
        private Button btnConfirm;
        private Button btnCancel;
        private Label lblTotalAmount;
        private Panel mainFormPanel;

        // Campos de información (Labels)
        private Label lblPlotName = new Label();
        private Label lblPlotId = new Label();
        private Label lblHarvestId = new Label();
        private Label lblWorkerCode = new Label();
        private Label lblWorkerId = new Label();
        private Label lblWorkerName = new Label();

        public ViewPaymentConfirm(HarvestDTO _harvestPayment, CollectorDTO _collectorPayment, List<CollectDTO> _collectsPayment)
        {
            this.harvestPayment = _harvestPayment;
            this.collectorPayment = _collectorPayment;
            this.collectsPayment = _collectsPayment;

            // Inicializar controles
            dgvCollectsToPayment = new DataGridView();
            btnConfirm = new Button();
            btnCancel = new Button();
            lblTotalAmount = new Label();
            mainFormPanel = new Panel();

            ApplyProfessionalDesign();
            loadDgvCollectsToPayment();
            loadData();

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
            this.Text = "CAFICAUCA - Confirmar Pago";
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
                Text = "CONFIRMAR PAGO DE RECOLECCIONES",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 📋 PANEL ÚNICO CON TODA LA INFORMACIÓN (AHORA 2 SECCIONES)
            var infoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 130,
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
            ConfigureDataGridFigmaStyle();
            dataGridContainerPanel.Controls.Add(dgvCollectsToPayment);
            mainContentPanel.Controls.Add(dataGridContainerPanel);

            // 🟦 PANEL DE TOTAL A PAGAR Y BOTONES
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 15, 40, 15)
            };

            // TOTAL A PAGAR (izquierda) - TEXTO COMPLETO
            var totalContainer = new Panel
            {
                Location = new Point(40, 15),
                Size = new Size(350, 40),
                BackColor = Color.Transparent
            };

            // Etiqueta más ancha para "Total a pagar:"
            var totalLabel = new Label
            {
                Text = "Total a pagar:",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(0, 5),
                Size = new Size(140, 30), // ANCHO AUMENTADO
                TextAlign = ContentAlignment.MiddleRight,
                AutoSize = false
            };

            lblTotalAmount.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTotalAmount.ForeColor = darkBlueColor;
            lblTotalAmount.Location = new Point(150, 5); // AJUSTADO
            lblTotalAmount.Size = new Size(200, 30);
            lblTotalAmount.TextAlign = ContentAlignment.MiddleLeft;
            lblTotalAmount.AutoSize = false;

            totalContainer.Controls.Add(totalLabel);
            totalContainer.Controls.Add(lblTotalAmount);

            // BOTONES (centrados y juntos)
            var buttonContainer = new Panel
            {
                Location = new Point((bottomPanel.Width - 430) / 2, 30),
                Size = new Size(430, 60),
                BackColor = Color.Transparent
            };

            ConfigureButtonsDesign(buttonContainer);

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
                Text = "inicio / pagos / pagar / confirmar pago total",
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

            // Cargar información
            LoadInformationData();

            // Ajustar redimensionamiento
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                buttonContainer.Location = new Point((bottomPanel.Width - buttonContainer.Width) / 2, 30);

                // Redistribuir la información al redimensionar
                if (infoPanel != null)
                {
                    infoPanel.Controls.Clear();
                    CreateUnifiedInfoTable(infoPanel);
                    LoadInformationData();
                }
            };
        }

        private void CreateUnifiedInfoTable(Panel container)
        {
            int containerWidth = container.Width - 80;

            // Dividir en 2 secciones: Lote/Cosecha y Recolector
            int sectionWidth = (containerWidth - 40) / 2;

            int startY = 10;
            int sectionHeight = 110;

            // SECCIÓN 1: INFORMACIÓN DEL LOTE Y COSECHA
            var lotSection = CreateSectionPanel("Información General", 40, startY, sectionWidth, sectionHeight);

            // Organizar en 2 columnas dentro de la sección
            int columnWidth = (sectionWidth - 20) / 2;

            // Columna izquierda
            CreateInfoField(lotSection, "Nombre del lote:", lblPlotName, 20, 40, 150, columnWidth - 170);
            CreateInfoField(lotSection, "ID de lote:", lblPlotId, 20, 75, 150, columnWidth - 170);

            // Columna derecha - ETIQUETA MÁS ANCHA
            CreateInfoField(lotSection, "Número de cosecha:", lblHarvestId, 20 + columnWidth, 40, 160, columnWidth - 180);

            container.Controls.Add(lotSection);

            // SECCIÓN 2: INFORMACIÓN DEL RECOLECTOR - TEXTO COMPLETO
            var collectorSection = CreateSectionPanel("Información Recolector", 40 + sectionWidth + 40, startY, sectionWidth, sectionHeight);

            // Organizar en 2 columnas con ESPACIOS CORRECTOS
            int col1X = 20;
            int col2X = 20 + (sectionWidth / 2);
            int fieldWidth = (sectionWidth / 2) - 40;

            // 🔥 CORRECCIÓN: Aumentar ancho de las etiquetas para que quepa "Número de cédula:"
            CreateInfoField(collectorSection, "Código trabajador:", lblWorkerCode, col1X, 40, 165, fieldWidth - 15);
            CreateInfoField(collectorSection, "Cédula:", lblWorkerId, col2X, 40, 165, fieldWidth - 15);
            CreateInfoField(collectorSection, "Nombre:", lblWorkerName, col1X, 75, 150, (fieldWidth * 2) + 20);

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

        private void LoadInformationData()
        {
            if (harvestPayment != null)
            {
                lblPlotName.Text = harvestPayment.plotName;
                lblPlotId.Text = harvestPayment.idPlot.ToString();
                lblHarvestId.Text = harvestPayment.id.ToString();
            }

            if (collectorPayment != null)
            {
                lblWorkerCode.Text = collectorPayment.workerCode;
                lblWorkerId.Text = collectorPayment.id.ToString();
                lblWorkerName.Text = $"{collectorPayment.firstName} {collectorPayment.lastName}";
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
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nCONFIRMAR PAGO",
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
            dgvCollectsToPayment.BorderStyle = BorderStyle.None;
            dgvCollectsToPayment.BackgroundColor = whiteColor;
            dgvCollectsToPayment.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvCollectsToPayment.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCollectsToPayment.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvCollectsToPayment.RowHeadersVisible = false;

            dgvCollectsToPayment.AllowUserToAddRows = false;
            dgvCollectsToPayment.AllowUserToDeleteRows = false;
            dgvCollectsToPayment.AllowUserToResizeRows = false;

            dgvCollectsToPayment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCollectsToPayment.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCollectsToPayment.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCollectsToPayment.EnableHeadersVisualStyles = false;
            dgvCollectsToPayment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCollectsToPayment.MultiSelect = false;
            dgvCollectsToPayment.ReadOnly = true;
            dgvCollectsToPayment.Dock = DockStyle.Fill;

            // 🔥 ESTILO IGUAL AL SEGUNDO CÓDIGO
            // Estilo de encabezados
            dgvCollectsToPayment.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgvCollectsToPayment.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgvCollectsToPayment.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvCollectsToPayment.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCollectsToPayment.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
            dgvCollectsToPayment.ColumnHeadersHeight = 45;

            // Estilo de celdas
            dgvCollectsToPayment.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvCollectsToPayment.DefaultCellStyle.BackColor = whiteColor;
            dgvCollectsToPayment.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgvCollectsToPayment.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCollectsToPayment.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgvCollectsToPayment.RowTemplate.Height = 45;
        }

        private void ConfigureButtonsDesign(Panel buttonContainer)
        {
            int buttonWidth = 200;
            int buttonHeight = 45;
            int spacing = 30;

            // Calcular posiciones para centrado perfecto
            int totalWidth = (buttonWidth * 2) + spacing;
            int startX = (buttonContainer.Width - totalWidth) / 2;

            // Botón CANCELAR (Rojo)
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.BackColor = redColor;
            btnCancel.ForeColor = whiteColor;
            btnCancel.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnCancel.Text = "CANCELAR";
            btnCancel.Size = new Size(buttonWidth, buttonHeight);
            btnCancel.Location = new Point(startX, 0);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += btnCancel_Click;

            // Botón CONFIRMAR (Verde)
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.BackColor = greenColor;
            btnConfirm.ForeColor = whiteColor;
            btnConfirm.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnConfirm.Text = "CONFIRMAR";
            btnConfirm.Size = new Size(buttonWidth, buttonHeight);
            btnConfirm.Location = new Point(startX + buttonWidth + spacing, 0);
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Click += btnConfirm_Click;

            ApplyRoundedCorners(btnCancel, 8);
            ApplyRoundedCorners(btnConfirm, 8);

            buttonContainer.Controls.Add(btnCancel);
            buttonContainer.Controls.Add(btnConfirm);
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
                dgvCollectsToPayment.RowHeadersVisible = false;

                // 🔥 Hacer la selección normal (visible)
                dgvCollectsToPayment.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
                dgvCollectsToPayment.DefaultCellStyle.SelectionForeColor = Color.Black;

                // 🔥 CONFIGURACIÓN IGUAL AL SEGUNDO CÓDIGO
                // Columnas según el segundo código
                AddColumn("collectId", "Numero de recolecta", 120);
                AddColumn("collectDate", "Fecha Recolecta", 130);
                AddColumn("collectedKilos", "Kilos Recolectados", 140);
                AddColumn("amountToPaid", "Monto a Pagar", 130);
                AddColumn("statusText", "Estado", 100);

                // Aplicar formatos
                if (dgvCollectsToPayment.Columns["collectDate"] != null)
                {
                    dgvCollectsToPayment.Columns["collectDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                if (dgvCollectsToPayment.Columns["collectedKilos"] != null)
                {
                    dgvCollectsToPayment.Columns["collectedKilos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvCollectsToPayment.Columns["collectedKilos"].DefaultCellStyle.Format = "N2";
                }

                if (dgvCollectsToPayment.Columns["amountToPaid"] != null)
                {
                    dgvCollectsToPayment.Columns["amountToPaid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    var culture = new System.Globalization.CultureInfo("es-CO");
                    culture.NumberFormat.CurrencySymbol = "$";
                    culture.NumberFormat.CurrencyPositivePattern = 2;
                    dgvCollectsToPayment.Columns["amountToPaid"].DefaultCellStyle.FormatProvider = culture;
                    dgvCollectsToPayment.Columns["amountToPaid"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar columnas del DataGridView: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔥 AGREGAR MÉTODO AUXILIAR PARA AGREGAR COLUMNAS (igual al segundo código)
        private void AddColumn(string dataProperty, string headerText, int width)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width
            };
            dgvCollectsToPayment.Columns.Add(column);
        }

        public void loadData()
        {
            decimal totalAmount = 0;
            if (collectsPayment == null || collectsPayment.Count == 0)
            {
                MessageBox.Show("No hay recolectas para mostrar en el pago.",
                                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var collect in collectsPayment)
            {
                if (collect.amountToPaid.HasValue)
                {
                    totalAmount += collect.amountToPaid.Value;

                    // Asegurar que el monto se muestre sin formato de miles
                    if (collect.amountToPaid.Value % 1 == 0)
                    {
                        collect.amountToPaid = decimal.Parse(collect.amountToPaid.Value.ToString("0"));
                    }
                }
            }

            lblTotalAmount.Text = totalAmount.ToString("C0");
            dgvCollectsToPayment.DataSource = collectsPayment;
            dgvCollectsToPayment.ClearSelection();
            dgvCollectsToPayment.CurrentCell = null;
        }

        private void ViewPaymentConfirm_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
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
                    decimal amount = collect.amountToPaid ?? 0;

                    paymentDetailIDS.Add(AppServices.PaymentDetailServices.save.execute(
                        amount,
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
                                $"Número de detalles de pagos creados: {paymentDetailIDS.Count}",
                                "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (this.Owner is ViewPayment viewPayment)
                {
                    viewPayment.loadDataGridView();
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