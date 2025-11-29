using CAFEPAY.ArqHex.Collectors.domain;
using CAFEPAY.ArqHex.Harvests.domain;
using CAFEPAY.ArqHex.Harvests.Domain;
using CAFEPAY.ArqHex.Share;
using CAFEPAY.ArqHex.Share.DTO;
using CAFEPAY.ArqHex.Share.Serializers;
using CAFEPAY.Views.ViewOrigin;
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
    public partial class ViewHarvestRegisterConfirm : Form
    {
        private readonly HarvestDTO harvestDTO;
        private readonly string plotInfomation;
        ViewHarvest viewHarvest;

        // Colores exactos del diseño
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro del formulario
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Confirmar
        private Color redColor = Color.FromArgb(183, 32, 46);      // #B7202E - Rojo del botón Rechazar
        private Color whiteColor = Color.White;
        private Color blackColor = Color.Black;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64); // Gris oscuro para el botón home

        private Button homeButton;

        public ViewHarvestRegisterConfirm(HarvestDTO _harvestDTO, string _plotInfomation, ViewHarvest _viewHarvest)
        {
            harvestDTO = _harvestDTO;
            this.plotInfomation = _plotInfomation;
            viewHarvest = _viewHarvest;

            InitializeCustomComponents();
            ApplyVisualDesign();
            loadComponets();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeCustomComponents()
        {
            // Labels para mostrar la información - CON ESTILO MEJORADO
            lbIdPlot = new Label
            {
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = blackColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 40,
                BackColor = whiteColor,
                Padding = new Padding(12, 8, 12, 8)
            };

            lbPricePerKilo = new Label
            {
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = blackColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 40,
                BackColor = whiteColor,
                Padding = new Padding(12, 8, 12, 8)
            };

            lbStartDate = new Label
            {
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = blackColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 40,
                BackColor = whiteColor,
                Padding = new Padding(12, 8, 12, 8)
            };

            // Botón Confirmar
            btnConfirm = new Button
            {
                Text = "Confirmar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(120, 40),
                Cursor = Cursors.Hand
            };
            btnConfirm.Click += btnConfirm_Click;

            // Botón Rechazar
            btnDecline = new Button
            {
                Text = "Rechazar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(120, 40),
                Cursor = Cursors.Hand
            };
            btnDecline.Click += btnDecline_Click;
        }

        void loadComponets()
        {
            lbIdPlot.Text = plotInfomation;
            lbStartDate.Text = harvestDTO.startDate.ToShortDateString();
            lbPricePerKilo.Text = harvestDTO.pricePerKilo.ToString("C2");
        }

        private void ApplyVisualDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(20);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 700);
            this.Text = "Confirmar Registro de Cosecha";

            // 🖼️ LOGO CAFICAUCA (Esquina superior izquierda)
            var logoImage = new PictureBox
            {
                Size = new Size(320, 70),
                Location = new Point(5, 5),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

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

            // 🏠 ICONO DE CASA (Home)
            homeButton = new Button
            {
                Size = new Size(40, 40),
                Location = new Point(this.ClientSize.Width - 50, 10),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                FlatStyle = FlatStyle.Flat,
                BackColor = darkGrayColor,
                ForeColor = whiteColor,
                Text = "🏠",
                Font = new Font("Segoe UI", 14),
                Cursor = Cursors.Hand
            };

            homeButton.FlatAppearance.BorderSize = 0;
            GraphicsPath homePath = new GraphicsPath();
            homePath.AddRectangle(new Rectangle(0, 0, 40, 40));
            homeButton.Region = new Region(homePath);

            homeButton.Click += (s, e) => {
                var viewMain = new ViewOrigin.ViewMain();
                viewMain.Show();
                this.Close();
            };
            this.Controls.Add(homeButton);

            // 📦 PANEL PRINCIPAL (AZUL OSCURO CON BORDE AZUL CLARO)
            var mainFormPanel = new Panel
            {
                Size = new Size(500, 600),
                BackColor = darkBlueColor,
                Padding = new Padding(40, 30, 40, 30),
                Location = new Point((this.Width - 500) / 2, 50)
            };
            ApplyRoundedCorners(mainFormPanel, 15);
            ApplyLightBlueBorder(mainFormPanel, 3);

            // 🏷️ TÍTULO PRINCIPAL
            var titleLabel = new Label
            {
                Text = "CONFIRMAR COSECHA",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = whiteColor,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 40) // Más espacio debajo del título
            };
            mainFormPanel.Controls.Add(titleLabel);

            // 📝 CONTENEDOR DE CAMPOS
            var fieldsContainer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                AutoScroll = true
            };

            // Agregar campos al diseño
            AddFieldsToDesign(fieldsContainer);

            mainFormPanel.Controls.Add(fieldsContainer);

            // 🔘 PANEL DE BOTONES
            var buttonsPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 20, 0, 0) // Más espacio arriba de los botones
            };

            ConfigureButtonsDesign(buttonsPanel);
            mainFormPanel.Controls.Add(buttonsPanel);

            this.Controls.Add(mainFormPanel);

            // 📍 BREADCRUMB (inicio / cosecha / confirmar)
            var breadcrumbLabel = new Label
            {
                Text = "inicio / cosecha / confirmar",
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = blackColor,
                Location = new Point(40, mainFormPanel.Bottom + 20),
                AutoSize = true
            };
            this.Controls.Add(breadcrumbLabel);

            // 🔄 AJUSTAR AL REDIMENSIONAR
            this.Resize += (s, e) => {
                mainFormPanel.Location = new Point((this.Width - mainFormPanel.Width) / 2, 50);
                breadcrumbLabel.Location = new Point(40, mainFormPanel.Bottom + 20);
                homeButton.Location = new Point(this.ClientSize.Width - 50, 10);
            };
        }

        private void CreateLogoPlaceholder(PictureBox pictureBox)
        {
            var placeholder = new Bitmap(180, 120);
            using (var g = Graphics.FromImage(placeholder))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.LightGray);

                using (var font = new Font("Segoe UI", 8, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.Gray))
                {
                    var text = "LOGO\nCAFICAUCA";
                    var textSize = g.MeasureString(text, font);
                    g.DrawString(text, font, brush,
                        (120 - textSize.Width) / 2,
                        (80 - textSize.Height) / 2);
                }
            }
            pictureBox.Image = placeholder;
        }

        private void AddFieldsToDesign(Panel container)
        {
            int currentY = 20; // Empezar más arriba para mejor distribución
            int fieldWidth = 420;
            int labelHeight = 25;
            int fieldHeight = 40;
            int verticalSpacing = 35; // Aumentado de 25 a 35 para más separación

            // 🔹 LOTE
            CreateStyledFieldWithValue(container, "Lote", lbIdPlot, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 PRECIO POR KILO
            CreateStyledFieldWithValue(container, "Precio por Kilo", lbPricePerKilo, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 FECHA DE INICIO
            CreateStyledFieldWithValue(container, "Fecha de inicio", lbStartDate, currentY, fieldWidth, labelHeight, fieldHeight);

            // Forzar scroll si es necesario
            container.AutoScrollMinSize = new Size(fieldWidth, currentY + fieldHeight + 50);
        }

        private void CreateStyledFieldWithValue(Panel container, string labelText, Control control, int y, int width, int labelHeight, int fieldHeight)
        {
            // Label del campo
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

            // Control con estilo (igual que ViewHarvestRegister)
            if (control != null)
            {
                control.Location = new Point(0, y + labelHeight + 12); // Aumentado de 8 a 12 para más separación
                control.Size = new Size(width, fieldHeight);
                control.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                control.BackColor = whiteColor;
                control.ForeColor = blackColor;

                // Aplicar bordes y estilo idéntico a ViewHarvestRegister
                if (control is System.Windows.Forms.Label labelControl)
                {
                    labelControl.BorderStyle = BorderStyle.FixedSingle;
                    labelControl.Padding = new Padding(12, 8, 12, 8);
                }

                ApplyRoundedCorners(control, 8);
                container.Controls.Add(control);
            }
        }

        private void ConfigureButtonsDesign(Panel buttonPanel)
        {
            // Botón Confirmar (Verde)
            btnConfirm.BackColor = greenColor;
            btnConfirm.ForeColor = whiteColor;
            btnConfirm.FlatStyle = FlatStyle.Flat;
            btnConfirm.FlatAppearance.BorderSize = 0;
            ApplyRoundedCorners(btnConfirm, 8);

            // Botón Rechazar (Rojo)
            btnDecline.BackColor = redColor;
            btnDecline.ForeColor = whiteColor;
            btnDecline.FlatStyle = FlatStyle.Flat;
            btnDecline.FlatAppearance.BorderSize = 0;
            ApplyRoundedCorners(btnDecline, 8);

            int panelWidth = 420;
            int totalButtonsWidth = btnConfirm.Width + 30 + btnDecline.Width;
            int startX = (panelWidth - totalButtonsWidth) / 2;

            btnConfirm.Location = new Point(startX, 20); // Aumentado de 15 a 20 para más espacio
            btnDecline.Location = new Point(startX + btnConfirm.Width + 30, 20);

            buttonPanel.Controls.Add(btnConfirm);
            buttonPanel.Controls.Add(btnDecline);
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que no haya cosechas activas en el mismo lote que se solapen
                ValidateNoOverlappingHarvests();

                long idHarvest = AppServices.HarvestServices.save.execute(harvestDTO.idPlot, harvestDTO.startDate, harvestDTO.pricePerKilo);
                MessageBox.Show($"Cosecha numero: {idHarvest} registrada", "Confirmación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                viewHarvest.loadHarvests();
                viewHarvest.Show();

                this.Owner.Close();
                this.Close();
            }
            catch (HarvestActiveExistsException ex)
            {
                MessageBox.Show(ex.Message, "Regla de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Conflicto de fechas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la cosecha: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ValidateNoOverlappingHarvests()
        {
            try
            {
                // Obtener todas las cosechas del mismo lote
                var allHarvests = AppServices.HarvestServices.query.execute();

                // Convertir a DTOs para trabajar consistentemente
                var allHarvestsDTO = HarvestMaper.ToDTOList(allHarvests);
                var plotHarvests = allHarvestsDTO.Where(h => h.idPlot == harvestDTO.idPlot).ToList();

                foreach (var existingHarvest in plotHarvests)
                {
                    // Si la cosecha existente está activa (sin fecha de fin)
                    if (existingHarvest.endDate == null)
                    {
                        // Ya hay una cosecha activa en este lote
                        throw new InvalidOperationException(
                            $"Ya existe una cosecha activa en este lote (Cosecha ID: {existingHarvest.id}). " +
                            $"No se puede crear una nueva cosecha mientras haya una activa.");
                    }

                    // Si la cosecha existente tiene fecha de fin, verificar solapamiento
                    if (existingHarvest.endDate.HasValue)
                    {
                        // Si la nueva cosecha comienza durante una cosecha existente
                        if (harvestDTO.startDate >= existingHarvest.startDate &&
                            harvestDTO.startDate <= existingHarvest.endDate.Value)
                        {
                            throw new InvalidOperationException(
                                $"La fecha de inicio seleccionada ({harvestDTO.startDate.ToShortDateString()}) " +
                                $"cae dentro del período de una cosecha existente (Cosecha ID: {existingHarvest.id}) " +
                                $"que va desde {existingHarvest.startDate.ToShortDateString()} " +
                                $"hasta {existingHarvest.endDate.Value.ToShortDateString()}.");
                        }
                    }
                }
            }
            catch (Exception ex) when (!(ex is InvalidOperationException))
            {
                // Si hay un error en la validación, relanzar como InvalidOperationException
                throw new InvalidOperationException($"Error validando fechas de cosecha: {ex.Message}");
            }
        }

        private void btnDecline_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void ViewRegisterConfirm_Load(object sender, EventArgs e)
        {
            viewHarvest.loadHarvests();
        }
    }
}