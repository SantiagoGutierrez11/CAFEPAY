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

namespace CAFEPAY.Views.ViewHarvest
{
    public partial class ViewHarvestRegister : Form
    {
        List<Plot> plots = new List<Plot>();
        List<PlotDTO> plotsDTO = new List<PlotDTO>();

        // Colores exactos del diseño
        private Color darkBlueColor = Color.FromArgb(13, 43, 97);  // #0D2B61 - Azul oscuro del formulario
        private Color lightBlueColor = Color.FromArgb(100, 149, 237); // #6495ED - Azul claro del contorno
        private Color greenColor = Color.FromArgb(11, 110, 51);    // #0B6E33 - Verde del botón Agregar
        private Color whiteColor = Color.White;
        private Color blackColor = Color.Black;
        private Color darkGrayColor = Color.FromArgb(64, 64, 64); // Gris oscuro para el botón home

        // Variable para el botón home
        private Button homeButton;

        public ViewHarvestRegister()
        {
            InitializeCustomComponents();
            ApplyVisualDesign();
            loadSettings();
            loadComboBoxPlot();

            // Pantalla completa
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeCustomComponents()
        {
            // ComboBox para Lote
            cmbIdPlot = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // DateTimePicker para Fecha de inicio - CON CALENDARIO COMPLETO
            dtTmStartDate = new DateTimePicker
            {
                Font = new Font("Segoe UI", 11),
                Format = DateTimePickerFormat.Short,
                ShowUpDown = false, // Esto permite ver el calendario completo
                MinDate = new DateTime(2020, 1, 1), // Fecha mínima razonable
                MaxDate = new DateTime(2030, 12, 31) // Fecha máxima razonable
            };

            // TextBox para Precio por Kilo - SOLO NÚMEROS (Máximo 4 dígitos)
            textBoxPricePerKilo = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                MaxLength = 4 // Máximo 4 números
            };

            // Botón Agregar
            btnAdd = new Button
            {
                Text = "Agregar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(120, 40),
                Cursor = Cursors.Hand
            };
            btnAdd.Click += btnAdd_Click;

            // Botón Cancelar
            btnDecline = new Button
            {
                Text = "Cancelar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Size = new Size(120, 40),
                Cursor = Cursors.Hand
            };
            btnDecline.Click += btnDecline_Click;
        }

        private void loadSettings()
        {
            var today = DateTime.Today;
            // Configuramos un rango razonable para el calendario
            dtTmStartDate.MinDate = new DateTime(2020, 1, 1);
            dtTmStartDate.MaxDate = new DateTime(2030, 12, 31);
            dtTmStartDate.Value = today;

            // Personalizar el DateTimePicker
            CustomizeDateTimePicker();
        }

        private void CustomizeDateTimePicker()
        {
            // Personalizar el aspecto del DateTimePicker
            dtTmStartDate.CalendarMonthBackground = Color.FromArgb(240, 240, 240);
            dtTmStartDate.CalendarTitleBackColor = darkBlueColor;
            dtTmStartDate.CalendarTitleForeColor = whiteColor;
            dtTmStartDate.CalendarForeColor = blackColor;

            // Intentar aplicar bordes redondeados al dropdown
            try
            {
                // Esto hará que el DateTimePicker tenga bordes redondeados
                ApplyRoundedCorners(dtTmStartDate, 8);
            }
            catch (Exception)
            {
                // Si falla, no hacemos nada
            }
        }

        public void loadComboBoxPlot()
        {
            plots = AppServices.PlotServices.query.execute();
            plotsDTO = PlotMapper.ToDTOList(plots);

            var active = plotsDTO
                .Where(p => p.status == 1 || string.Equals(p.statusText, "ACTIVO", StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p.idPlot)
                .Select(p => new KeyValuePair<long, string>(p.idPlot, $"{p.idPlot} - {p.name}"))
                .ToList();

            cmbIdPlot.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIdPlot.DataSource = active;
            cmbIdPlot.DisplayMember = "Value";
            cmbIdPlot.ValueMember = "Key";
        }

        private void ApplyVisualDesign()
        {
            // Configuración principal del formulario
            this.BackColor = whiteColor;
            this.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            this.Padding = new Padding(20);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 700);
            this.Text = "Registrar Nueva Cosecha";

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
                var viewMain = new ViewMain.ViewMain();
                viewMain.Show();
                this.Close();
            };
            this.Controls.Add(homeButton);

            // 📦 PANEL PRINCIPAL (AZUL OSCURO CON BORDE AZUL CLARO)
            var mainFormPanel = new Panel
            {
                Size = new Size(500, 550),
                BackColor = darkBlueColor,
                Padding = new Padding(40, 30, 40, 30),
                Location = new Point((this.Width - 500) / 2, 50)
            };
            ApplyRoundedCorners(mainFormPanel, 15);
            ApplyLightBlueBorder(mainFormPanel, 3);

            // 🏷️ TÍTULO PRINCIPAL
            var titleLabel = new Label
            {
                Text = "AGREGAR COSECHA",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = whiteColor,
                Dock = DockStyle.Top,
                Height = 50,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(0, 0, 0, 30)
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
                Height = 70,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 15, 0, 0)
            };

            ConfigureButtonsDesign(buttonsPanel);
            mainFormPanel.Controls.Add(buttonsPanel);

            this.Controls.Add(mainFormPanel);

            // 📍 BREADCRUMB (inicio / cosecha / registrar)
            var breadcrumbLabel = new Label
            {
                Text = "inicio / cosecha / agregar",
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
            int currentY = 80;
            int fieldWidth = 420;
            int labelHeight = 25;
            int fieldHeight = 40;
            int verticalSpacing = 25;

            // 🔹 LOTE
            CreateStyledComboBoxField(container, "Lote", cmbIdPlot, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 FECHA DE INICIO
            CreateStyledDateTimePickerField(container, "Fecha de inicio", dtTmStartDate, currentY, fieldWidth, labelHeight, fieldHeight);
            currentY += fieldHeight + verticalSpacing;

            // 🔹 PRECIO POR KILO (CORREGIDO)
            CreateStyledFieldWithExample(container, "Precio por Kilo", "Ej: 1500", textBoxPricePerKilo, currentY, fieldWidth, labelHeight, fieldHeight);

            // Forzar scroll si es necesario
            container.AutoScrollMinSize = new Size(fieldWidth, currentY + fieldHeight + 50);
        }

        private void CreateStyledFieldWithExample(Panel container, string labelText, string exampleText, Control control, int y, int width, int labelHeight, int fieldHeight)
        {
            // Panel para contener título y ejemplo
            var headerPanel = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, labelHeight),
                BackColor = Color.Transparent
            };

            // Label principal (título)
            var label = new Label
            {
                Text = labelText,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = whiteColor,
                Location = new Point(0, 0),
                Size = new Size(width / 2, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Label de ejemplo (estilo diferente)
            var exampleLabel = new Label
            {
                Text = exampleText,
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.LightGray,
                Location = new Point(width / 2, 0),
                Size = new Size(width / 2, labelHeight),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleRight
            };

            headerPanel.Controls.Add(label);
            headerPanel.Controls.Add(exampleLabel);
            container.Controls.Add(headerPanel);

            // Control con estilo
            if (control != null)
            {
                control.Location = new Point(0, y + labelHeight + 8);
                control.Size = new Size(width, fieldHeight);
                control.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                control.BackColor = whiteColor;
                control.ForeColor = blackColor;

                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.Padding = new Padding(12, 8, 12, 8);
                    textBox.Text = "";
                    // Agregar evento para validar solo números en tiempo de escritura
                    textBox.KeyPress += TextBoxPricePerKilo_KeyPress;
                }

                ApplyRoundedCorners(control, 8);
                container.Controls.Add(control);
            }
        }

        // Evento para validar que solo se ingresen números en el precio
        private void TextBoxPricePerKilo_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo dígitos, control keys (backspace, delete, etc.)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                // Mostrar tooltip o mensaje indicando que solo se permiten números
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void CreateStyledComboBoxField(Panel container, string labelText, ComboBox comboBox, int y, int width, int labelHeight, int fieldHeight)
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

            // ComboBox con estilo
            if (comboBox != null)
            {
                comboBox.Location = new Point(0, y + labelHeight + 8);
                comboBox.Size = new Size(width, fieldHeight);
                comboBox.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                comboBox.BackColor = whiteColor;
                comboBox.ForeColor = blackColor;
                comboBox.FlatStyle = FlatStyle.Flat;
                ApplyRoundedCorners(comboBox, 8);
                container.Controls.Add(comboBox);
            }
        }

        private void CreateStyledDateTimePickerField(Panel container, string labelText, DateTimePicker dateTimePicker, int y, int width, int labelHeight, int fieldHeight)
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

            // DateTimePicker con estilo
            if (dateTimePicker != null)
            {
                dateTimePicker.Location = new Point(0, y + labelHeight + 8);
                dateTimePicker.Size = new Size(width, fieldHeight);
                dateTimePicker.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                dateTimePicker.BackColor = whiteColor;
                dateTimePicker.ForeColor = blackColor;
                dateTimePicker.Format = DateTimePickerFormat.Short;
                ApplyRoundedCorners(dateTimePicker, 8);
                container.Controls.Add(dateTimePicker);
            }
        }

        private void ConfigureButtonsDesign(Panel buttonPanel)
        {
            // Botón Agregar (Verde)
            btnAdd.BackColor = greenColor;
            btnAdd.ForeColor = whiteColor;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.FlatAppearance.BorderSize = 0;
            ApplyRoundedCorners(btnAdd, 8);

            // Botón Cancelar (Rojo)
            btnDecline.BackColor = Color.FromArgb(183, 32, 46); // Rojo vino
            btnDecline.ForeColor = whiteColor;
            btnDecline.FlatStyle = FlatStyle.Flat;
            btnDecline.FlatAppearance.BorderSize = 0;
            ApplyRoundedCorners(btnDecline, 8);

            int panelWidth = 420;
            int totalButtonsWidth = btnAdd.Width + 30 + btnDecline.Width;
            int startX = (panelWidth - totalButtonsWidth) / 2;

            btnAdd.Location = new Point(startX, 15);
            btnDecline.Location = new Point(startX + btnAdd.Width + 30, 15);

            buttonPanel.Controls.Add(btnAdd);
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

        // MÉTODOS EXISTENTES DEL FORMULARIO ORIGINAL
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var _idPlot = (long)cmbIdPlot.SelectedValue;
            var _pricePerKilo = textBoxPricePerKilo.Text.Trim();
            var _startDate = dtTmStartDate.Value;

            // Validaciones básicas
            if (cmbIdPlot.SelectedValue == null)
            {
                MessageBox.Show("El campo 'Lote' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbIdPlot.Focus(); return;
            }
            if (string.IsNullOrWhiteSpace(_pricePerKilo))
            {
                MessageBox.Show("El campo 'Precio por Kilo' es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPricePerKilo.Focus(); return;
            }

            // Validar que solo contenga números
            if (!_pricePerKilo.All(char.IsDigit))
            {
                MessageBox.Show("El Precio por Kilo solo puede contener números.\nNo se permiten puntos, comas, espacios ni otros caracteres.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPricePerKilo.Focus();
                textBoxPricePerKilo.SelectAll();
                return;
            }

            // Validar máximo 4 números
            if (_pricePerKilo.Length > 4)
            {
                MessageBox.Show("El Precio por Kilo no puede tener más de 4 dígitos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPricePerKilo.Focus();
                textBoxPricePerKilo.SelectAll();
                return;
            }

            // Convertir a decimal (ya validamos que son solo números)
            if (!decimal.TryParse(_pricePerKilo, out decimal priceValue))
            {
                MessageBox.Show("El Precio por Kilo debe ser un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPricePerKilo.Focus(); return;
            }

            // Validar que esté entre 500 y 2000 pesos
            if (priceValue < 500)
            {
                MessageBox.Show("El Precio por Kilo debe ser de al menos $500 pesos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPricePerKilo.Focus();
                textBoxPricePerKilo.SelectAll();
                return;
            }

            if (priceValue > 2000)
            {
                MessageBox.Show("El Precio por Kilo no puede ser mayor a $2,000 pesos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPricePerKilo.Focus();
                textBoxPricePerKilo.SelectAll();
                return;
            }

            // Validar que la fecha sea posterior o igual a hoy
            if (_startDate.Date < DateTime.Today)
            {
                MessageBox.Show("La fecha de inicio no puede ser anterior a hoy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtTmStartDate.Focus(); return;
            }

            // Validar que no haya cosechas activas o que se solapen en el mismo lote
            try
            {
                ValidateNoOverlappingHarvests(_idPlot, _startDate);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Conflicto de fechas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var saveHarvest = new HarvestDTO
            {
                id = null,
                idPlot = _idPlot,
                startDate = _startDate,
                pricePerKilo = priceValue,
                status = 1,
                statusText = "ACTIVO"
            };

            ViewHarvestRegisterConfirm viewHarvestRegisterConfirm = new ViewHarvestRegisterConfirm(saveHarvest, cmbIdPlot.Text, (ViewHarvest)this.Owner);
            viewHarvestRegisterConfirm.Owner = this;
            viewHarvestRegisterConfirm.Show();
            this.Hide();
        }

        private void ValidateNoOverlappingHarvests(long plotId, DateTime startDate)
        {
            try
            {
                // Obtener todas las cosechas del mismo lote
                var allHarvests = AppServices.HarvestServices.query.execute();

                // Convertir a DTOs para trabajar consistentemente
                var allHarvestsDTO = HarvestMaper.ToDTOList(allHarvests);
                var plotHarvests = allHarvestsDTO.Where(h => h.idPlot == plotId).ToList();

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
                        if (startDate >= existingHarvest.startDate &&
                            startDate <= existingHarvest.endDate.Value)
                        {
                            throw new InvalidOperationException(
                                $"La fecha de inicio seleccionada ({startDate.ToShortDateString()}) " +
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
            if (this.Owner != null)
            {
                var originalWindowState = this.Owner.WindowState;
                this.Owner.Show();
                this.Owner.BringToFront();
                this.Owner.WindowState = originalWindowState;
                this.Owner.Refresh();
            }
            this.Close();
        }

        // Métodos vacíos para compatibilidad
        private void ViewHarvestRegister_Load(object sender, EventArgs e) { }
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e) { }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void textBoxIdPlot_TextChanged(object sender, EventArgs e) { }
        private void cmbIdPlot_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}