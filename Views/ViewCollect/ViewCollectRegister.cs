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
    public partial class ViewCollectRegister : Form
    {
        private CollectorDTO collectorRegister;
        private HarvestDTO harvestRegister;

        // Colores exactos del diseño CAFICAUCA
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Registrar
        private Color redColor = Color.FromArgb(183, 32, 46);      // #B7202E - Rojo del botón Cancelar
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);  // Gris oscuro para el botón home

        // Variables para validación
        private bool allowDecimal = true; // Permitir decimales
        private char decimalSeparator = '.'; // Separador decimal (puede ser '.' o ',')

        // Botón home
        private Button homeButton;

        public ViewCollectRegister(HarvestDTO _harvestRegister, CollectorDTO _collectorRegister)
        {
            InitializeComponent();
            this.harvestRegister = _harvestRegister;
            this.collectorRegister = _collectorRegister;
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
            this.Text = "CAFICAUCA - Agregar Recolección";

            // 🔝 ENCABEZADO SUPERIOR - Logo CAFICAUCA
            var topHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70, // Reducido para dar más espacio al título
                BackColor = whiteColor,
                Padding = new Padding(20, 10, 40, 10)
            };

            // Panel del logo
            var logoPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 300,
                BackColor = Color.Transparent,
                Height = 50 // Reducido
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
                    logoPicture.Size = new Size(280, 50); // Reducido
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

            // 🏷️ TÍTULO PRINCIPAL - MÁS ARRIBA
            var titleContainerPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60, // Más compacto
                BackColor = whiteColor,
                Padding = new Padding(0, 5, 0, 0) // Menos padding
            };

            // Título principal centrado
            var mainTitleLabel = new Label
            {
                Text = "AGREGAR RECOLECCIÓN",
                Font = new Font("Segoe UI", 26, FontStyle.Bold), // Un poco más pequeño
                ForeColor = darkBlueColor,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 45
            };
            titleContainerPanel.Controls.Add(mainTitleLabel);

            // Subtítulo
            var subtitleLabel = new Label
            {
                Text = "Registrar nueva recolección de café",
                Font = new Font("Segoe UI", 12, FontStyle.Regular), // Más pequeño
                ForeColor = Color.FromArgb(100, 100, 100),
                Dock = DockStyle.Bottom,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter
            };
            titleContainerPanel.Controls.Add(subtitleLabel);

            // 📦 PANEL PRINCIPAL - MÁS ARRIBA
            var mainFormPanel = new Panel
            {
                Size = new Size(1100, 600),
                BackColor = darkBlueColor,
                Padding = new Padding(40, 25, 40, 30),
                Location = new Point((this.Width - 1100) / 2, 90) // Posicionado mucho más arriba
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
                Height = 80, // Reducido
                BackColor = Color.Transparent,
                Padding = new Padding(0, 15, 0, 0) // Menos padding
            };

            ConfigureButtonsDesign(buttonsPanel);
            mainFormPanel.Controls.Add(buttonsPanel);

            // 📍 BREADCRUMB
            var breadcrumbPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 35, // Reducido
                BackColor = Color.FromArgb(240, 240, 240),
                Padding = new Padding(40, 8, 40, 8)
            };

            var breadcrumbLabel = new Label
            {
                Text = "inicio / recolectas / agregar recolección",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };
            breadcrumbPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES
            this.Controls.Add(mainFormPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(breadcrumbPanel);

            // Ajustar redimensionamiento
            this.Resize += (s, e) => {
                mainFormPanel.Location = new Point((this.Width - mainFormPanel.Width) / 2, 70); // Ajustado para posición más alta
                homeButton.Location = new Point(topHeaderPanel.Width - 50, 15);
            };
        }

        private void CreateInformationPanelsVertical(Panel container)
        {
            int panelWidth = 1000;
            int panelHeight = 135; // Altura reducida para paneles más compactos
            int spacing = 15; // Menos espacio entre paneles

            // 🟦 PANEL 1: INFORMACIÓN COSECHA
            var harvestPanel = CreateInfoPanel("Información Cosecha", 0, panelWidth, panelHeight);
            AddHarvestInfoToPanel(harvestPanel);
            container.Controls.Add(harvestPanel);

            // 🟦 PANEL 2: INFORMACIÓN RECOLECTOR
            var collectorPanel = CreateInfoPanel("Información Recolector", panelHeight + spacing, panelWidth, panelHeight);
            AddCollectorInfoToPanel(collectorPanel);
            container.Controls.Add(collectorPanel);

            // 🟦 PANEL 3: REGISTRAR RECOLECTA
            var collectPanel = CreateInfoPanel("Registrar Recolecta", (panelHeight + spacing) * 2, panelWidth, panelHeight);
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
                Padding = new Padding(25, 15, 25, 12) // Menos padding
            };
            ApplyRoundedCorners(panel, 10);
            ApplyLightBlueBorder(panel, 2);

            // Título del panel en negrita
            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 13, FontStyle.Bold), // Negrita
                ForeColor = whiteColor,
                Dock = DockStyle.Top,
                Height = 22, // Reducido
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = new Padding(0, 0, 0, 15) // Menos margen
            };
            panel.Controls.Add(titleLabel);

            return panel;
        }

        private void AddHarvestInfoToPanel(Panel panel)
        {
            int currentY = 45; // Posición más baja para dejar espacio al título
            int labelWidth = 180;
            int valueWidth = 200;

            // Tres columnas para los tres datos
            int column1X = 30;
            int column2X = 300; // Separación entre columnas
            int column3X = 600;

            // Estos tres usan CreateInfoField que ahora tiene negrita
            CreateInfoField(panel, "Nombre del Lote:", harvestRegister.plotName, column1X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Cosecha ID:", harvestRegister.id.ToString(), column2X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Lote ID:", harvestRegister.idPlot.ToString(), column3X, currentY, labelWidth, valueWidth);
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

            // Estos tres también usan CreateInfoField que ahora tiene negrita
            CreateInfoField(panel, "Código de trabajador:", collectorRegister.workerCode, column1X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Nombre completo:", $"{collectorRegister.firstName} {collectorRegister.lastName}", column2X, currentY, labelWidth, valueWidth);
            CreateInfoField(panel, "Número de cédula:", collectorRegister.id.ToString(), column3X, currentY, labelWidth, valueWidth);
        
        }

        private void AddCollectInfoToPanel(Panel panel)
        {
            int currentY = 45;
            int labelWidth = 180;
            int valueWidth = 200;

            // Dos columnas para los dos datos
            int column1X = 30;
            int column2X = 300;

            // COLUMNA 1: Fecha de recolección
            CreateInfoField(panel, "Fecha de recolección:", DateTime.Today.ToString("dd/MM/yyyy"), column1X, currentY, labelWidth, valueWidth);

            // COLUMNA 2: Kilos Recolectados (campo editable)
            var kilosLabel = new Label
            {
                Text = "Kilos Recolectados: *",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = whiteColor,
                Location = new Point(column2X, currentY),
                Size = new Size(labelWidth, 22),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(kilosLabel);

            textBoxKilos.Location = new Point(column2X + labelWidth, currentY - 5); // Ajustado para alineación
            textBoxKilos.Size = new Size(180, 30); // Tamaño ajustado
            textBoxKilos.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            textBoxKilos.BackColor = whiteColor;
            textBoxKilos.ForeColor = Color.FromArgb(60, 60, 60);
            textBoxKilos.Text = "";
            textBoxKilos.TextAlign = HorizontalAlignment.Center;
            textBoxKilos.BorderStyle = BorderStyle.FixedSingle;
            textBoxKilos.Visible = true;

            // 🔒 AGREGAR VALIDACIÓN AL TEXTBOX
            textBoxKilos.KeyPress += TextBoxKilos_KeyPress;
            textBoxKilos.Validating += TextBoxKilos_Validating;

            ApplyRoundedCorners(textBoxKilos, 5);
            panel.Controls.Add(textBoxKilos);

            // Texto de ayuda debajo del campo de kilos
            var helpLabel = new Label
            {
                Text = "Ingrese la cantidad de kilos recolectados",
                Font = new Font("Segoe UI", 9, FontStyle.Italic), // Más pequeño
                ForeColor = Color.FromArgb(180, 180, 180),
                Location = new Point(column2X, currentY + 35),
                Size = new Size(300, 15),
                TextAlign = ContentAlignment.MiddleLeft
            };
            panel.Controls.Add(helpLabel);
        }

        private void CreateInfoField(Panel panel, string labelText, string value, int x, int y, int labelWidth, int valueWidth)
        {
            // Label (en negrita)
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 10, FontStyle.Bold), // Negrita
                ForeColor = Color.FromArgb(220, 220, 220), // Gris más claro
                Location = new Point(x, y),
                Size = new Size(labelWidth, 20), // Más compacto
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent
            };
            panel.Controls.Add(label);

            // Valor
            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 10, FontStyle.Regular), // Regular para valores
                ForeColor = whiteColor,
                Location = new Point(x, y + 20),
                Size = new Size(valueWidth, 22), // Más compacto
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None
            };
            panel.Controls.Add(valueLabel);
        }

        private void ConfigureButtonsDesign(Panel buttonPanel)
        {
            int panelWidth = 1000;
            int buttonWidth = 200; // Un poco más angostos
            int buttonHeight = 45; // Un poco más bajos
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
            btnCancel.Location = new Point(startX, 20); // Menos espacio arriba
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.Visible = true;
            btnCancel.Click += btnCancel_Click;

            // Botón REGISTRAR (Verde)
            btnRegister.Text = "REGISTRAR";
            btnRegister.BackColor = greenColor;
            btnRegister.ForeColor = whiteColor;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnRegister.Size = new Size(buttonWidth, buttonHeight);
            btnRegister.Location = new Point(startX + buttonWidth + spacing, 20);
            btnRegister.Cursor = Cursors.Hand;
            btnRegister.Visible = true;
            btnRegister.Click += btnRegister_Click;

            ApplyRoundedCorners(btnCancel, 8);
            ApplyRoundedCorners(btnRegister, 8);

            buttonPanel.Controls.Add(btnCancel);
            buttonPanel.Controls.Add(btnRegister);
        }

        private void CreateLogoPlaceholder(Panel logoPanel)
        {
            var placeholder = new Panel
            {
                Size = new Size(280, 50), // Reducido
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

        // 🔒 MÉTODO DE VALIDACIÓN PARA EL CAMPO DE KILOS
        private void TextBoxKilos_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir teclas de control (backspace, delete, etc.)
            if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return;
            }

            // Permitir dígitos
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
                return;
            }

            // Permitir separador decimal si está habilitado
            if (allowDecimal && (e.KeyChar == '.' || e.KeyChar == ','))
            {
                // Determinar automáticamente el separador decimal del sistema
                char systemDecimalSeparator = Convert.ToChar(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                decimalSeparator = systemDecimalSeparator;

                // Reemplazar el carácter ingresado por el separador del sistema
                e.KeyChar = decimalSeparator;

                // Verificar que no haya ya un separador decimal en el texto
                if (textBoxKilos.Text.Contains(decimalSeparator))
                {
                    e.Handled = true; // Ya hay un separador, no permitir otro
                    return;
                }

                // Verificar que el separador no sea el primer carácter
                if (textBoxKilos.Text.Length == 0)
                {
                    e.Handled = true; // No permitir separador al inicio
                    return;
                }

                e.Handled = false;
                return;
            }

            // Cualquier otro carácter no permitido
            e.Handled = true;
        }

        // 🔧 MÉTODO ADICIONAL PARA VALIDACIÓN AL PERDER EL FOCO (OPCIONAL)
        private void TextBoxKilos_Validating(object sender, CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKilos.Text))
            {
                // Reemplazar comas por puntos si es necesario para la conversión
                string textToValidate = textBoxKilos.Text.Replace(',', '.');

                if (!decimal.TryParse(textToValidate, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal result))
                {
                    // Si no se puede convertir, mostrar mensaje y limpiar el campo
                    MessageBox.Show("Por favor ingrese un valor numérico válido para los kilos.", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxKilos.Text = "";
                    textBoxKilos.Focus();
                }
                else
                {
                    // Formatear el número con el separador decimal del sistema
                    textBoxKilos.Text = result.ToString(System.Globalization.CultureInfo.CurrentCulture);
                }
            }
        }

        // MÉTODOS ORIGINALES (MANTENER FUNCIONALIDAD)
        public void LoadData()
        {
            // Los datos se cargan directamente en los labels durante la creación
        }

        private void ViewCollectDetail_Load(object sender, EventArgs e) { }

        private void label10_Click(object sender, EventArgs e) { }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Owner?.Show();
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxKilos.Text))
            {
                try
                {
                    CollectDTO collectRegister = new CollectDTO()
                    {
                        collectId = null,
                        collectorWorkerCode = collectorRegister.workerCode,
                        plotId = harvestRegister.idPlot,
                        harvestId = harvestRegister.id,
                        collectDate = DateTime.Now,
                        collectedKilos = decimal.Parse(textBoxKilos.Text),
                        amountToPaid = null,
                        status = 1,
                        isCountable = 1,
                        statusText = "Registrado"
                    };
                    ViewCollectRegisterConfirm viewCollectRegisterConfirm = new ViewCollectRegisterConfirm(collectorRegister, harvestRegister, collectRegister, this.Owner);
                    viewCollectRegisterConfirm.Owner = this;
                    this.Hide();
                    viewCollectRegisterConfirm.Show();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Por favor ingrese un valor numérico válido para los kilos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("El campo 'Kilos Recolectados' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}