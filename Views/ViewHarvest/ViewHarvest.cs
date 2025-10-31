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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CAFEPAY.Views.ViewMain;

namespace CAFEPAY.Views.ViewHarvest
{
    public partial class ViewHarvest : Form
    {
        private string _lastSortProp;
        private bool _sortAsc = true;
        List<Harvest> listHarvest = new List<Harvest>();
        List<HarvestDTO> listHarvestDTO = new List<HarvestDTO>();

        // Colores exactos del FIGMA (igual que ViewCollector)
        private Color redColor = Color.FromArgb(183, 32, 46);     // #B7202E
        private Color darkBlueColor = Color.FromArgb(13, 43, 97); // #0D2B61
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);

       

        public ViewHarvest()
        {
            InitializeComponent();
            ApplyExactFigmaDesign();
            loadHarvests();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;

            // Conectar eventos
            dgHarvest.SelectionChanged += dgHarvest_SelectionChanged;
            dgHarvest.ColumnHeaderMouseClick += dgHarvest_ColumnHeaderMouseClick;
        }

        private void ApplyExactFigmaDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 800);
            this.Text = "CAFICAUCA - Consulta de Datos de Cosechas";

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
                var viewMain = new ViewMain.ViewMain();
                viewMain.Show();
                this.Close();
            };

            topHeaderPanel.Controls.Add(homeButton);
            topHeaderPanel.Controls.Add(logoPanel);

            // 🏷️ TÍTULO PRINCIPAL - "CONSULTA DE DATOS DE COSECHAS"
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
                Size = new Size(600, 70),
                Location = new Point((this.Width - 600) / 2, 0),
                BackColor = darkBlueColor,
                Anchor = AnchorStyles.None
            };

            // Rectángulo blanco interior
            var whiteInnerPanel = new Panel
            {
                Size = new Size(590, 60),
                Location = new Point(5, 5),
                BackColor = whiteColor
            };

            // Label del título
            var mainTitleLabel = new Label
            {
                Text = "CONSULTA DE DATOS DE COSECHAS",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 🔘 PANEL DE BOTONES SUPERIORES - CON BOTONES MODIFICADOS
            var buttonContainerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // Botón AZUL - "Agregar"
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.BackColor = darkBlueColor;
            btnAdd.ForeColor = whiteColor;
            btnAdd.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnAdd.Text = "Agregar";
            btnAdd.Size = new Size(200, 50);
            btnAdd.Location = new Point(buttonContainerPanel.Width / 4 - 100, 25);
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;
            

            // Botón ROJO - "Finalizar" ← NUEVO BOTÓN
            btnFinish.FlatStyle = FlatStyle.Flat;
            btnFinish.BackColor = redColor;
            btnFinish.ForeColor = whiteColor;
            btnFinish.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnFinish.Text = "Finalizar";
            btnFinish.Size = new Size(200, 50);
            btnFinish.Location = new Point(3 * buttonContainerPanel.Width / 4 - 100, 25);
            btnFinish.Anchor = AnchorStyles.None;
            btnFinish.Cursor = Cursors.Hand;
            btnFinish.FlatAppearance.BorderSize = 0;
            btnFinish.Enabled = false; // Inicialmente deshabilitado hasta que se seleccione una cosecha
            btnFinish.Click += btnFinish_Click;

            // Aplicar esquinas redondeadas a los botones
            ApplyRoundedCorners(btnAdd, 10);
            ApplyRoundedCorners(btnFinish, 10);

            buttonContainerPanel.Controls.Add(btnFinish);
            buttonContainerPanel.Controls.Add(btnAdd);

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

            // Configurar DataGridView
            ConfigureDataGridFigmaStyle();

            // Agregar DataGridView al panel con borde azul
            dataGridContainerPanel.Controls.Add(dgHarvest);

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
                Text = "inicio / cosechas",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };

            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES AL FORMULARIO EN ORDEN CORRECTO
            this.Controls.Add(mainContentPanel);
            this.Controls.Add(buttonContainerPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Asegurar que los controles se redimensionen correctamente
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                btnAdd.Location = new Point(buttonContainerPanel.Width / 4 - 100, 25);
                btnFinish.Location = new Point(3 * buttonContainerPanel.Width / 4 - 100, 25);
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
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nCOSECHAS",
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
            dgHarvest.BorderStyle = BorderStyle.None;
            dgHarvest.BackgroundColor = whiteColor;
            dgHarvest.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgHarvest.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgHarvest.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgHarvest.RowHeadersVisible = false;
            dgHarvest.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgHarvest.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgHarvest.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgHarvest.EnableHeadersVisualStyles = false;
            dgHarvest.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgHarvest.MultiSelect = false;
            dgHarvest.ReadOnly = true;
            dgHarvest.Dock = DockStyle.Fill;

            // Estilo de encabezados
            dgHarvest.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgHarvest.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgHarvest.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgHarvest.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgHarvest.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
            dgHarvest.ColumnHeadersHeight = 45;

            // Estilo de celdas
            dgHarvest.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgHarvest.DefaultCellStyle.BackColor = whiteColor;
            dgHarvest.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgHarvest.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgHarvest.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgHarvest.RowTemplate.Height = 45;
        }

        private void ViewHarvest_Load(object sender, EventArgs e) { }

        private void dgHarvest_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var col = dgHarvest.Columns[e.ColumnIndex];
            var prop = col.DataPropertyName;
            if (string.IsNullOrWhiteSpace(prop)) return;

            _sortAsc = (_lastSortProp == prop) ? !_sortAsc : true;
            _lastSortProp = prop;

            Func<HarvestDTO, object> key = x => x?.GetType().GetProperty(prop)?.GetValue(x, null);
            var sorted = _sortAsc
                ? listHarvestDTO.OrderBy(key).ToList()
                : listHarvestDTO.OrderByDescending(key).ToList();

            dgHarvest.DataSource = null;
            dgHarvest.DataSource = sorted;
        }

        public void loadHarvests()
        {
            try
            {
                // 1) Traer cosechas
                listHarvest = AppServices.HarvestServices.query.execute();
                listHarvestDTO = HarvestMaper.ToDTOList(listHarvest);

                // 2) Traer lotes y mapear a diccionario id->nombre
                var plots = AppServices.PlotServices.query.execute();
                var plotsDTO = PlotMapper.ToDTOList(plots);
                var plotNameById = plotsDTO.ToDictionary(p => p.idPlot, p => p.name);

                // 3) Completar nombre de lote en cada DTO
                foreach (var h in listHarvestDTO)
                    h.plotName = plotNameById.TryGetValue(h.idPlot, out var name) ? name : "(desconocido)";

                // 4) Orden: activas primero
                listHarvestDTO = listHarvestDTO
                    .OrderByDescending(h => h.status == 1 && h.endDate == null)
                    .ThenByDescending(h => h.startDate)
                    .ToList();

                // 5) Bind al grid
                dgHarvest.AutoGenerateColumns = false;
                dgHarvest.Columns.Clear();
                AddColumn("idPlot", "Parcela Id", 110);
                AddColumn("plotName", "Nombre de lote", 180);
                AddColumn("id", "Cosecha Id", 110);
                AddColumn("startDate", "Fecha Inicio", 120);
                AddColumn("endDate", "Fecha Fin", 120);
                AddColumn("pricePerKilo", "Precio por Kilo", 130);
                AddColumn("statusText", "Estado", 100);

                dgHarvest.DataSource = listHarvestDTO;
                dgHarvest.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las cosechas: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
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
            dgHarvest.Columns.Add(column);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgHarvest.CurrentCell != null)
                dgHarvest.ClearSelection();

            ViewHarvestRegister viewHarvestRegister = new ViewHarvestRegister();
            viewHarvestRegister.Owner = this;
            viewHarvestRegister.Show();
            this.Hide();
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            if (dgHarvest.CurrentCell == null)
            {
                MessageBox.Show("Por favor, seleccione una cosecha para finalizar.",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

            int rowSelected = dgHarvest.CurrentCell.RowIndex;

            if (rowSelected < 0 || rowSelected >= listHarvestDTO.Count)
            {
                MessageBox.Show("La selección no es válida.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            var selectedHarvest = listHarvestDTO[rowSelected];
            if (selectedHarvest == null)
            {
                MessageBox.Show("La cosecha seleccionada no es válida.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            // Aquí puedes implementar la lógica para finalizar la cosecha
            MessageBox.Show($"Finalizar cosecha ID: {selectedHarvest.id}\nLote: {selectedHarvest.plotName}",
                          "Finalizar Cosecha",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Information);
        }

        private void dgHarvest_SelectionChanged(object sender, EventArgs e)
        {
            btnFinish.Enabled = dgHarvest.CurrentCell != null;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                loadHarvests();
                dgHarvest.ClearSelection();
                btnFinish.Enabled = false;
            }
        }
    }
}