using CAFEPAY.Views.ViewOrigin;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CAFEPAY.Views.ViewPayment
{
    public partial class ViewMenuPayment : Form
    {
        // Colores exactos del FIGMA
        private Color redColor = Color.FromArgb(183, 32, 46);     // #B7202E
        private Color darkBlueColor = Color.FromArgb(13, 43, 97); // #0D2B61
        private Color whiteColor = Color.White;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64);
        private Color lightGrayColor = Color.FromArgb(240, 240, 240);

        // Botones del menú
        private Button btnPayment = new Button();
        private Button btnManagePayments = new Button();
        private Button btnBack = new Button();

        public ViewMenuPayment()
        {
            ApplyExactFigmaDesign();
            this.WindowState = FormWindowState.Maximized;

            // Conectar eventos
            btnPayment.Click += btnPayment_Click;
            btnManagePayments.Click += btnManagePayments_Click;
            btnBack.Click += btnBack_Click;
        }

        private void ApplyExactFigmaDesign()
        {
            // Configuración principal
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(0);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(1200, 800);
            this.Text = "CAFICAUCA - Menú de Pagos";

            // 🔝 ENCABEZADO SUPERIOR
            var topHeaderPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 90,
                BackColor = whiteColor,
                Padding = new Padding(20, 10, 40, 10)
            };

            // Logo
            var logoPanel = CreateLogoPanel();
            topHeaderPanel.Controls.Add(logoPanel);

            // 🏷️ TÍTULO PRINCIPAL
            var titleContainerPanel = CreateTitlePanel();

            // 🎯 PANEL CENTRAL CON TARJETAS
            var menuContainerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = whiteColor,
                Padding = new Padding(0, 60, 0, 60) // Sin padding lateral aquí
            };

            // 🔥 PANEL CONTENEDOR CENTRADO PARA LAS TARJETAS
            var cardsContainer = new Panel
            {
                Size = new Size(900, 400), // Más ancho para centrar mejor
                Location = new Point((this.Width - 900) / 2, 50),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.None
            };

            // 🔥 CALCULAR POSICIONES CENTRADAS
            int cardWidth = 380; // Un poco más ancho
            int cardHeight = 200;
            int spacing = 60; // Más espacio entre tarjetas

            // Posicionar primera tarjeta (centrada considerando el espacio)
            int card1X = (cardsContainer.Width - (cardWidth * 2 + spacing)) / 2;
            int card2X = card1X + cardWidth + spacing;

            // Tarjeta 1: Pagar (más a la derecha)
            var cardPagar = CreateMenuCard("Pagar", "Calcular pagos disponibles para cada recolector");
            cardPagar.Size = new Size(cardWidth, cardHeight);
            cardPagar.Location = new Point(card1X, 0);
            cardPagar.Click += (s, e) => btnPayment.PerformClick();

            // Tarjeta 2: Gestionar Pagos (más a la izquierda)
            var cardGestionar = CreateMenuCard("Gestionar Pagos", "Consultar o eliminar pagos registrados");
            cardGestionar.Size = new Size(cardWidth, cardHeight);
            cardGestionar.Location = new Point(card2X, 0);
            cardGestionar.Click += (s, e) => btnManagePayments.PerformClick();

            cardsContainer.Controls.Add(cardPagar);
            cardsContainer.Controls.Add(cardGestionar);
            menuContainerPanel.Controls.Add(cardsContainer);

            // 🔙 BOTÓN REGRESAR
            var bottomPanel = CreateBottomPanel();

            // 📋 BREADCRUMB
            var breadcrumbPanel = CreateBreadcrumbPanel();

            // 🔄 AGREGAR CONTROLES
            this.Controls.Add(menuContainerPanel);
            this.Controls.Add(titleContainerPanel);
            this.Controls.Add(topHeaderPanel);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(breadcrumbPanel);

            // Redimensionamiento
            this.Resize += (s, e) => {
                var titlePanel = titleContainerPanel.Controls[0] as Panel;
                if (titlePanel != null)
                    titlePanel.Location = new Point((titleContainerPanel.Width - titlePanel.Width) / 2, 0);

                // Centrar cardsContainer
                cardsContainer.Location = new Point((this.Width - cardsContainer.Width) / 2, 50);

                // Centrar botón Regresar
                btnBack.Location = new Point(bottomPanel.Width / 2 - 90, 25);
            };
        }

        private Panel CreateMenuCard(string title, string description)
        {
            // Crear panel personalizado SIN iconos
            var card = new MenuCardControl(title, description)
            {
                BackColor = whiteColor,
                Cursor = Cursors.Hand
            };

            return card;
        }

        // 🔥 CONTROL PERSONALIZADO CON POSICIONES AJUSTADAS
        private class MenuCardControl : Panel
        {
            private string title;
            private string description;
            private bool isHovered = false;
            private bool isPressed = false;

            // Colores
            private Color redColor = Color.FromArgb(183, 32, 46);
            private Color darkBlueColor = Color.FromArgb(13, 43, 97);
            private Color whiteColor = Color.White;
            private Color darkGrayColor = Color.FromArgb(64, 64, 64);
            private Color lightGrayColor = Color.FromArgb(240, 240, 240);
            private Color hoverBgColor = Color.FromArgb(240, 245, 255);
            private Color pressedBgColor = Color.FromArgb(220, 235, 250);

            public MenuCardControl(string title, string description)
            {
                this.title = title;
                this.description = description;

                this.BorderStyle = BorderStyle.FixedSingle;
                this.Padding = new Padding(20);

                // Configurar eventos
                this.MouseEnter += (s, e) => {
                    isHovered = true;
                    this.Invalidate();
                };

                this.MouseLeave += (s, e) => {
                    isHovered = false;
                    isPressed = false;
                    this.Invalidate();
                };

                this.MouseDown += (s, e) => {
                    isPressed = true;
                    this.Invalidate();
                };

                this.MouseUp += (s, e) => {
                    isPressed = false;
                    this.Invalidate();
                };

                this.Paint += MenuCardControl_Paint;
            }

            private void MenuCardControl_Paint(object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                // Determinar colores según estado
                Color backgroundColor = isPressed ? pressedBgColor :
                                      isHovered ? hoverBgColor : whiteColor;

                Color titleColor = (isHovered || isPressed) ? redColor : darkBlueColor;
                Color borderColor = (isHovered || isPressed) ? darkBlueColor : lightGrayColor;

                // Establecer fondo
                this.BackColor = backgroundColor;

                // Dibujar borde
                using (Pen borderPen = new Pen(borderColor, 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
                }

                // 🔥 TÍTULO MÁS ARRIBA (35px desde arriba)
                using (Font titleFont = new Font("Segoe UI", 22, FontStyle.Bold)) // 22px para mejor visibilidad
                using (Brush titleBrush = new SolidBrush(titleColor))
                {
                    SizeF titleSize = g.MeasureString(title, titleFont);
                    float titleX = (this.Width - titleSize.Width) / 2; // Centrado horizontal
                    float titleY = 25; // 🔥 SUBIDO 15px (antes estaba en 50)

                    g.DrawString(title, titleFont, titleBrush, titleX, titleY);
                }

                // 🔥 LÍNEA ROJA MÁS ABAJO (70px desde arriba)
                using (Pen linePen = new Pen(redColor, 3))
                {
                    float lineY = 70; // 🔥 BAJADA 15px (antes estaba en 85)
                    float linePadding = 50; // Más padding para líneas más cortas y elegantes
                    g.DrawLine(linePen, linePadding, lineY, this.Width - linePadding, lineY);
                }

                // 🔥 DESCRIPCIÓN MÁS ABAJO (90px desde arriba)
                using (Font descFont = new Font("Segoe UI", 11, FontStyle.Regular))
                using (Brush descBrush = new SolidBrush(darkGrayColor))
                {
                    RectangleF descRect = new RectangleF(30, 90, this.Width - 60, 70); // 🔥 90px en lugar de 100
                    StringFormat format = new StringFormat();
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    format.Trimming = StringTrimming.Word;

                    g.DrawString(description, descFont, descBrush, descRect, format);
                }

                // 🔥 INDICADOR DE ACCIÓN (más sutil)
                using (Font actionFont = new Font("Segoe UI", 8, FontStyle.Italic))
                using (Brush actionBrush = new SolidBrush(Color.FromArgb(120, 120, 120)))
                {
                    string actionText = "Click para continuar →";
                    SizeF actionSize = g.MeasureString(actionText, actionFont);
                    float actionX = this.Width - actionSize.Width - 15; // 15px del borde derecho
                    float actionY = this.Height - actionSize.Height - 10; // 10px del borde inferior
                    g.DrawString(actionText, actionFont, actionBrush, actionX, actionY);
                }
            }
        }

        // 🔥 MÉTODOS AUXILIARES
        private Panel CreateLogoPanel()
        {
            var logoPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 350,
                BackColor = Color.Transparent,
                Height = 70,
                Padding = new Padding(10, 0, 0, 0)
            };

            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "LOGO-CAFICAUCA.png");
                if (File.Exists(imagePath))
                {
                    var logoPicture = new PictureBox
                    {
                        Image = Image.FromFile(imagePath),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(320, 70),
                        Location = new Point(5, 5),
                        Cursor = Cursors.Hand
                    };
                    logoPanel.Controls.Add(logoPicture);
                }
                else
                {
                    CreateSimulatedLogo(logoPanel);
                }
            }
            catch
            {
                CreateSimulatedLogo(logoPanel);
            }

            return logoPanel;
        }

        private Panel CreateTitlePanel()
        {
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
                BackColor = darkBlueColor
            };

            var whiteInnerPanel = new Panel
            {
                Size = new Size(590, 60),
                Location = new Point(5, 5),
                BackColor = whiteColor
            };

            var mainTitleLabel = new Label
            {
                Text = "MENÚ DE PAGOS",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.Black,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            whiteInnerPanel.Controls.Add(mainTitleLabel);
            blueOuterPanel.Controls.Add(whiteInnerPanel);
            titleContainerPanel.Controls.Add(blueOuterPanel);

            return titleContainerPanel;
        }

        private Panel CreateBottomPanel()
        {
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 100,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 20, 40, 20)
            };

            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.BackColor = darkGrayColor;
            btnBack.ForeColor = whiteColor;
            btnBack.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnBack.Text = "← Regresar";
            btnBack.Size = new Size(180, 50);
            btnBack.Location = new Point(bottomPanel.Width / 2 - 90, 25);
            btnBack.Anchor = AnchorStyles.None;
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            ApplyRoundedCorners(btnBack, 10);

            bottomPanel.Controls.Add(btnBack);
            return bottomPanel;
        }

        private Panel CreateBreadcrumbPanel()
        {
            var breadcrumbPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = lightGrayColor,
                Padding = new Padding(40, 10, 40, 10)
            };

            var breadcrumbLabel = new Label
            {
                Text = "inicio / pagos",
                Font = new Font("Segoe UI", 9, FontStyle.Regular),
                ForeColor = darkGrayColor,
                Dock = DockStyle.Left,
                AutoSize = true
            };

            breadcrumbPanel.Controls.Add(breadcrumbLabel);
            return breadcrumbPanel;
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
            var path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        // MÉTODOS DE EVENTOS
        private void btnPayment_Click(object sender, EventArgs e)
        {
            ViewPayment viewPayment = new ViewPayment(this);
            viewPayment.Owner = this;
            viewPayment.Show();
            this.Hide();
        }

        private void btnManagePayments_Click(object sender, EventArgs e)
        {
            ViewPaymentConsultDelete viewPaymentConsult = new ViewPaymentConsultDelete();
            viewPaymentConsult.Owner = this.Owner;
            this.Hide();
            viewPaymentConsult.Show();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
            else
            {
                var viewMain = new ViewOrigin.ViewMain();
                viewMain.Show();
            }
            this.Close();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                this.WindowState = FormWindowState.Maximized;

                // 🔥 SOLUCIÓN: Recrear la interfaz cuando se hace visible
                if (this.Controls.Count > 0)
                {
                    this.SuspendLayout();

                    // Guardar estado actual
                    FormWindowState savedState = this.WindowState;

                    // Forzar redibujado de todos los controles
                    this.Invalidate(true);

                    // Recorrer y refrescar todos los controles
                    RefreshAllControls(this);

                    this.ResumeLayout(true);
                    this.PerformLayout();
                    this.Refresh();

                    this.WindowState = savedState;
                }
            }
        }

        private void RefreshAllControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.Invalidate();
                control.Update();
                control.Refresh();

                if (control.HasChildren)
                {
                    RefreshAllControls(control);
                }
            }
        }
    }
}