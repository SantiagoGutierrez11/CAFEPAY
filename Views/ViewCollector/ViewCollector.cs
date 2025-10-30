using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewCollector
{
    public partial class ViewCollector : Form
    {
        private List<Collector> listCollector;
        private List<CollectorDTO> listDTOCollector;

        // Colores exactos del FIGMA
        private Color redColor = Color.FromArgb(183, 32, 46);     // #B7202E
        private Color darkBlueColor = Color.FromArgb(13, 43, 97); // #0D2B61
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);

        public ViewCollector()
        {
            InitializeComponent();
            ApplyExactFigmaDesign();
            loadCollectors();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;

            // Conectar eventos
            dgCollector.SelectionChanged += dgCollector_SelectionChanged;

        }
            
        private void ApplyExactFigmaDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 800);
            this.Text = "CAFICAUCA - Consulta de Datos de Recolectores";

            // 🔝 ENCABEZADO SUPERIOR - Logo CAFICAUCA
            var topHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90, // Un poco más alto para la imagen más grande
                BackColor = whiteColor,
                Padding = new Padding(20, 10, 40, 10) // Menos padding izquierdo para más a la esquina
            };

            // Panel del logo - MÁS GRANDE y MÁS A LA ESQUINA
            var logoPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 350, // Más ancho para imagen más grande
                BackColor = Color.Transparent,
                Height = 70,
                Padding = new Padding(10, 0, 0, 0) // Pegado a la izquierda
            };

            // 🖼️ CARGAR IMAGEN DESDE CARPETA RESOURCES - MÁS GRANDE
            try
            {
                // Ruta de la imagen en la carpeta Resources
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "LOGO-CAFICAUCA.png");

                if (File.Exists(imagePath))
                {
                    PictureBox logoPicture = new PictureBox();
                    logoPicture.Image = Image.FromFile(imagePath);
                    logoPicture.SizeMode = PictureBoxSizeMode.Zoom;
                    logoPicture.Size = new Size(320, 70); // MÁS GRANDE: 320x70
                    logoPicture.Location = new Point(5, 5); // MÁS PEGADO A LA ESQUINA
                    logoPicture.Cursor = Cursors.Hand;

                    // Tooltip para la imagen
                    ToolTip toolTip = new ToolTip();
                    toolTip.SetToolTip(logoPicture, "CAFICAUCA - Cooperativa de Caficultores del Cauca");

                    logoPanel.Controls.Add(logoPicture);
                }
                else
                {
                    // Si no encuentra la imagen, mostrar simulación
                    CreateSimulatedLogo(logoPanel);
                }
            }
            catch (Exception)
            {
                // 🖼️ SIMULACIÓN DEL LOGO en caso de error
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
                // Acción al hacer clic en el botón home
                MessageBox.Show("Volviendo al menú principal...", "Información",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            };

            topHeaderPanel.Controls.Add(homeButton);
            topHeaderPanel.Controls.Add(logoPanel);

            // 🏷️ TÍTULO PRINCIPAL - "CONSULTA DE DATOS DE RECOLECTORES" (EN ESPAÑOL)
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

            // Label del título EN ESPAÑOL
            var mainTitleLabel = new Label
            {
                Text = "CONSULTA DE DATOS DE RECOLECTORES",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 🔘 PANEL DE BOTONES SUPERIORES (ARRIBA DEL DATAGRIDVIEW)
            var buttonContainerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 100,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            // Botón AZUL - "Registrar Recolector"
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.BackColor = darkBlueColor;
            btnAdd.ForeColor = whiteColor;
            btnAdd.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnAdd.Text = "Registrar Recolector";
            btnAdd.Size = new Size(200, 50);
            btnAdd.Location = new Point(buttonContainerPanel.Width / 4 - 100, 25);
            btnAdd.Anchor = AnchorStyles.None;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatAppearance.BorderSize = 0;

            // Botón ROJO - "Modificar"
            btnModify.FlatStyle = FlatStyle.Flat;
            btnModify.BackColor = redColor;
            btnModify.ForeColor = whiteColor;
            btnModify.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnModify.Text = "Modificar";
            btnModify.Size = new Size(200, 50);
            btnModify.Location = new Point(3 * buttonContainerPanel.Width / 4 - 100, 25);
            btnModify.Anchor = AnchorStyles.None;
            btnModify.Cursor = Cursors.Hand;
            btnModify.FlatAppearance.BorderSize = 0;
            btnModify.Enabled = false;

            // Aplicar esquinas redondeadas a los botones
            ApplyRoundedCorners(btnAdd, 10);
            ApplyRoundedCorners(btnModify, 10);

            buttonContainerPanel.Controls.Add(btnModify);
            buttonContainerPanel.Controls.Add(btnAdd);

            // 📊 PANEL PRINCIPAL DE CONTENIDO (DataGridView) - CON BORDE AZUL
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
                BackColor = darkBlueColor, // Color azul del borde
                Padding = new Padding(2), // Grosor del borde
                Margin = new Padding(0, 10, 0, 0)
            };

            // Configurar DataGridView
            ConfigureDataGridFigmaStyle();

            // Agregar DataGridView al panel con borde azul
            dataGridContainerPanel.Controls.Add(dgCollector);

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
                Text = "inicio / recolector",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };

            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES AL FORMULARIO EN ORDEN CORRECTO
            this.Controls.Add(mainContentPanel);
            this.Controls.Add(buttonContainerPanel); // Botones ARRIBA del DataGridView
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Asegurar que los controles se redimensionen correctamente
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                btnAdd.Location = new Point(buttonContainerPanel.Width / 4 - 100, 25);
                btnModify.Location = new Point(3 * buttonContainerPanel.Width / 4 - 100, 25);
            };
        }

        private void CreateSimulatedLogo(Panel logoPanel)
        {
            var simulatedLogoPanel = new Panel
            {
                Size = new Size(320, 70), // MÁS GRANDE
                Location = new Point(5, 5), // MÁS PEGADO A LA ESQUINA
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle
            };

            var logoText = new Label
            {
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nRECOLECTORES", // AGREGADO "RECOLECTORES"
                Font = new Font("Segoe UI", 9, FontStyle.Bold), // Fuente un poco más grande
                ForeColor = darkBlueColor,
                Location = new Point(15, 8), // Ajustado para tamaño más grande
                AutoSize = false,
                Size = new Size(290, 55), // Más grande
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Línea decorativa roja MÁS GRANDE
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
            dgCollector.BorderStyle = BorderStyle.None;
            dgCollector.BackgroundColor = whiteColor;
            dgCollector.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 250);
            dgCollector.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgCollector.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            dgCollector.RowHeadersVisible = false;
            dgCollector.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgCollector.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgCollector.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgCollector.EnableHeadersVisualStyles = false;
            dgCollector.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgCollector.MultiSelect = false;
            dgCollector.ReadOnly = true;
            dgCollector.Dock = DockStyle.Fill;

            // Estilo de encabezados
            dgCollector.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgCollector.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgCollector.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgCollector.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgCollector.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
            dgCollector.ColumnHeadersHeight = 45;

            // Estilo de celdas
            dgCollector.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgCollector.DefaultCellStyle.BackColor = whiteColor;
            dgCollector.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgCollector.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgCollector.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgCollector.RowTemplate.Height = 45;
        }

        public void loadCollectors()
        {
            try
            {
                listCollector = AppServices.CollectorServices.query.execute();
                listDTOCollector = CollectorMaper.ToDTOList(listCollector);

                dgCollector.AutoGenerateColumns = false;
                dgCollector.Columns.Clear();

                // Columnas según diseño
                AddColumn("workerCode", "ID TRABAJADOR", 150);
                AddColumn("id", "CÉDULA", 130);
                AddColumn("firstName", "NOMBRES", 180);
                AddColumn("lastName", "APELLIDOS", 180);
                AddColumn("phone", "TELÉFONO", 150);

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
                    Width = 120
                };
                dgCollector.Columns.Add(colStatus);

                dgCollector.DataSource = listDTOCollector;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los recolectores: {ex.Message}",
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
            dgCollector.Columns.Add(column);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dgCollector.CurrentCell != null)
                dgCollector.ClearSelection();

            ViewCollectorRegister viewCollectorRegister = new ViewCollectorRegister();
            viewCollectorRegister.Owner = this;
            viewCollectorRegister.Show();
            this.Hide();
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            // VALIDACIONES COMPLETAS DEL CÓDIGO ORIGINAL
            if (dgCollector.CurrentCell == null)
            {
                MessageBox.Show("Por favor, seleccione un recolector para modificar.",
                              "Selección requerida",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
                return;
            }

            int rowSelected = dgCollector.CurrentCell.RowIndex;

            if (rowSelected < 0 || rowSelected >= listDTOCollector.Count)
            {
                MessageBox.Show("La selección no es válida.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            var selectedCollector = listDTOCollector[rowSelected];
            if (selectedCollector == null)
            {
                MessageBox.Show("El recolector seleccionado no es válido.",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }

            ViewCollectorModify viewCollectorModify = new ViewCollectorModify(selectedCollector, this);
            viewCollectorModify.Owner = this;
            viewCollectorModify.Show();
            this.Hide();
        }

        private void dgCollector_SelectionChanged(object sender, EventArgs e)
        {
            btnModify.Enabled = dgCollector.CurrentCell != null;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                loadCollectors();
                dgCollector.ClearSelection();
                btnModify.Enabled = false;
            }
        }

        private void dgCollector_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
        }

        private void ViewCollector_Load(object sender, EventArgs e)
        {
            // -----
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            // -----
        }
    }
}