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

namespace CAFEPAY.Views.ViewMain
{
    public partial class ViewMain : Form
    {
        // Colores del diseño
        private Color redColor = Color.FromArgb(164, 36, 52);     // #A42434 - Rojo vino
        private Color darkBlueColor = Color.FromArgb(13, 43, 97); // #0D2B61 - Azul oscuro
        private Color whiteColor = Color.White;
        private Color blackColor = Color.Black;

        public ViewMain()
        {
            InitializeComponent();
            ApplyMainDesign();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
            this.Text = "CAFEPAY - Sistema Gestor de Pagos de Nómina";
        }

        private void ApplyMainDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 800);

            // 🔝 ENCABEZADO SUPERIOR - Logo CAFICAUCA MÁS GRANDE
            var topHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150, // ← MÁS ALTO para logo más grande
                BackColor = whiteColor,
                Padding = new Padding(20, 20, 40, 20)
            };

            // 🖼️ LOGO CAFICAUCA MÁS GRANDE (Esquina superior izquierda)
            var logoPanel = new Panel
            {
                Size = new Size(500, 110), // ← MÁS GRANDE: 500x110
                Location = new Point(20, 20),
                BackColor = Color.Transparent
            };

            // Cargar logo desde Resources - MÁS GRANDE
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "LOGO-CAFICAUCA.png");
                if (File.Exists(imagePath))
                {
                    var logoPicture = new PictureBox
                    {
                        Size = new Size(480, 110), // ← MÁS GRANDE: 480x110
                        Location = new Point(0, 0),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Image = Image.FromFile(imagePath),
                        Cursor = Cursors.Hand
                    };
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

            topHeaderPanel.Controls.Add(logoPanel);

            // 🏷️ TÍTULO DEL SISTEMA (Parte superior central-derecha)
            var titlePanel = new Panel
            {
                Size = new Size(500, 100),
                Location = new Point(topHeaderPanel.Width - 550, 25), // Ajustado por logo más grande
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Transparent
            };

            // Título CAFEPAY (rojo con efecto sombra)
            var cafepayLabel = new Label
            {
                Text = "CAFEPAY",
                Font = new Font("Segoe UI", 32, FontStyle.Bold),
                ForeColor = redColor,
                Location = new Point(0, 0),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Aplicar efecto de sombra al texto
            ApplyTextShadow(cafepayLabel);

            // Subtítulo
            var subtitleLabel = new Label
            {
                Text = "Sistema Gestor de Pagos de Nómina",
                Font = new Font("Segoe UI", 14, FontStyle.Regular),
                ForeColor = blackColor,
                Location = new Point(5, 55),
                AutoSize = true,
                BackColor = Color.Transparent
            };

            // Aplicar contorno azul sutil al subtítulo
            ApplyBlueOutline(subtitleLabel);

            titlePanel.Controls.Add(cafepayLabel);
            titlePanel.Controls.Add(subtitleLabel);
            topHeaderPanel.Controls.Add(titlePanel);

            // 📦 PANEL CENTRAL - Botones principales
            var centerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = whiteColor,
                Padding = new Padding(100, 50, 100, 100)
            };

            // Contenedor de botones
            var buttonsContainer = new Panel
            {
                Size = new Size(1000, 300),
                Location = new Point((centerPanel.Width - 1000) / 2, (centerPanel.Height - 300) / 2),
                BackColor = Color.Transparent
            };

            // Crear los 4 botones principales (CON POSICIONES INTERCAMBIADAS)
            CreateMainButtons(buttonsContainer);

            centerPanel.Controls.Add(buttonsContainer);

            // 📍 PIE DE PÁGINA
            var footerPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50, 
                BackColor = whiteColor,
                Padding = new Padding(40, 15, 40, 15) 
            };

            var breadcrumbLabel = new Label
            {
                Text = "Inicio",
                Font = new Font("Segoe UI", 14, FontStyle.Bold), // ← TEXTO MÁS GRANDE Y EN NEGRITA
                ForeColor = blackColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };

            footerPanel.Controls.Add(breadcrumbLabel);

            // 🔄 AGREGAR TODOS LOS CONTROLES AL FORMULARIO
            this.Controls.Add(centerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(footerPanel);

            // Ajustar redimensionamiento
            this.Resize += (s, e) => {
                titlePanel.Location = new Point(topHeaderPanel.Width - 550, 25);
                buttonsContainer.Location = new Point(
                    (centerPanel.Width - buttonsContainer.Width) / 2,
                    (centerPanel.Height - buttonsContainer.Height) / 2
                );
            };
        }

        private void CreateLogoPlaceholder(Panel container)
        {
            var placeholder = new Panel
            {
                Size = new Size(480, 110), // ← MÁS GRANDE
                Location = new Point(0, 0),
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
            };

            var logoText = new Label
            {
                Text = "LOGO CAFICAUCA\n(TAMAÑO GRANDE)",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = darkBlueColor,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            placeholder.Controls.Add(logoText);
            container.Controls.Add(placeholder);
        }

        private void CreateMainButtons(Panel container)
        {
            int buttonWidth = 200;
            int spacing = 40;
            int totalWidth = (buttonWidth * 4) + (spacing * 3);
            int startX = (container.Width - totalWidth) / 2;

            // ← POSICIONES INTERCAMBIADAS: Ahora RECOLECTORES va primero

            // Botón 1: RECOLECTORES (AHORA PRIMERO)
            var btnRecolectores = CreateMainButton("RECOLECTORES", CreateCosechasIcon(), startX, 0);
            btnRecolectores.Click += (s, e) => OpenRecolectoresModule();
            container.Controls.Add(btnRecolectores);

            // Botón 2: COSECHAS (AHORA SEGUNDO)
            var btnCosechas = CreateMainButton("COSECHAS", CreateRecolectoresIcon(), startX + buttonWidth + spacing, 0);
            btnCosechas.Click += (s, e) => OpenCosechasModule();
            container.Controls.Add(btnCosechas);

            // Botón 3: COLECTA
            var btnColecta = CreateMainButton("COLECTA", CreateColectaIcon(), startX + (buttonWidth + spacing) * 2, 0);
            btnColecta.Click += (s, e) => OpenColectaModule();
            container.Controls.Add(btnColecta);

            // Botón 4: PAGOS
            var btnPagos = CreateMainButton("PAGOS", CreatePagosIcon(), startX + (buttonWidth + spacing) * 3, 0);
            btnPagos.Click += (s, e) => OpenPagosModule();
            container.Controls.Add(btnPagos);
        }

        private Button CreateMainButton(string text, Image icon, int x, int y)
        {
            var button = new Button
            {
                Size = new Size(200, 180),
                Location = new Point(x, y),
                BackColor = redColor,
                ForeColor = whiteColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Text = text,
                TextImageRelation = TextImageRelation.ImageAboveText,
                Image = icon,
                Padding = new Padding(0, 20, 0, 10),
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(140, 30, 45);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(120, 25, 38);

            // Aplicar esquinas redondeadas
            ApplyRoundedCorners(button, 12);

            // Aplicar sombra sutil
            ApplyButtonShadow(button);

            return button;
        }

        private Image CreateRecolectoresIcon()
        {
            var bitmap = new Bitmap(80, 80);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (var pen = new Pen(whiteColor, 3))
                {
                    // Primera figura (izquierda)
                    g.DrawEllipse(pen, 20, 20, 12, 12); // Cabeza
                    g.DrawLine(pen, 26, 32, 26, 50);    // Cuerpo

                    // Segunda figura (derecha)
                    g.DrawEllipse(pen, 48, 20, 12, 12); // Cabeza
                    g.DrawLine(pen, 54, 32, 54, 50);    // Cuerpo

                    // Hombros (líneas horizontales)
                    g.DrawLine(pen, 20, 35, 32, 35);
                    g.DrawLine(pen, 48, 35, 60, 35);
                }
            }
            return bitmap;
        }

        private Image CreateCosechasIcon()
        {
            var bitmap = new Bitmap(80, 80);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                // Dibujar campesino con sombrero y planta de café
                using (var pen = new Pen(whiteColor, 3))
                {
                    // Cabeza
                    g.DrawEllipse(pen, 35, 10, 10, 10);

                    // Cuerpo
                    g.DrawLine(pen, 40, 20, 40, 35);

                    // Brazos
                    g.DrawLine(pen, 40, 25, 30, 30);
                    g.DrawLine(pen, 40, 25, 50, 30);

                    // Piernas
                    g.DrawLine(pen, 40, 35, 35, 45);
                    g.DrawLine(pen, 40, 35, 45, 45);

                    // Sombrero
                    g.DrawEllipse(pen, 32, 8, 16, 6);
                    g.DrawLine(pen, 32, 11, 48, 11);

                    // Planta de café en la mano
                    g.DrawLine(pen, 30, 30, 25, 20);
                    g.DrawEllipse(pen, 20, 15, 10, 10); // Granos de café
                }
            }
            return bitmap;
        }

        private Image CreateColectaIcon()
        {
            var bitmap = new Bitmap(80, 80);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (var pen = new Pen(whiteColor, 3))
                {
                    // Tres figuras humanas pequeñas
                    for (int i = 0; i < 3; i++)
                    {
                        int x = 20 + (i * 20);
                        g.DrawEllipse(pen, x, 25, 8, 8);   // Cabeza
                        g.DrawLine(pen, x + 4, 33, x + 4, 45); // Cuerpo
                    }
                }
            }
            return bitmap;
        }

        private Image CreatePagosIcon()
        {
            var bitmap = new Bitmap(80, 80);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);

                using (var pen = new Pen(whiteColor, 3))
                using (var font = new Font("Segoe UI", 24, FontStyle.Bold))
                {
                    // Símbolo de dólar
                    var text = "$";
                    var textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, Brushes.White,
                        (80 - textSize.Width) / 2,
                        (80 - textSize.Height) / 2);
                }
            }
            return bitmap;
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

        private void ApplyButtonShadow(Control control)
        {
            control.Paint += (s, e) =>
            {
                // Dibujar sombra sutil alrededor del botón
                using (var pen = new Pen(Color.FromArgb(30, 0, 0, 0), 2))
                {
                    e.Graphics.DrawRectangle(pen, 1, 1, control.Width - 3, control.Height - 3);
                }
            };
        }

        private void ApplyTextShadow(Control control)
        {
            control.Paint += (s, e) =>
            {
                var label = (Label)control;
                // Dibujar texto con sombra
                using (var shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                {
                    e.Graphics.DrawString(label.Text, label.Font, shadowBrush,
                        label.ClientRectangle.X + 1, label.ClientRectangle.Y + 1);
                }
                // Texto principal
                using (var mainBrush = new SolidBrush(label.ForeColor))
                {
                    e.Graphics.DrawString(label.Text, label.Font, mainBrush, label.ClientRectangle);
                }
            };
        }

        private void ApplyBlueOutline(Control control)
        {
            control.Paint += (s, e) =>
            {
                var label = (Label)control;
                // Contorno azul sutil
                using (var outlineBrush = new SolidBrush(Color.FromArgb(30, darkBlueColor.R, darkBlueColor.G, darkBlueColor.B)))
                using (var format = new StringFormat())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    var rect = label.ClientRectangle;
                    // Dibujar contorno en varias posiciones para efecto de outline
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;
                            var shadowRect = new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);
                            e.Graphics.DrawString(label.Text, label.Font, outlineBrush, shadowRect, format);
                        }
                    }
                    // Texto principal
                    e.Graphics.DrawString(label.Text, label.Font, Brushes.Black, rect, format);
                }
            };
        }

        // Métodos para abrir los módulos
        private void OpenRecolectoresModule()
        {
            // Aquí abrirías tu ViewCollector existente
            var viewCollector = new ViewCollector.ViewCollector();
            viewCollector.Show();
            this.Hide();
        }

        private void OpenCosechasModule()
        {
            var viewHarvest = new ViewHarvest.ViewHarvest(); 
            viewHarvest.Owner = this;
            viewHarvest.Show();
            this.Hide();
        }

        private void OpenColectaModule()
        {
            MessageBox.Show("Módulo COLECTA - En desarrollo", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OpenPagosModule()
        {
            MessageBox.Show("Módulo PAGOS - En desarrollo", "Información",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}