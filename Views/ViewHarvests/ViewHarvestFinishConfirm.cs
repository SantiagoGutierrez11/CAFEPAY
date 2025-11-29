using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Harvests.Infrastructure;
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

namespace CAFEPAY.Views.ViewHarvest
{
    public partial class ViewHarvestFinishConfirm : Form
    {
        private HarvestDTO harvestDTO;
        private PlotDTO plotOfHarvest;

        // Colores exactos del diseño
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro del formulario
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Confirmar
        private Color whiteColor = Color.White;
        private Color successGreen = Color.FromArgb(46, 125, 50); // Verde éxito

        // Botones
        private Button btnConfirm = new Button();
        private Button btnDecline = new Button();

        public ViewHarvestFinishConfirm(PlotDTO _plotDTO, HarvestDTO _harvestDTO)
        {
            plotOfHarvest = _plotDTO;
            harvestDTO = _harvestDTO;
            InitializeComponent();
            ApplyVisualDesign();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
        }

        private void ApplyVisualDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(20);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 700);
            this.Text = "Finalización de Cosecha";
            this.WindowState = FormWindowState.Normal;
            this.MaximizeBox = true;

            // 🖼️ LOGO CAFICAUCA (Esquina superior izquierda)
            var logoImage = new PictureBox
            {
                Size = new Size(320, 70),
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            // Cargar logo desde recursos
            try
            {
                string imagePath = Path.Combine(Application.StartupPath, "Resources", "LOGO-CAFICAUCA.png");
                if (File.Exists(imagePath))
                {
                    logoImage.Image = Image.FromFile(imagePath);
                }
                else
                {
                    CreateLogoPlaceholder(logoImage);
                }
            }
            catch (Exception ex)
            {
                CreateLogoPlaceholder(logoImage);
                Console.WriteLine("Error cargando logo: " + ex.Message);
            }
            this.Controls.Add(logoImage);

            // 📦 PANEL PRINCIPAL (AZUL OSCURO CON BORDE AZUL CLARO)
            var mainFormPanel = new Panel
            {
                Size = new Size(500, 730),
                BackColor = darkBlueColor,
                Padding = new Padding(40, 30, 40, 30),
                Location = new Point((this.Width - 500) / 2, 30)
            };
            ApplyRoundedCorners(mainFormPanel, 15);
            ApplyLightBlueBorder(mainFormPanel, 3);

            // ✅ ICONO DE CONFIRMACIÓN
            var confirmIcon = new Label
            {
                Text = "?",
                Font = new Font("Segoe UI", 36, FontStyle.Bold),
                ForeColor = successGreen,
                Dock = DockStyle.Top,
                Height = 60,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 5)
            };
            mainFormPanel.Controls.Add(confirmIcon);

            // 🏷️ TÍTULO PRINCIPAL
            var titleLabel = new Label
            {
                Text = "ESTAS SEGURO",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = whiteColor,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 5)
            };
            mainFormPanel.Controls.Add(titleLabel);

            var subtitleLabel = new Label
            {
                Text = "La cosecha se finalizará con esta información",
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = Color.FromArgb(200, 200, 200),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 30)
            };
            mainFormPanel.Controls.Add(subtitleLabel);

            // 📝 CONTENEDOR DE INFORMACIÓN
            var fieldsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = true
            };

            // Agregar campos al diseño
            AddInfoFieldsToDesign(fieldsContainer);

            mainFormPanel.Controls.Add(fieldsContainer);

            // 🔘 PANEL DE BOTONES
            var buttonsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 15, 0, 0)
            };

            ConfigureButtonsDesign(buttonsPanel);
            mainFormPanel.Controls.Add(buttonsPanel);

            this.Controls.Add(mainFormPanel);

            // 🔄 AJUSTAR AL REDIMENSIONAR
            this.Resize += (s, e) => {
                mainFormPanel.Location = new Point((this.Width - mainFormPanel.Width) / 2, 30);
            };
        }

        private void AddInfoFieldsToDesign(Panel container)
        {
            int currentY = 160;
            int fieldWidth = 420;
            int labelHeight = 25;
            int fieldHeight = 40;
            int verticalSpacing = 35;

            // 🔹 Nombre de lote
            var plotNameLabel = new Label();
            plotNameLabel.Text = plotOfHarvest.name;
            CreateStyledInfoField(container, "Nombre de lote", plotNameLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Numero de cosecha
            var harvestNumberLabel = new Label();
            harvestNumberLabel.Text = harvestDTO.id.ToString();
            CreateStyledInfoField(container, "Cosecha ID", harvestNumberLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Precio por kilo
            var priceLabel = new Label();
            priceLabel.Text = harvestDTO.pricePerKilo.ToString("C2");
            CreateStyledInfoField(container, "Precio por kilo", priceLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Fecha Inicio
            var startDateLabel = new Label();
            startDateLabel.Text = harvestDTO.startDate.ToString("dd/MM/yyyy");
            CreateStyledInfoField(container, "Fecha Inicio", startDateLabel, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 Fecha de cierre
            var endDateLabel = new Label();
            endDateLabel.Text = DateTime.Now.ToString("dd/MM/yyyy");
            CreateStyledInfoField(container, "Fecha de cierre", endDateLabel, currentY, fieldWidth, labelHeight, fieldHeight);
        }

        private void CreateStyledInfoField(Panel container, string labelText, Control control, int y, int width, int labelHeight, int fieldHeight)
        {
            // Label en blanco
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = whiteColor,
                Location = new Point(0, y),
                Size = new Size(width, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            container.Controls.Add(label);

            // Control con estilo (igual que los campos de entrada pero de solo lectura)
            if (control != null)
            {
                control.Location = new Point(0, y + labelHeight + 8);
                control.Size = new Size(width, fieldHeight);
                control.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                control.BackColor = Color.FromArgb(240, 240, 240); // Fondo gris claro para solo lectura
                control.ForeColor = Color.FromArgb(80, 80, 80);    // Texto gris oscuro

                if (control is System.Windows.Forms.Label lbl)
                {
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    lbl.BorderStyle = BorderStyle.FixedSingle;
                    lbl.Padding = new Padding(12, 8, 12, 8);
                }

                ApplyRoundedCorners(control, 8);
                container.Controls.Add(control);
            }
        }

        private void ConfigureButtonsDesign(Panel buttonPanel)
        {
            // Botón Confirmar (Verde)
            btnConfirm.Text = "CONFIRMAR";
            btnConfirm.BackColor = greenColor;
            btnConfirm.ForeColor = whiteColor;
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.FlatAppearance.BorderSize = 0;
            btnConfirm.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnConfirm.Size = new Size(120, 40);
            btnConfirm.Cursor = Cursors.Hand;
            btnConfirm.Click += btnConfirm_Click;
            ApplyRoundedCorners(btnConfirm, 8);

            // Botón Cancelar (Rojo)
            btnDecline.Text = "RECHAZAR";
            btnDecline.BackColor = Color.FromArgb(183, 32, 46); // Rojo vino
            btnDecline.ForeColor = whiteColor;
            btnDecline.FlatStyle = FlatStyle.Flat;
            btnDecline.FlatAppearance.BorderSize = 0;
            btnDecline.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnDecline.Size = new Size(120, 40);
            btnDecline.Cursor = Cursors.Hand;
            btnDecline.Click += btnDecline_Click;
            ApplyRoundedCorners(btnDecline, 8);

            int panelWidth = 420;
            int totalButtonsWidth = btnConfirm.Width + 30 + btnDecline.Width;
            int startX = (panelWidth - totalButtonsWidth) / 2;

            btnConfirm.Location = new Point(startX, 15);
            btnDecline.Location = new Point(startX + btnConfirm.Width + 30, 15);

            buttonPanel.Controls.Add(btnConfirm);
            buttonPanel.Controls.Add(btnDecline);
        }

        private void CreateLogoPlaceholder(PictureBox pictureBox)
        {
            var placeholder = new Bitmap(320, 70);
            using (var g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.LightGray);

                using (var font = new Font("Segoe UI", 10, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    var text = "LOGO CAFICAUCA";
                    var textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, brush,
                        (320 - textSize.Width) / 2,
                        (70 - textSize.Height) / 2);
                }
            }
            pictureBox.Image = placeholder;
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

        private void ViewHarvestFinishConfirm_Load(object sender, EventArgs e)
        {

        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            if (this.Owner is ViewHarvest parent)
            {
                parent.loadHarvests();
                this.Owner?.Show();
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                AppServices.HarvestServices.update.execute(harvestDTO.id, harvestDTO.idPlot, harvestDTO.startDate, DateTime.Today, harvestDTO.pricePerKilo, 2);
                MessageBox.Show($"Se ha finalizado la cosecha correctamente", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (this.Owner is ViewHarvest parent)
                {
                    parent.loadHarvests();
                    this.Owner.Show();
                    this.Close();
                }
            }
            catch (HarvestHasPendingCollectsException ex)
            {
                MessageBox.Show(
                    "No se puede finalizar la cosecha porque tiene recolecciones pendientes de pago.\n\n" +
                    "Por favor, complete o elimine todas las recolecciones antes de finalizar.",
                    "No se puede finalizar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (InvalidHarvestDurationException ex)
            {
                MessageBox.Show(
                    "La fecha de finalización no es válida.\n\n" +
                    "Debe ser posterior a la fecha de inicio de la cosecha.",
                    "Fecha inválida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            catch (HarvestNotFoundException ex)
            {
                MessageBox.Show(
                    "No se encontró la cosecha seleccionada.\n\n" +
                    "Es posible que ya haya sido eliminada.",
                    "Cosecha no encontrada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (HarvestOperationException ex)
            {
                // Remover el código de error ORA-XXXXX del mensaje
                string mensaje = ex.Message;
                if (mensaje.Contains("ORA-"))
                {
                    int index = mensaje.IndexOf("ORA-");
                    int endIndex = mensaje.IndexOf(':', index);
                    if (endIndex > index)
                    {
                        mensaje = mensaje.Substring(endIndex + 1).Trim();
                    }
                }

                MessageBox.Show(
                    $"Error al finalizar la cosecha:\n\n{mensaje}",
                    "Error de operación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error inesperado al finalizar la cosecha.\n\n" +
                    "Por favor, intente nuevamente o contacte al administrador.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}