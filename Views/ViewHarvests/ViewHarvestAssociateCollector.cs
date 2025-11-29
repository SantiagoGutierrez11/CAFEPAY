using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Collects.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Plots.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewHarvests
{
    public partial class ViewHarvestAssociateCollector : Form
    {
        private PlotDTO plotDTO;
        private HarvestDTO harvestDTO;
        private List<Collector> listCollector;
        private List<CollectorDTO> listDTOCollector;

        // Colores exactos del FIGMA
        private Color redColor = Color.FromArgb(183, 32, 46);     // #B7202E
        private Color darkBlueColor = Color.FromArgb(13, 43, 97); // #0D2B61
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);
        private Color greenColor = Color.FromArgb(34, 139, 34);   // Verde para el botón Asociar

        // Controles
        private DataGridView dgCollectors = new DataGridView();
        private Button btnAssociate = new Button();
        private Button btnBack = new Button();

        // Campos de información (ahora son Labels)
        private Label lblPlotName = new Label();
        private Label lblPlotId = new Label();
        private Label lblHarvestId = new Label();
        private Label lblStartDate = new Label();
        private Label lblPricePerKilo = new Label();

        public ViewHarvestAssociateCollector(PlotDTO plotDTO, HarvestDTO harvestDTO)
        {
            this.plotDTO = plotDTO;
            this.harvestDTO = harvestDTO;
            ApplyExactFigmaDesign();
            loadCollectors();
        }

        public ViewHarvestAssociateCollector()
        {
            ApplyExactFigmaDesign();
        }

        private void ApplyExactFigmaDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 800);
            this.Text = "CAFICAUCA - Asociar Recolectores a Cosecha";
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
                // Volver al menú principal
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
                Size = new Size(700, 70),
                Location = new Point((this.Width - 700) / 2, 0),
                BackColor = darkBlueColor,
                Anchor = AnchorStyles.None
            };

            var whiteInnerPanel = new Panel
            {
                Size = new Size(690, 60),
                Location = new Point(5, 5),
                BackColor = whiteColor
            };

            var mainTitleLabel = new Label
            {
                Text = "ASOCIAR RECOLECTORES A COSECHA ACTIVA",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            // 📋 PANEL DE INFORMACIÓN DE LA COSECHA
            var infoPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 130, // Reducida porque ahora es más compacto
                BackColor = whiteColor,
                Padding = new Padding(40, 15, 40, 10) // Padding ajustado
            };

            // Crear tabla de información
            CreateHarvestInfoTable(infoPanel);

            // 🔘 PANEL DE BOTONES
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 15, 40, 15)
            };

            // Botón VOLVER (Azul)
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.BackColor = darkBlueColor;
            btnBack.ForeColor = whiteColor;
            btnBack.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnBack.Text = "Volver";
            btnBack.Size = new Size(150, 50);
            btnBack.Location = new Point(buttonPanel.Width / 4 - 75, 15);
            btnBack.Anchor = AnchorStyles.None;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Click += btnBack_Click;

            // Botón ASOCIAR (Verde)
            btnAssociate.FlatStyle = FlatStyle.Flat;
            btnAssociate.BackColor = greenColor;
            btnAssociate.ForeColor = whiteColor;
            btnAssociate.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnAssociate.Text = "Asociar Recolectores";
            btnAssociate.Size = new Size(200, 50);
            btnAssociate.Location = new Point(3 * buttonPanel.Width / 4 - 100, 15);
            btnAssociate.Anchor = AnchorStyles.None;
            btnAssociate.Cursor = Cursors.Hand;
            btnAssociate.FlatAppearance.BorderSize = 0;
            btnAssociate.Click += btnAssociate_Click;

            ApplyRoundedCorners(btnBack, 10);
            ApplyRoundedCorners(btnAssociate, 10);

            buttonPanel.Controls.Add(btnBack);
            buttonPanel.Controls.Add(btnAssociate);

            // 📊 PANEL PRINCIPAL DE CONTENIDO (DataGridView)
            var mainContentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = whiteColor,
                Padding = new Padding(40, 20, 40, 60)
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
            dataGridContainerPanel.Controls.Add(dgCollectors);
            mainContentPanel.Controls.Add(dataGridContainerPanel);

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
                Text = "inicio / cosechas / asociar recolectores",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };
            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES
            this.Controls.Add(mainContentPanel);
            this.Controls.Add(buttonPanel);
            this.Controls.Add(infoPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Ajustar redimensionamiento
            // En el método ApplyExactFigmaDesign, actualiza el evento Resize:
            this.Resize += (s, e) => {
                blueOuterPanel.Location = new Point((titleContainerPanel.Width - blueOuterPanel.Width) / 2, 0);
                btnBack.Location = new Point(buttonPanel.Width / 4 - 75, 15);
                btnAssociate.Location = new Point(3 * buttonPanel.Width / 4 - 100, 15);

                // 🔥 REDISTRIBUIR LA INFORMACIÓN AL REDIMENSIONAR
                if (infoPanel != null)
                {
                    infoPanel.Controls.Clear();
                    CreateHarvestInfoTable(infoPanel);
                    LoadHarvestInfo();
                }
            };

            // Cargar información de la cosecha
            LoadHarvestInfo();
        }

        private void CreateHarvestInfoTable(Panel container)
        {
            int containerWidth = container.Width - 80; // 40px padding en cada lado

            // Calcular anchos para dos columnas
            int columnWidth = (containerWidth - 60) / 2; // 60px de separación entre columnas
            int labelWidth = 160; // Un poco más ancho para etiquetas más largas
            int valueWidth = columnWidth - labelWidth - 20; // Espacio para valores

            int startY = 20;
            int rowHeight = 35; // Más compacto

            // COLUMNA IZQUIERDA - 3 campos
            CreateInfoField(container, "Nombre del lote:", lblPlotName, 40, startY, labelWidth, valueWidth);
            CreateInfoField(container, "Cosecha ID:", lblHarvestId, 40, startY + rowHeight, labelWidth, valueWidth);
            CreateInfoField(container, "Precio por kilo:", lblPricePerKilo, 40, startY + (rowHeight * 2), labelWidth, valueWidth);

            // COLUMNA DERECHA - 2 campos
            int rightColumnX = 40 + columnWidth + 20; // 20px de separación entre columnas
            CreateInfoField(container, "Parcela ID:", lblPlotId, rightColumnX, startY, labelWidth, valueWidth);
            CreateInfoField(container, "Fecha de inicio:", lblStartDate, rightColumnX, startY + rowHeight, labelWidth, valueWidth);
        }

        private void CreateInfoField(Panel container, string labelText, Label valueLabel, int x, int y, int labelWidth, int valueWidth)
        {
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold), // Tamaño aumentado
                ForeColor = darkBlueColor,
                Location = new Point(x, y),
                Size = new Size(labelWidth, 28), // Un poco más alto
                TextAlign = ContentAlignment.MiddleLeft
            };

            valueLabel.Font = new Font("Segoe UI", 11, FontStyle.Regular); // Tamaño aumentado
            valueLabel.Location = new Point(x + labelWidth + 8, y); // +8 para separación
            valueLabel.Size = new Size(valueWidth, 28); // Un poco más alto
            valueLabel.ForeColor = Color.FromArgb(60, 60, 60);
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            valueLabel.BackColor = Color.Transparent;
            valueLabel.Cursor = Cursors.Default;

            container.Controls.Add(label);
            container.Controls.Add(valueLabel);
        }

        private void LoadHarvestInfo()
        {
            if (plotDTO != null && harvestDTO != null)
            {
                lblPlotName.Text = plotDTO.name;
                lblPlotId.Text = plotDTO.idPlot.ToString();
                lblHarvestId.Text = harvestDTO.id.ToString();
                lblStartDate.Text = harvestDTO.startDate.ToString("dd/MM/yyyy");

                // Formato mejorado para el precio con tamaño consistente
                lblPricePerKilo.Text = string.Format("$ {0:#,##0.00}", harvestDTO.pricePerKilo);
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
                Text = "CAFICAUCA\nCOOPERATIVA DE CAFICULTORES DEL CAUCA\nASOCIAR RECOLECTORES",
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
            dgCollectors.MultiSelect = true;
            dgCollectors.ReadOnly = true;
            dgCollectors.Dock = DockStyle.Fill;

            // Estilo de encabezados
            dgCollectors.ColumnHeadersDefaultCellStyle.BackColor = darkBlueColor;
            dgCollectors.ColumnHeadersDefaultCellStyle.ForeColor = whiteColor;
            dgCollectors.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgCollectors.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgCollectors.ColumnHeadersDefaultCellStyle.Padding = new Padding(15, 0, 0, 0);
            dgCollectors.ColumnHeadersHeight = 45;

            // Estilo de celdas
            dgCollectors.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgCollectors.DefaultCellStyle.BackColor = whiteColor;
            dgCollectors.DefaultCellStyle.ForeColor = Color.FromArgb(60, 60, 60);
            dgCollectors.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgCollectors.DefaultCellStyle.Padding = new Padding(15, 10, 15, 10);
            dgCollectors.RowTemplate.Height = 45;
        }

        public void loadCollectors()
        {
            try
            {
                listCollector = AppServices.CollectorServices.queryByStatus.execute(1);
                listDTOCollector = CollectorMaper.ToDTOList(listCollector);

                dgCollectors.AutoGenerateColumns = false;
                dgCollectors.Columns.Clear();

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
                dgCollectors.Columns.Add(colStatus);

                dgCollectors.DataSource = listDTOCollector;
                dgCollectors.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los recolectores: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            dgCollectors.Columns.Add(column);
        }

        private void btnAssociate_Click(object sender, EventArgs e)
        {
            // 🔹 MANTENER TODA LA LÓGICA ORIGINAL DE ASOCIACIÓN
            if (dgCollectors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos un recolector para asociar.",
                              "Selección requerida", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (harvestDTO == null)
            {
                MessageBox.Show("No se ha cargado la información de la cosecha.",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                List<CollectDTO> collectsZero = new List<CollectDTO>();

                foreach (DataGridViewRow row in dgCollectors.SelectedRows)
                {
                    if (row.DataBoundItem is CollectorDTO collector && !string.IsNullOrWhiteSpace(collector.workerCode))
                    {
                        CollectDTO collect = new CollectDTO
                        {
                            collectId = null,
                            plotId = plotDTO.idPlot,
                            harvestId = harvestDTO.id,
                            collectorWorkerCode = collector.workerCode,
                            collectedKilos = 0,
                            collectDate = DateTime.Today,
                            amountToPaid = 0,
                            isCountable = 0,
                            status = 0,
                            statusText = "ZERO"
                        };
                        collectsZero.Add(collect);
                    }
                }

                if (collectsZero.Count == 0)
                {
                    MessageBox.Show("No se encontraron recolectores válidos para asociar.",
                                  "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<string> exitosos = new List<string>();
                List<string> fallidos = new List<string>();

                foreach (CollectDTO collectFor in collectsZero)
                {
                    try
                    {
                        AppServices.CollectServices.save.execute(
                            collectFor.collectId,
                            collectFor.collectorWorkerCode,
                            collectFor.collectDate,
                            collectFor.collectedKilos,
                            collectFor.harvestId,
                            collectFor.status,
                            collectFor.amountToPaid,
                            collectFor.plotId,
                            collectFor.isCountable
                        );
                        exitosos.Add($"✓ Recolector {collectFor.collectorWorkerCode} asociado exitosamente");
                    }
                    catch (InvalidOperationException ex)
                    {
                        string errorMsg = ex.Message;
                        if (errorMsg.Contains("Ya existe un registro ZERO") || errorMsg.Contains("ya está asociado"))
                        {
                            fallidos.Add($"✗ Recolector {collectFor.collectorWorkerCode} ya está asociado");
                        }
                        else
                        {
                            fallidos.Add($"✗ Recolector {collectFor.collectorWorkerCode} falló: {errorMsg}");
                        }
                    }
                    catch (Exception ex)
                    {
                        fallidos.Add($"✗ Recolector {collectFor.collectorWorkerCode} falló: {ex.Message}");
                    }
                }

                StringBuilder mensaje = new StringBuilder();
                mensaje.AppendLine("Resultado de la asociación de recolectores:");
                mensaje.AppendLine();

                if (exitosos.Count > 0)
                {
                    mensaje.AppendLine("EXITOSOS:");
                    foreach (string msg in exitosos) mensaje.AppendLine(msg);
                    mensaje.AppendLine();
                }

                if (fallidos.Count > 0)
                {
                    mensaje.AppendLine("FALLIDOS:");
                    foreach (string msg in fallidos) mensaje.AppendLine(msg);
                }

                MessageBoxIcon icono = fallidos.Count == 0 ? MessageBoxIcon.Information :
                                     exitosos.Count == 0 ? MessageBoxIcon.Error : MessageBoxIcon.Warning;
                string titulo = fallidos.Count == 0 ? "Éxito" : exitosos.Count == 0 ? "Error" : "Resultado Parcial";

                MessageBox.Show(mensaje.ToString(), titulo, MessageBoxButtons.OK, icono);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al procesar los recolectores: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }
    }
}