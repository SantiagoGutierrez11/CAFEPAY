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
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CAFEPAY.ArqHex.Share.AppServices;
using CAFEPAY.Views.ViewOrigin;

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

        // Colores exactos del FIGMA
        private Color redColor = Color.FromArgb(183, 32, 46);     // #B7202E
        private Color darkBlueColor = Color.FromArgb(13, 43, 97); // #0D2B61
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);

        // Controles dinámicos
        private ComboBox cmbHarvestDynamic;
        private ComboBox cmbCollectorDynamic;
        private Button btnAddDynamic;
        private Button btnHomeDynamic;
        private DataGridView dgvCollectsDynamic;

        public ViewCollect()
        {
            InitializeComponent();

            // Ocultar controles del diseñador
            HideDesignerControls();

            ApplyExactFigmaDesign();
            loadHarvestComboBox();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
        }

        private void HideDesignerControls()
        {
            // Ocultar todos los controles creados por el diseñador
            foreach (Control control in this.Controls)
            {
                control.Visible = false;
            }
        }

        private void ApplyExactFigmaDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 800);
            this.Text = "CAFICAUCA - Gestión de Recolectas";

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
                Location = new Point(topHeaderPanel.Width - 60, 25), // Más a la esquina
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
                // Volver al menú principal
                var viewMain = new ViewOrigin.ViewMain();
                viewMain.Show();
                this.Close();
            };

            topHeaderPanel.Controls.Add(homeButton);
            topHeaderPanel.Controls.Add(logoPanel);

            // 🏷️ TÍTULO PRINCIPAL - "GESTIÓN DE RECOLECTAS"
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
                Size = new Size(500, 70),
                Location = new Point((this.Width - 500) / 2, 0),
                BackColor = darkBlueColor,
                Anchor = AnchorStyles.None
            };

            // Rectángulo blanco interior
            var whiteInnerPanel = new Panel
            {
                Size = new Size(490, 60),
                Location = new Point(5, 5),
                BackColor = whiteColor
            };

            // Label del título
            var mainTitleLabel = new Label
            {
                Text = "GESTIÓN DE RECOLECTAS",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 📋 PANEL DE FILTROS (Cosecha y Recolector)
            var filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 20, 40, 20)
            };

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
                Location = new Point(120, 20),
                Size = new Size(300, 35),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbHarvestDynamic.SelectedIndexChanged += cmbHarvest_SelectedIndexChanged;

            // Label y ComboBox para Recolector
            var lblCollector = new Label
            {
                Text = "Recolector",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(450, 25),
                AutoSize = true
            };

            cmbCollectorDynamic = new ComboBox
            {
                Location = new Point(540, 20),
                Size = new Size(300, 35),
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbCollectorDynamic.SelectedIndexChanged += cmbCollector_SelectedIndexChanged;

            filterPanel.Controls.Add(lblHarvest);
            filterPanel.Controls.Add(cmbHarvestDynamic);
            filterPanel.Controls.Add(lblCollector);
            filterPanel.Controls.Add(cmbCollectorDynamic);

            // 🔘 PANEL DE BOTONES
            var buttonContainerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // Botón AZUL - "Agregar"
            btnAddDynamic = new Button();
            btnAddDynamic.FlatStyle = FlatStyle.Flat;
            btnAddDynamic.BackColor = darkBlueColor;
            btnAddDynamic.ForeColor = whiteColor;
            btnAddDynamic.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnAddDynamic.Text = "Agregar";
            btnAddDynamic.Size = new Size(150, 50);
            btnAddDynamic.Location = new Point(buttonContainerPanel.Width / 2 - 75, 25);
            btnAddDynamic.Anchor = AnchorStyles.None;
            btnAddDynamic.Cursor = Cursors.Hand;
            btnAddDynamic.FlatAppearance.BorderSize = 0;
            btnAddDynamic.Click += button1_Click;
            ApplyRoundedCorners(btnAddDynamic, 10);

            buttonContainerPanel.Controls.Add(btnAddDynamic);

            // 📊 PANEL PRINCIPAL DE CONTENIDO (DataGridView)
            var mainContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = whiteColor,
                Padding = new Padding(40, 20, 40, 60)
            };

            // 🟦 PANEL CON BORDE AZUL para el DataGridView
            var dataGridContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = darkBlueColor,
                Padding = new Padding(2),
                Margin = new Padding(0, 10, 0, 0)
            };

            // Configurar DataGridView dinámico
            dgvCollectsDynamic = new DataGridView();
            ConfigureDataGridFigmaStyle();

            // Agregar DataGridView al panel con borde azul
            dataGridContainerPanel.Controls.Add(dgvCollectsDynamic);

            // Agregar el panel con borde al panel principal
            mainContentPanel.Controls.Add(dataGridContainerPanel);

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
                Text = "inicio / recolectas",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };

            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES AL FORMULARIO EN ORDEN CORRECTO
            this.Controls.Add(mainContentPanel);
            this.Controls.Add(buttonContainerPanel);
            this.Controls.Add(filterPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Asegurar que los controles se redimensionen correctamente
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                btnAddDynamic.Location = new Point(buttonContainerPanel.Width / 2 - 75, 25);
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
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nRECOLECTAS",
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
            // 🔥 CONFIGURACIÓN EXACTA COMO ViewCollector
            dgvCollectsDynamic.BorderStyle = BorderStyle.None;
            dgvCollectsDynamic.BackgroundColor = whiteColor;
            dgvCollectsDynamic.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgvCollectsDynamic.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvCollectsDynamic.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgvCollectsDynamic.RowHeadersVisible = false;
            dgvCollectsDynamic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCollectsDynamic.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvCollectsDynamic.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvCollectsDynamic.EnableHeadersVisualStyles = false;
            dgvCollectsDynamic.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCollectsDynamic.MultiSelect = false;
            dgvCollectsDynamic.ReadOnly = true;
            dgvCollectsDynamic.Dock = DockStyle.Fill;

            // Estilo de encabezados - EXACTAMENTE IGUAL
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCollectsDynamic.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
            dgvCollectsDynamic.ColumnHeadersHeight = 45;

            // Estilo de celdas - EXACTAMENTE IGUAL
            dgvCollectsDynamic.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvCollectsDynamic.DefaultCellStyle.BackColor = whiteColor;
            dgvCollectsDynamic.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgvCollectsDynamic.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvCollectsDynamic.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgvCollectsDynamic.RowTemplate.Height = 45;
        }

        public void loadLastDataGridView()
        {
            if (collectorRegister != null && harvestRegister != null)
            {
                collects = AppServices.CollectServices.queryByWorkerCode.execute(1, collectorRegister.workerCode, harvestRegister.idPlot, harvestRegister.id);
                collectsDTO = CollectMaper.ToDTOList(collects);

                // Invertir el orden para mostrar los más recientes primero
                collectsDTO.Reverse();

                dgvCollectsDynamic.DataSource = collectsDTO;
            }
        }

        public void loadHarvestComboBox()
        {
            try
            {
                harvests = AppServices.HarvestServices.queryByStatus.execute(1);
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
                    cmbHarvestDynamic.DataSource = null;
                    cmbHarvestDynamic.DataSource = harvestDTO;
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

        public void loadDgvCollects()
        {
            try
            {
                dgvCollectsDynamic.AutoGenerateColumns = false;
                dgvCollectsDynamic.Columns.Clear();

                // Columnas según diseño
                AddColumn("collectId", "NUMERO DE RECOLECTA", 150);
                AddColumn("collectDate", "FECHA RECOLECTA", 150);
                AddColumn("collectedKilos", "KILOS RECOLECTADOS", 160);
                AddColumn("amountToPaid", "MONTO A PAGAR", 160);
                AddColumn("statusText", "ESTADO", 120);

                // Aplicar formatos específicos después de crear las columnas
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

                    // 🔥 FORMATO CON PESOS COLOMBIANOS
                    var culture = new System.Globalization.CultureInfo("es-CO");
                    culture.NumberFormat.CurrencySymbol = "$";
                    culture.NumberFormat.CurrencyPositivePattern = 2; // $1.00 en lugar de 1.00$

                    dgvCollectsDynamic.Columns["amountToPaid"].DefaultCellStyle.FormatProvider = culture;
                    dgvCollectsDynamic.Columns["amountToPaid"].DefaultCellStyle.Format = "C2";
                }

                dgvCollectsDynamic.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al configurar columnas del DataGridView: {ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 🔥 MÉTODO EXACTO COMO ViewCollector
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (harvestRegister == null)
            {
                MessageBox.Show("Debe seleccionar una cosecha antes de registrar una recolecta.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (collectorRegister == null)
            {
                MessageBox.Show("Debe seleccionar un recolector antes de registrar una recolecta.",
                                "Advertencia",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            ViewCollectRegister viewCollectRegister = new ViewCollectRegister(harvestRegister, collectorRegister);
            viewCollectRegister.Owner = this;
            viewCollectRegister.Show();
            this.Hide();
        }

        private void cmbHarvest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cmbHarvestDynamic.SelectedItem is HarvestDTO selectedHarvest) ||
                selectedHarvest.harvestName == "-- Seleccione una cosecha --" ||
                selectedHarvest.id == null)
            {
                cmbCollectorDynamic.DataSource = null;
                cmbCollectorDynamic.Items.Clear();
                cmbCollectorDynamic.Text = string.Empty;
                dgvCollectsDynamic.DataSource = null;
                dgvCollectsDynamic.Refresh();
                harvestRegister = null;
                collectorRegister = null;
                return;
            }

            loadCollectors(selectedHarvest.idPlot, selectedHarvest.id.Value);
            harvestRegister = selectedHarvest;

            dgvCollectsDynamic.DataSource = null;
            dgvCollectsDynamic.Refresh();
            collectorRegister = null;
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
                    MessageBox.Show("No hay recolectores asociados a esta cosecha.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("No se encontraron códigos de trabajadores válidos.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string workerCodesString = string.Join(",", workerCodes.Select(code => $"'{code}'"));

                var collectors = AppServices.CollectorServices.queryByIn.execute(workerCodesString);

                if (collectors == null || collectors.Count == 0)
                {
                    cmbCollectorDynamic.DataSource = null;
                    cmbCollectorDynamic.Items.Clear();
                    cmbCollectorDynamic.Text = string.Empty;
                    dgvCollectsDynamic.DataSource = null;
                    MessageBox.Show("No se encontraron recolectores.",
                                   "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            }
        }

        private void cmbCollector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (collects != null)
            {
                collects.Clear();
            }
            if (collectsDTO != null)
            {
                collectsDTO.Clear();
            }

            if (!(cmbHarvestDynamic.SelectedItem is HarvestDTO selectedHarvest) ||
                selectedHarvest.harvestName == "-- Seleccione una cosecha --")
            {
                dgvCollectsDynamic.DataSource = null;
                dgvCollectsDynamic.Refresh();
                collectorRegister = null;
                return;
            }

            if (!(cmbCollectorDynamic.SelectedItem is CollectorDTO selectedCollector) ||
                selectedCollector.displayName == "-- Seleccione un recolector --")
            {
                dgvCollectsDynamic.DataSource = null;
                dgvCollectsDynamic.Refresh();
                collectorRegister = null;
                return;
            }

            try
            {
                collects = AppServices.CollectServices.queryByWorkerCode.execute(
                    1,
                    selectedCollector.workerCode,
                    selectedHarvest.idPlot,
                    selectedHarvest.id);

                if (collects == null || collects.Count == 0)
                {
                    dgvCollectsDynamic.DataSource = null;
                    dgvCollectsDynamic.Refresh();
                    collectorRegister = selectedCollector;
                    MessageBox.Show($"El recolector {selectedCollector.displayName} aún no tiene recolectas registradas.",
                                   "Información",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information);
                    return;
                }

                collectsDTO = CollectMaper.ToDTOList(collects);

                // Invertir el orden para mostrar los más recientes primero
                collectsDTO.Reverse();

                dgvCollectsDynamic.DataSource = collectsDTO;
                collectorRegister = selectedCollector;

                // Asegurar que se carguen las columnas
                loadDgvCollects();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las recolectas: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                dgvCollectsDynamic.DataSource = null;
                collectorRegister = null;
            }
        }

        // Métodos existentes que se mantienen
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void ViewCollect_Load(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}