using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Collects.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CAFEPAY.Views.ViewOrigin;

namespace CAFEPAY.Views.ViewPayment
{
    public partial class ViewPayment : Form
    {
        private List<Harvest> harvests;
        private List<HarvestDTO> harvestsDTO;
        private List<Collector> collectors;
        private List<CollectorDTO> collectorsDTO;
        private HarvestDTO harvestPayment;
        private CollectorDTO collectorPayment;
        private List<Collect> collects;
        private List<CollectDTO> collectsDTO;
        private Form viewMenuPayment;

        // Colores exactos del FIGMA
        private Color redColor = Color.FromArgb(183, 32, 46);     // #B7202E
        private Color darkBlueColor = Color.FromArgb(13, 43, 97); // #0D2B61
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);
        private Color greenColor = Color.FromArgb(34, 139, 34);   // Verde para el segundo botón

        // Controles dinámicos
        private ComboBox cmbHarvestDynamic;
        private ComboBox cmbCollectorDynamic;
        private Button btnCalculateTotalDynamic;
        private Button btnCalculateSelectedDynamic;
        private Button btnHomeDynamic;
        private Button btnBackDynamic;
        private DataGridView dgvCollectsDynamic;
        private TextBox txtTotalAmountDynamic;

        // 🔥 VARIABLES para posición de botones
        private int buttonPositionX = 590; // Posición X fija para botón azul
        private int buttonSpacing = 230;   // Espacio entre botones

        public ViewPayment(Form _viewMenuPayment)
        {
            this.viewMenuPayment = _viewMenuPayment;
            ApplyExactFigmaDesign();
            loadHarvestComboBox();
            loadDgvCollects();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
        }

        private void ApplyExactFigmaDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 800);
            this.Text = "CAFICAUCA - Gestión de Pagos";

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

            // 🏷️ TÍTULO PRINCIPAL - "CALCULAR PAGO"
            var titleContainerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(0, 30, 0, 0)
            };

            // Rectángulo azul exterior
            var blueOuterPanel = new Panel
            {
                Size = new Size(400, 70),
                Location = new Point((this.Width - 400) / 2, 0),
                BackColor = darkBlueColor,
                Anchor = AnchorStyles.None
            };

            // Rectángulo blanco interior
            var whiteInnerPanel = new Panel
            {
                Size = new Size(390, 60),
                Location = new Point(5, 5),
                BackColor = whiteColor
            };

            // Label del título
            var mainTitleLabel = new Label
            {
                Text = "CALCULAR PAGO",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 📋 PANEL DE FILTROS Y BOTONES (Cosecha + Recolector + Botones)
            var filterButtonPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 140,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // 📋 FILTROS (IZQUIERDA)
            // Label y ComboBox para Cosecha
            var lblHarvest = new Label
            {
                Text = "Cosecha",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(40, 25),
                AutoSize = true
            };

            cmbHarvestDynamic = new ComboBox
            {
                Location = new Point(40, 55),
                Size = new Size(250, 35),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbHarvestDynamic.SelectedIndexChanged += cmbHarvests_SelectedIndexChanged;

            // Label y ComboBox para Recolector
            var lblCollector = new Label
            {
                Text = "Recolector",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(310, 25),
                AutoSize = true
            };

            cmbCollectorDynamic = new ComboBox
            {
                Location = new Point(310, 55),
                Size = new Size(250, 35),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCollectorDynamic.SelectedIndexChanged += cmbCollectors_SelectedIndexChanged;

            // 🔘 BOTONES DE ACCIÓN (MÁS A LA IZQUIERDA - POSICIÓN ABSOLUTA)
            btnCalculateTotalDynamic = new Button();
            btnCalculateTotalDynamic.FlatStyle = FlatStyle.Flat;
            btnCalculateTotalDynamic.BackColor = darkBlueColor; // AZUL
            btnCalculateTotalDynamic.ForeColor = whiteColor;
            btnCalculateTotalDynamic.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCalculateTotalDynamic.Text = "Calcular Pago Total";
            btnCalculateTotalDynamic.Size = new Size(220, 45);
            btnCalculateTotalDynamic.Location = new Point(buttonPositionX, 45); // 🔥 USAMOS VARIABLE
            btnCalculateTotalDynamic.Cursor = Cursors.Hand;
            btnCalculateTotalDynamic.FlatAppearance.BorderSize = 0;
            btnCalculateTotalDynamic.Click += btnCalculateTotalPayment_Click;
            ApplyRoundedCorners(btnCalculateTotalDynamic, 8);

            btnCalculateSelectedDynamic = new Button();
            btnCalculateSelectedDynamic.FlatStyle = FlatStyle.Flat;
            btnCalculateSelectedDynamic.BackColor = greenColor; // VERDE
            btnCalculateSelectedDynamic.ForeColor = whiteColor;
            btnCalculateSelectedDynamic.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCalculateSelectedDynamic.Text = "Calcular Pago Seleccionado";
            btnCalculateSelectedDynamic.Size = new Size(220, 45);
            btnCalculateSelectedDynamic.Location = new Point(buttonPositionX + buttonSpacing, 45); // 🔥 POSICIÓN RELATIVA
            btnCalculateSelectedDynamic.Cursor = Cursors.Hand;
            btnCalculateSelectedDynamic.FlatAppearance.BorderSize = 0;
            btnCalculateSelectedDynamic.Click += btnPaymentPartial_Click;
            ApplyRoundedCorners(btnCalculateSelectedDynamic, 8);

            // Agregar todos los controles al panel
            filterButtonPanel.Controls.Add(lblHarvest);
            filterButtonPanel.Controls.Add(cmbHarvestDynamic);
            filterButtonPanel.Controls.Add(lblCollector);
            filterButtonPanel.Controls.Add(cmbCollectorDynamic);
            filterButtonPanel.Controls.Add(btnCalculateTotalDynamic);
            filterButtonPanel.Controls.Add(btnCalculateSelectedDynamic);

            // 📊 PANEL DE DATOS (DataGridView)
            var dataPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = whiteColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // 🟦 PANEL CON BORDE AZUL para el DataGridView
            var dataGridContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = darkBlueColor,
                Padding = new Padding(2),
                Margin = new Padding(0, 0, 0, 10)
            };

            // Configurar DataGridView dinámico
            dgvCollectsDynamic = new DataGridView();
            ConfigureDataGridFigmaStyle();
            dgvCollectsDynamic.Dock = DockStyle.Fill;

            // 🔥 CORRECCIÓN: Eliminar fila extra al final
            dgvCollectsDynamic.AllowUserToAddRows = false;
            dgvCollectsDynamic.AllowUserToDeleteRows = false;

            // Agregar DataGridView al panel con borde azul
            dataGridContainerPanel.Controls.Add(dgvCollectsDynamic);
            dataPanel.Controls.Add(dataGridContainerPanel);

            // 💰 PANEL DE TOTALES Y REGRESAR
            var totalBackPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 25, 40, 25)
            };

            // Label y TextBox para Monto Total (IZQUIERDA)
            var lblTotalAmount = new Label
            {
                Text = "Monto total a pagar",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(40, 20),
                AutoSize = true
            };

            txtTotalAmountDynamic = new TextBox
            {
                Location = new Point(40, 50),
                Size = new Size(300, 40),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ReadOnly = true,
                BackColor = whiteColor,
                ForeColor = darkGrayColor,
                TextAlign = HorizontalAlignment.Center,
                Text = "No hay datos",
                BorderStyle = BorderStyle.FixedSingle,
                // 🔥 CORRECCIÓN COMPLETA: NO permitir interacción
                Cursor = Cursors.Default,
                TabStop = false,
                Enabled = false // 🔥 ESTA ES LA CLAVE - Deshabilita completamente el control
            };

            // 🔥 BOTÓN REGRESAR - Ahora va a ViewMenuPayment
            btnBackDynamic = new Button();
            btnBackDynamic.FlatStyle = FlatStyle.Flat;
            btnBackDynamic.BackColor = darkGrayColor;
            btnBackDynamic.ForeColor = whiteColor;
            btnBackDynamic.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnBackDynamic.Text = "← Regresar";
            btnBackDynamic.Size = new Size(180, 50);
            btnBackDynamic.Location = new Point(totalBackPanel.Width / 2 - 90, 35);
            btnBackDynamic.Anchor = AnchorStyles.None;
            btnBackDynamic.Cursor = Cursors.Hand;
            btnBackDynamic.FlatAppearance.BorderSize = 0;
            btnBackDynamic.Click += btnBack_Click;
            ApplyRoundedCorners(btnBackDynamic, 10);

            totalBackPanel.Controls.Add(lblTotalAmount);
            totalBackPanel.Controls.Add(txtTotalAmountDynamic);
            totalBackPanel.Controls.Add(btnBackDynamic);

            // 📋 BREADCRUMB (pie de página inferior izquierda)
            var breadcrumbPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 10, 40, 10)
            };

            var breadcrumbLabel = new Label
            {
                Text = "inicio / pagos / pagar",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };

            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES AL FORMULARIO EN ORDEN CORRECTO
            this.Controls.Add(dataPanel);
            this.Controls.Add(totalBackPanel);
            this.Controls.Add(filterButtonPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // 🔥 CORRECCIÓN: ELIMINAR el evento Resize que estaba moviendo los botones
            // Los botones se quedan en su posición fija
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                btnBackDynamic.Location = new Point(totalBackPanel.Width / 2 - 90, 35);
                // 🔥 NO movemos los botones de calcular - se quedan donde los pusimos
            };
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
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nPAGOS",
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
            dgvCollectsDynamic.BorderStyle = BorderStyle.None;
            dgvCollectsDynamic.BackgroundColor = whiteColor;
            dgvCollectsDynamic.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvCollectsDynamic.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCollectsDynamic.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvCollectsDynamic.RowHeadersVisible = false;

            dgvCollectsDynamic.AllowUserToAddRows = false;
            dgvCollectsDynamic.AllowUserToDeleteRows = false;
            dgvCollectsDynamic.AllowUserToResizeRows = false;

            dgvCollectsDynamic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCollectsDynamic.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCollectsDynamic.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCollectsDynamic.EnableHeadersVisualStyles = false;
            dgvCollectsDynamic.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCollectsDynamic.MultiSelect = true;
            dgvCollectsDynamic.ReadOnly = true;

            // Estilo de encabezados
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
            dgvCollectsDynamic.ColumnHeadersHeight = 45;

            // Estilo de celdas
            dgvCollectsDynamic.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvCollectsDynamic.DefaultCellStyle.BackColor = whiteColor;
            dgvCollectsDynamic.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgvCollectsDynamic.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCollectsDynamic.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgvCollectsDynamic.RowTemplate.Height = 45;
        }

        public void loadDgvCollects()
        {
            try
            {
                dgvCollectsDynamic.AutoGenerateColumns = false;
                dgvCollectsDynamic.Columns.Clear();

                // Columnas según la imagen
                AddColumn("collectId", "Numero de recolecta", 120);
                AddColumn("collectDate", "Fecha Recolecta", 130);
                AddColumn("collectedKilos", "Kilos Recolectados", 140);
                AddColumn("amountToPaid", "Monto a Pagar", 130);
                AddColumn("statusText", "Estado", 100);

                // Aplicar formatos
                if (dgvCollectsDynamic.Columns["collectDate"] != null)
                {
                    dgvCollectsDynamic.Columns["collectDate"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }

                if (dgvCollectsDynamic.Columns["collectedKilos"] != null)
                {
                    dgvCollectsDynamic.Columns["collectedKilos"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvCollectsDynamic.Columns["collectedKilos"].DefaultCellStyle.Format = "N2";
                }

                if (dgvCollectsDynamic.Columns["amountToPaid"] != null)
                {
                    dgvCollectsDynamic.Columns["amountToPaid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    var culture = new System.Globalization.CultureInfo("es-CO");
                    culture.NumberFormat.CurrencySymbol = "$";
                    culture.NumberFormat.CurrencyPositivePattern = 2;
                    dgvCollectsDynamic.Columns["amountToPaid"].DefaultCellStyle.FormatProvider = culture;
                    dgvCollectsDynamic.Columns["amountToPaid"].DefaultCellStyle.Format = "C2";
                }

                dgvCollectsDynamic.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar columnas: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddColumn(string dataProperty, string headerText, int width)
        {
            var column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProperty,
                HeaderText = headerText,
                Width = width
            };
            dgvCollectsDynamic.Columns.Add(column);
        }

        // 🔄 MÉTODOS DE CARGA DE DATOS
        public void loadHarvestComboBox()
        {
            try
            {
                harvests = AppServices.HarvestServices.queryByStatus.execute(1);
                if (harvests == null || harvests.Count == 0) return;

                harvestsDTO = HarvestMaper.ToDTOList(harvests);
                if (harvestsDTO != null && harvestsDTO.Count > 0)
                {
                    harvestsDTO.Insert(0, new HarvestDTO
                    {
                        harvestName = "-- Seleccione una cosecha --"
                    });
                    cmbHarvestDynamic.DataSource = null;
                    cmbHarvestDynamic.DataSource = harvestsDTO;
                    cmbHarvestDynamic.DisplayMember = "harvestName";
                    cmbHarvestDynamic.ValueMember = null;
                    cmbHarvestDynamic.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar cosechas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadCollectors(long idPlot, long idHarvest)
        {
            try
            {
                var collectsZero = AppServices.CollectServices.queryByStatus.execute(0, 0, idPlot, idHarvest);

                if (collectsZero == null || collectsZero.Count == 0)
                {
                    cmbCollectorDynamic.DataSource = null;
                    cmbCollectorDynamic.Items.Clear();
                    cmbCollectorDynamic.Text = string.Empty;
                    dgvCollectsDynamic.DataSource = null;
                    txtTotalAmountDynamic.Text = "No hay datos";
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
                    cmbCollectorDynamic.DataSource = null;
                    cmbCollectorDynamic.Items.Clear();
                    cmbCollectorDynamic.Text = string.Empty;
                    dgvCollectsDynamic.DataSource = null;
                    txtTotalAmountDynamic.Text = "No hay datos";
                    return;
                }

                string workerCodesString = string.Join(",", workerCodes.Select(code => $"'{code}'"));
                var collectors = AppServices.CollectorServices.queryByIn.execute(workerCodesString);

                if (collectors == null || collectors.Count == 0)
                {
                    cmbCollectorDynamic.DataSource = null;
                    return;
                }

                var collectorsDTO = CollectorMaper.ToDTOList(collectors);
                collectorsDTO.Insert(0, new CollectorDTO
                {
                    displayName = "-- Seleccione un recolector --"
                });

                cmbCollectorDynamic.DataSource = null;
                cmbCollectorDynamic.DataSource = collectorsDTO;
                cmbCollectorDynamic.DisplayMember = "displayName";
                cmbCollectorDynamic.ValueMember = null;
                cmbCollectorDynamic.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar recolectores: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCollectorDynamic.DataSource = null;
                cmbCollectorDynamic.Items.Clear();
                cmbCollectorDynamic.Text = string.Empty;
                dgvCollectsDynamic.DataSource = null;
                txtTotalAmountDynamic.Text = "No hay datos";
            }
        }

        // 🔄 MÉTODOS DE EVENTOS
        private void cmbHarvests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHarvestDynamic.SelectedItem is HarvestDTO selectedHarvest && selectedHarvest.id != null)
            {
                loadCollectors(selectedHarvest.idPlot, selectedHarvest.id.Value);
                harvestPayment = selectedHarvest;
                dgvCollectsDynamic.DataSource = null;
                txtTotalAmountDynamic.Text = "No hay datos";
            }
            else
            {
                cmbCollectorDynamic.DataSource = null;
                txtTotalAmountDynamic.Text = "No hay datos";
            }
        }

        private void cmbCollectors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (collects != null) collects.Clear();
            if (collectsDTO != null) collectsDTO.Clear();

            if (!(cmbHarvestDynamic.SelectedItem is HarvestDTO selectedHarvest) ||
                selectedHarvest.harvestName == "-- Seleccione una cosecha --")
            {
                dgvCollectsDynamic.DataSource = null;
                txtTotalAmountDynamic.Text = "No hay datos";
                return;
            }

            if (!(cmbCollectorDynamic.SelectedItem is CollectorDTO selectedCollector) ||
                selectedCollector.displayName == "-- Seleccione un recolector --")
            {
                dgvCollectsDynamic.DataSource = null;
                dgvCollectsDynamic.Refresh();
                collectorPayment = null;
                txtTotalAmountDynamic.Text = "No hay datos";
                return;
            }

            try
            {
                collects = AppServices.CollectServices.queryByStatusAndWorkerCode.execute(
                    1,
                    selectedCollector.workerCode,
                    1,
                    selectedHarvest.idPlot,
                    selectedHarvest.id.Value);

                if (collects == null || collects.Count == 0)
                {
                    dgvCollectsDynamic.DataSource = null;
                    txtTotalAmountDynamic.Text = "No hay datos";
                    collectorPayment = selectedCollector;
                    return;
                }

                collectsDTO = CollectMaper.ToDTOList(collects);
                decimal? totalAmountToPaid = 0;
                foreach (CollectDTO collectSum in collectsDTO)
                {
                    totalAmountToPaid += collectSum.amountToPaid;
                }

                var sortableList = new BindingList<CollectDTO>(collectsDTO);
                dgvCollectsDynamic.DataSource = sortableList;
                dgvCollectsDynamic.ClearSelection();

                dgvCollectsDynamic.AllowUserToAddRows = false;

                ReEnableSorting();
                dgvCollectsDynamic.CurrentCell = null;

                collectorPayment = selectedCollector;
                txtTotalAmountDynamic.Text = totalAmountToPaid?.ToString("C2") ?? "No hay datos";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las recolectas: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvCollectsDynamic.DataSource = null;
                collectorPayment = null;
                txtTotalAmountDynamic.Text = "No hay datos";
            }
        }

        private void btnCalculateTotalPayment_Click(object sender, EventArgs e)
        {
            if (harvestPayment == null)
            {
                MessageBox.Show("Debe seleccionar una cosecha antes de calcular el pago.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (collectorPayment == null)
            {
                MessageBox.Show("Debe seleccionar un recolector antes de calcular el pago.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (collectsDTO == null || collectsDTO.Count == 0)
            {
                MessageBox.Show("No hay recolectas para calcular el pago.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ViewPaymentConfirm viewPaymentConfirm = new ViewPaymentConfirm(harvestPayment, collectorPayment, collectsDTO);
            viewPaymentConfirm.Owner = this;
            viewPaymentConfirm.Show();
            this.Hide();
        }

        private void btnPaymentPartial_Click(object sender, EventArgs e)
        {
            if (harvestPayment == null)
            {
                MessageBox.Show("Debe seleccionar una cosecha antes de calcular el pago.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (collectorPayment == null)
            {
                MessageBox.Show("Debe seleccionar un recolector antes de calcular el pago.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (collectsDTO == null || collectsDTO.Count == 0)
            {
                MessageBox.Show("No hay recolectas para calcular el pago.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvCollectsDynamic.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar al menos una recolecta para el pago parcial.",
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<CollectDTO> collectsSelected = new List<CollectDTO>();
            foreach (DataGridViewRow row in dgvCollectsDynamic.SelectedRows)
            {
                if (row.DataBoundItem is CollectDTO collect)
                {
                    collectsSelected.Add(new CollectDTO()
                    {
                        collectDate = collect.collectDate,
                        collectedKilos = collect.collectedKilos,
                        collectId = collect.collectId,
                        collectorWorkerCode = collect.collectorWorkerCode,
                        isCountable = collect.isCountable,
                        amountToPaid = collect.amountToPaid,
                        harvestId = collect.harvestId,
                        plotId = collect.plotId,
                        status = collect.status,
                        statusText = collect.statusText
                    });
                }
            }

            ViewPaymentConfirm viewPaymentConfirm = new ViewPaymentConfirm(harvestPayment, collectorPayment, collectsSelected);
            viewPaymentConfirm.Owner = this;
            this.Hide();
            viewPaymentConfirm.Show();
        }

        private void ReEnableSorting()
        {
            foreach (DataGridViewColumn column in dgvCollectsDynamic.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                // 🔥 CORRECCIÓN: Ahora va a ViewMenuPayment, no a ViewMain
                if (this.viewMenuPayment != null && !this.viewMenuPayment.IsDisposed)
                {
                    this.viewMenuPayment.Show();
                    this.viewMenuPayment.WindowState = FormWindowState.Maximized; // Asegurar que esté maximizado
                    this.Close();
                }
                else
                {
                    // Si por alguna razón viewMenuPayment no está disponible,
                    // crear uno nuevo
                    var menuPayment = new ViewMenuPayment();
                    menuPayment.WindowState = FormWindowState.Maximized;
                    menuPayment.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al regresar: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Fallback: ir al menú principal
                var viewMain = new ViewOrigin.ViewMain();
                viewMain.Show();
                this.Close();
            }
        }

        public void loadDataGridView()
        {
            try
            {
                collects = AppServices.CollectServices.queryByStatusAndWorkerCode.execute(
                    1,
                    collectorPayment.workerCode,
                    1,
                    harvestPayment.idPlot,
                    harvestPayment.id.Value);

                collectsDTO = CollectMaper.ToDTOList(collects);
                var sortableList = new BindingList<CollectDTO>(collectsDTO);
                dgvCollectsDynamic.DataSource = sortableList;
                dgvCollectsDynamic.ClearSelection();

                dgvCollectsDynamic.AllowUserToAddRows = false;

                ReEnableSorting();
                dgvCollectsDynamic.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las recolectas: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvCollectsDynamic.DataSource = null;
                collectorPayment = null;
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible && harvestPayment != null && collectorPayment != null)
            {
                loadDataGridView();
            }
        }

        // 🔥 MÉTODO PARA ELIMINAR ERROR DEL DISEÑADOR
        private void ViewPayment_Load(object sender, EventArgs e)
        {
            // Método vacío para compatibilidad con el diseñador
        }
    }
}