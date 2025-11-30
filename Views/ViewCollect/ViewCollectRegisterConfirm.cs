using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
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

namespace CAFEPAY.Views.ViewCollect
{
    public partial class ViewCollectRegisterConfirm : Form
    {
        private HarvestDTO harvestRegister;
        private CollectorDTO collectorRegister;
        private CollectDTO collectRegister;
        private Form viewCollect;

        // Colores exactos del diseño CAFICAUCA
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Confirmar
        private Color redColor = Color.FromArgb(183, 32, 46);      // #B7202E - Rojo del botón Cancelar
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);  // Gris oscuro para el botón home

        // Botones
        private Button btnConfirm = new Button();
        private Button btnCancel = new Button();

        // Botón home
        private Button homeButton;

        public ViewCollectRegisterConfirm(CollectorDTO _collectorRegister, HarvestDTO _harvestRegister, CollectDTO _collectRegister, Form _viewCollect)
        {
            InitializeComponent();
            this.collectorRegister = _collectorRegister;
            this.harvestRegister = _harvestRegister;
            this.collectRegister = _collectRegister;
            this.viewCollect = _viewCollect;
            ApplyProfessionalDesign();
            LoadData();

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
            this.Text = "CAFICAUCA - Confirmar Registro de Recolección";

            // 🔝 ENCABEZADO SUPERIOR - Logo CAFICAUCA
            var topHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = whiteColor,
                Padding = new Padding(20, 10, 40, 10)
            };

            // Panel del logo
            var logoPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 300,
                BackColor = Color.Transparent,
                Height = 50
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
                    logoPicture.Size = new Size(280, 50);
                    logoPicture.Location = new Point(10, 0);
                    logoPanel.Controls.Add(logoPicture);
                }
                else
                {
                    CreateLogoPlaceholder(logoPanel);
                }
            }
            catch (Exception)
            {
                CreateLogoPlaceholder(logoPanel);
            }

            // 🏠 BOTÓN HOME (esquina superior derecha)
            topHeaderPanel.Controls.Add(logoPanel);

            // 🏷️ TÍTULO PRINCIPAL - SOLO TEXTO
            var titleContainerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60, // Más compacto
                BackColor = Color.Transparent, // Fondo transparente
                Padding = new Padding(0, 5, 0, 0) // Más padding arriba para subir el texto
            };

            // Título principal centrado - SOLO TEXTO
            var mainTitleLabel = new Label
            {
                Text = "Confirmación del Registro",
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = darkBlueColor, // Texto azul
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                BackColor = Color.Transparent // Fondo transparente
            };
            titleContainerPanel.Controls.Add(mainTitleLabel);

            // 📦 PANEL PRINCIPAL - MÁS ARRIBA
            var mainFormPanel = new Panel
            {
                Size = new Size(1100, 650),
                BackColor = darkBlueColor,
                Padding = new Padding(40, 25, 40, 30),
                Location = new Point((this.Width - 1100) / 2, 20) // Cambiado a 50 para subirlo más
            };
            ApplyRoundedCorners(mainFormPanel, 15);
            ApplyLightBlueBorder(mainFormPanel, 3);

            // 📊 CONTENEDOR DE LOS 3 PANELES DE INFORMACIÓN
            var contentPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = false
            };

            // Crear los 3 paneles de información en distribución vertical
            CreateInformationPanelsVertical(contentPanel);

            mainFormPanel.Controls.Add(contentPanel);

            // 🔘 PANEL DE BOTONES
            var buttonsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 15, 0, 0)
            };

            ConfigureButtonsDesign(buttonsPanel);
            mainFormPanel.Controls.Add(buttonsPanel);

            // 📍 BREADCRUMB
            var breadcrumbPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 35,
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(40, 8, 40, 8)
            };

            var breadcrumbLabel = new Label
            {
                Text = "inicio / recolectas / agregar recolección / confirmar",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };
            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES
            this.Controls.Add(mainFormPanel);
            this.Controls.Add(mainTitleLabel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Ajustar redimensionamiento
            this.Resize += (s, e) => {
                mainFormPanel.Location = new Point((this.Width - mainFormPanel.Width) / 2, 70);
               
            };
        }

        private void CreateInformationPanelsVertical(Panel container)
        {
            int panelWidth = 1000;
            int panelHeight = 150; // Altura ajustada para mostrar más información
            int spacing = 15;

            // 🟦 PANEL 1: DATOS DE LA COSECHA
            var harvestPanel = CreateInfoPanel("Datos de la Cosecha", 0, panelWidth, panelHeight);
            AddHarvestInfoToPanel(harvestPanel);
            container.Controls.Add(harvestPanel);

            // 🟦 PANEL 2: DATOS DEL RECOLECTOR
            var collectorPanel = CreateInfoPanel("Datos del Recolector", panelHeight + spacing, panelWidth, panelHeight);
            AddCollectorInfoToPanel(collectorPanel);
            container.Controls.Add(collectorPanel);

            // 🟦 PANEL 3: DATOS DE LA RECOLECTA
            var collectPanel = CreateInfoPanel("Datos de la Recolecta", (panelHeight + spacing) * 2, panelWidth, panelHeight);
            AddCollectInfoToPanel(collectPanel);
            container.Controls.Add(collectPanel);
        }

        private Panel CreateInfoPanel(string title, int y, int width, int height)
        {
            var panel = new Panel
            {
                Size = new Size(width, height),
                Location = new Point(0, y),
                BackColor = Color.FromArgb(25, 55, 109),
                Padding = new Padding(25, 15, 25, 12)
            };
            ApplyRoundedCorners(panel, 10);
            ApplyLightBlueBorder(panel, 2);

            // Título del panel en negrita
            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = whiteColor,
                Dock = DockStyle.Top,
                Height = 22,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 0, 15)
            };
            panel.Controls.Add(titleLabel);

            return panel;
        }

        private void AddHarvestInfoToPanel(Panel panel)
        {
            int currentY = 45;
            int labelWidth = 180;
            int valueWidth = 200;

            // Tres columnas para los tres datos
            int column1X = 30;
            int column2X = 300;
            int column3X = 600;

            CreateInfoField(panel, "Nombre de lote:", harvestRegister.plotName, column1X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Id del lote:", harvestRegister.idPlot.ToString(), column2X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Número de la cosecha:", harvestRegister.id.ToString(), column3X, currentY, labelWidth, valueWidth);
        }

        private void AddCollectorInfoToPanel(Panel panel)
        {
            int currentY = 45;
            int labelWidth = 180;
            int valueWidth = 200;

            // Tres columnas para los tres datos
            int column1X = 30;
            int column2X = 300;
            int column3X = 600;

            CreateInfoField(panel, "Id del trabajador:", collectorRegister.workerCode, column1X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Cédula:", collectorRegister.id.ToString(), column2X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Nombre:", $"{collectorRegister.firstName} {collectorRegister.lastName}", column3X, currentY, labelWidth, valueWidth);
        }

        private void AddCollectInfoToPanel(Panel panel)
        {
            int currentY = 45;
            int labelWidth = 180;
            int valueWidth = 250;

            // Tres columnas para los datos de recolección
            int column1X = 30;
            int column2X = 300;
            int column3X = 600;

            // Calcular el valor a pagar
            decimal amountToPay = collectRegister.collectedKilos * harvestRegister.pricePerKilo;

            CreateInfoField(panel, "Kilos recogidos:", collectRegister.collectedKilos.ToString(), column1X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Fecha:", collectRegister.collectDate.ToString("yyyy-MM-dd"), column2X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Estado:", collectRegister.statusText, column3X, currentY, labelWidth, valueWidth);

            // Valor a pagar en una fila adicional centrada
            var amountLabel = new Label
            {
                Text = "Valor a pagar:",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = whiteColor,
                Location = new Point(column1X, currentY + 50),
                Size = new Size(150, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(amountLabel);

            var amountValue = new Label
            {
                Text = amountToPay.ToString("C2"),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(255, 215, 0), // Color dorado para destacar
                Location = new Point(column1X + 160, currentY + 48),
                Size = new Size(250, 30),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(amountValue);
        }

        private void CreateInfoField(Panel panel, string labelText, string value, int x, int y, int labelWidth, int valueWidth)
        {
            // Label (en negrita)
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 220, 220),
                Location = new Point(x, y),
                Size = new Size(labelWidth, 20),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panel.Controls.Add(label);

            // Valor
            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = whiteColor,
                Location = new Point(x, y + 20),
                Size = new Size(valueWidth, 22),
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };
            panel.Controls.Add(valueLabel);
        }

        private void ConfigureButtonsDesign(Panel buttonPanel)
        {
            int panelWidth = 1000;
            int buttonWidth = 200;
            int buttonHeight = 45;
            int spacing = 40;

            // Calcular posiciones para centrado perfecto
            int totalWidth = (buttonWidth * 2) + spacing;
            int startX = (panelWidth - totalWidth) / 2;

            // Botón CANCELAR (Rojo)
            btnCancel.Text = "CANCELAR";
            btnCancel.BackColor = redColor;
            btnCancel.ForeColor = whiteColor;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnCancel.Size = new Size(buttonWidth, buttonHeight);
            btnCancel.Location = new Point(startX, 20);
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Visible = true;
            btnCancel.Click += btnCancel_Click;

            // Botón CONFIRMAR (Verde)
            btnConfirm.Text = "CONFIRMAR";
            btnConfirm.BackColor = greenColor;
            btnConfirm.ForeColor = whiteColor;
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnConfirm.Size = new Size(buttonWidth, buttonHeight);
            btnConfirm.Location = new Point(startX + buttonWidth + spacing, 20);
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.Visible = true;
            btnConfirm.Click += btnConfirm_Click;

            ApplyRoundedCorners(btnCancel, 8);
            ApplyRoundedCorners(btnConfirm, 8);

            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnConfirm);
        }

        private void CreateLogoPlaceholder(Panel logoPanel)
        {
            var placeholder = new Panel
            {
                Size = new Size(280, 50),
                Location = new Point(10, 0),
                BackColor = Color.FromArgb(240, 240, 240),
                BorderStyle = BorderStyle.FixedSingle
            };

            var logoText = new Label
            {
                Text = "CAFICAUCA",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Location = new Point(10, 15),
                AutoSize = false,
                Size = new Size(260, 25),
                TextAlign = ContentAlignment.MiddleLeft
            };

            placeholder.Controls.Add(logoText);
            logoPanel.Controls.Add(placeholder);
        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            if (control.Width > 0 && control.Height > 0)
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, radius, radius, 180, 90);
                path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
                path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
                path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
                path.CloseFigure();
                control.Region = new Region(path);
            }
        }

        private void ApplyLightBlueBorder(Control control, int borderWidth)
        {
            control.Paint += (s, e) =>
            {
                using (var pen = new Pen(lightBlueColor, borderWidth))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, control.Width - 1, control.Height - 1);
                }
            };
        }

        public void LoadData()
        {
            // Los datos se cargan directamente en los labels durante la creación de los paneles
        }

        private void ViewCollectRegisterConfirm_Load(object sender, EventArgs e)
        {
            // Código de carga adicional si es necesario
        }

        private void label13_Click(object sender, EventArgs e)
        {
            // Evento placeholder
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                AppServices.CollectServices.save.execute(
                    null,
                    collectorRegister.workerCode,
                    collectRegister.collectDate,
                    collectRegister.collectedKilos,
                    harvestRegister.id,
                    1,
                    null,
                    collectRegister.plotId,
                    1);

                MessageBox.Show(
                    "Recolección registrada exitosamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.Owner.Close();

                if (viewCollect is ViewCollect parent)
                {
                    parent.Show();
                    parent.loadLastDataGridView();
                }
                this.Close();
            }
            catch (InvalidOperationException ex)
            {
                // Capturar errores de negocio específicos del repositorio
                string errorMessage = ex.Message;

                // Personalizar mensaje según el tipo de error
                if (errorMessage.Contains("ya ha registrado una recolecta") ||
                    errorMessage.Contains("Error 20072"))
                {
                    MessageBox.Show(
                        $"El recolector '{collectorRegister.workerCode}' ya tiene una recolección registrada " +
                        $"para la cosecha #{harvestRegister.id} en el lote #{collectRegister.plotId}.\n\n" +
                        $"Por favor, verifique los datos " + "\n\n" + "Un recolector solo puede registrar una recolecta por día para cada cosecha",
                        "Registro Duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else if (errorMessage.Contains("Ya existe un registro ZERO"))
                {
                    MessageBox.Show(
                        "Ya existe un registro ZERO para esta combinación de recolector y cosecha.",
                        "Registro Duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(
                        $"Error al registrar la recolección:\n\n{errorMessage}",
                        "Error de Validación",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Capturar cualquier otro error no esperado
                MessageBox.Show(
                    $"Error inesperado al registrar la recolección:\n\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}